using AutoMapper;
using BestCV.Application.Models.Employer;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BestCV.Application.Models.Candidates;
using BestCV.Domain.Aggregates.Candidate;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using BestCV.Domain.Aggregates.Employer;
using NPOI.SS.Formula.Functions;

namespace BestCV.Application.Services.Implement
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository employerRepository;
        private readonly IEmployerMetaRepository employerMetaRepository;
        private readonly IEmployerWalletRepository employerWalletRepository;
        private readonly IWalletTypeRepository walletTypeRepository;
        private readonly ILogger<EmployerService> logger;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;

        public EmployerService(
            IEmployerRepository _employerRepository,
            IEmployerMetaRepository _emloyerMetaRepository,
            IEmployerWalletRepository _employerWalletRepository,
            IWalletTypeRepository _walletTypeRepository,
            ILoggerFactory _loggerFactory,
            IMapper _mapper,
            ITokenService _tokenService,
            IConfiguration _configuration)
        {
            employerRepository = _employerRepository;
            employerMetaRepository = _emloyerMetaRepository;
            employerWalletRepository = _employerWalletRepository;
            walletTypeRepository = _walletTypeRepository; 
            logger = _loggerFactory.CreateLogger<EmployerService>();
            mapper = _mapper;
            tokenService = _tokenService;
            configuration = _configuration;
        }

        public async Task<BestCVResponse> ChangePassword(long id, ChangePasswordEmployerDTO obj)
        {
            var employer = await employerRepository.GetByIdAsync(id);
            if (employer == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", id);
            }
            if (employer.Password != obj.OldPassword.ToHash256())
            {
                return BestCVResponse.BadRequest(new List<string>() { "Mật khẩu cũ không chính xác." });
            }
            employer.Password = obj.NewPassword.ToHash256();
            await employerRepository.UpdateAsync(employer);
            await employerRepository.SaveChangesAsync();
            return BestCVResponse.Success(employer);
        }



        public async Task<BestCVResponse> CheckVerifyCode(string value, string hashCode)
        {
            var dataMeta = await employerMetaRepository.CheckVerifyCode(value, hashCode);
            if (dataMeta != null)
            {
                //check expiry time
                if (DateTime.Now > dataMeta.CreatedTime.AddHours(EmployerMetaConstants.CONFIRM_EMAIL_EXPIRY_TIME))
                {
                    return BestCVResponse.Error();
                }
                else
                {
                    dataMeta.Active = false;
                    // deactive meta
                    await employerMetaRepository.UpdateAsync(dataMeta);
                    await employerMetaRepository.SaveChangesAsync();
                    return BestCVResponse.Success(dataMeta);
                }
            }
            else
            {
                return BestCVResponse.NotFound("", dataMeta);
            }
        }

        public Task<BestCVResponse> CreateAsync(EmployerSignUpDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<EmployerSignUpDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var data = await employerRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<EmployerDTO>(data);
            return BestCVResponse.Success(model);
        }


        public async Task<(BestCVResponse, EmailMessage<EmployerConfirmEmailBody>?)> SignUp(EmployerSignUpDTO obj)
        {
            var checkEmail = await employerRepository.EmailExisted(obj.Email);
            var checkPhone = await employerRepository.PhonedExisted(obj.Phone);

            if (checkEmail)
            {
                return (BestCVResponse.Error("Email đã được sử dụng."), null);
            }
            else if (checkPhone)
            {
                return (BestCVResponse.Error("Số điện thoại đã được sử dụng."), null);
            }
            else
            {
                var emp = mapper.Map<Employer>(obj);
                emp.Username = obj.Email;
                emp.EmployerStatusId = Convert.ToInt32(AccountStatusId.ACTIVE);
                emp.Password = obj.Password.ToHash256();
                emp.Search = emp.Email + " " + emp.Username + " " + emp.Fullname + " " + emp.Phone + " " + emp.SkypeAccount;
                emp.SkypeAccount = obj.SkypeAccount.Trim().Length > 0 ? obj.SkypeAccount : null;

                var database = await employerMetaRepository.BeginTransactionAsync();

                using (database)
                {
                    try
                    {
                        // thêm vào bảng employer
                        await employerRepository.CreateAsync(emp);
                        // lưu thay đổi trong bảng employer
                        await employerRepository.SaveChangesAsync();

                        if (emp.Id > 0)
                        {
                            var randomString = StringExtension.RandomString(8);
                            var employerMeta = new EmployerMeta()
                            {
                                Id = 0,
                                Active = true,
                                Name = emp.Username,
                                CreatedTime = DateTime.Now,
                                EmployerId = emp.Id,
                                Key = EmployerMetaConstants.CONFIRM_EMAIL_KEY,
                                Value = randomString,
                                Description = (emp.Email + "-" + randomString).ToHash256()
                            };

                            // thêm vào bảng employerMeta 
                            await employerMetaRepository.CreateAsync(employerMeta);
                            // lưu thay đổi trong bảng employerMeta
                            await employerMetaRepository.SaveChangesAsync();

                            List<WalletType> walletTypes = await walletTypeRepository.FindByConditionAsync(x => x.Active);
                            List<EmployerWallet> employerWallets = new List<EmployerWallet>();
                            if (walletTypes != null)
                            {
                                foreach (var walletType in walletTypes)
                                {
                                    var employerWallet = new EmployerWallet()
                                    {
                                        Id = 0,
                                        WalletTypeId = walletType.Id,
                                        EmployerId = emp.Id,
                                        Active = true,
                                        Value = EmployerWalletConstants.DEFAULT_VALUE,
                                        CreatedTime = DateTime.Now
                                    };
                                    employerWallets.Add(employerWallet);
                                }
                            }
                            // thêm vào list employerWallet bảng employerWallet
                            await employerWalletRepository.CreateListAsync(employerWallets);
                            // lưu thay đổi trong bảng employerWallet
                            await employerWalletRepository.SaveChangesAsync();

                            if (employerMeta.Id > 0)
                            {
                                // setup email khi đăng ký thành công
                                var message = SendEmailAsync(emp, employerMeta);

                                logger.LogInformation("Tài khoản {Username} đã được tạo lúc {CreatedTime}", emp.Username, DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));


                                // hoàn thành transaction
                                await employerMetaRepository.EndTransactionAsync();
                                return (BestCVResponse.Success(obj), message);
                            }
                            else
                            {
                                await employerMetaRepository.RollbackTransactionAsync();
                                await employerWalletRepository.RollbackTransactionAsync();
                                return (BestCVResponse.Error("Đăng ký tài khoản nhà tuyển dụng không thành công"), null);
                            }
                        }
                        else
                        {
                            await employerMetaRepository.RollbackTransactionAsync();
                            await employerWalletRepository.RollbackTransactionAsync();
                            return (BestCVResponse.Error("Đăng ký tài khoản nhà tuyển dụng không thành công"), null);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Có lỗi khi tạo tài khoản nhà tuyển dụng{emp.Username}");
                        return (BestCVResponse.Error(), null);
                    }
                }


            }

        }


        public async Task<(BestCVResponse, EmailMessage<ForgotPasswordEmailBody>?)> ForgotPassword(string email)
        {
            var emp = await employerRepository.FindByEmail(email);
            var database = await employerMetaRepository.BeginTransactionAsync();
            using (database)
            {
                try
                {
                    if (emp.Id > 0)
                    {
                        var randomString = StringExtension.RandomString(8);
                        var employerMeta = new EmployerMeta()
                        {
                            Id = 0,
                            Active = true,
                            Name = emp.Username,
                            CreatedTime = DateTime.Now,
                            EmployerId = emp.Id,
                            Key = EmployerMetaConstants.FORGOT_PASSWORD_EMAIL_KEY,
                            Value = randomString,
                            Description = (emp.Email + "-" + randomString).ToHash256()
                        };

                        // thêm vào bảng employerMeta 
                        await employerMetaRepository.CreateAsync(employerMeta);
                        // lưu thay đổi trong bảng employerMeta
                        await employerMetaRepository.SaveChangesAsync();
                        if (employerMeta.Id > 0)
                        {
                            // setup email 
                            var message = SendEmailForgotPasswordAsync(emp, employerMeta);
                            // hoàn thành transaction
                            await employerMetaRepository.EndTransactionAsync();
                            return (BestCVResponse.Success(), message);
                        }
                        else
                        {
                            await employerMetaRepository.RollbackTransactionAsync();
                            return (BestCVResponse.Error("Email chưa kích hoạt. "), null);
                        }
                    }
                    else
                    {
                        await employerMetaRepository.RollbackTransactionAsync();
                        return (BestCVResponse.Error("Email chưa kích hoạt."), null);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Có lỗi khi gửi email");
                    return (BestCVResponse.Error(), null);
                }
            }

        }

        public Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateEmployerDTO obj)
        {
            List<string> listErrors = new List<string>();
            var skypeAccount = await employerRepository.SkypeAccountExisted(obj.SkypeAccount);
            if (obj.Id == 0)
            {
                listErrors.Add("Tài khoản Không tồn tại");
            }
            if (skypeAccount)
            {
                listErrors.Add("Tài khoản skype đã được sử dụng");
            }
            if (listErrors.Count > 0)
            {
                return BestCVResponse.BadRequest(listErrors);
            }
            var employer = await employerRepository.GetByIdAsync(obj.Id);
            if (employer == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, employer);
            await employerRepository.UpdateAsync(model);
            await employerRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateEmployerDTO> obj)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> SignInAsync(SignInEmployerDTO obj)
        {
            List<string> errors = new();
            Employer? employer = null;
            // Nếu là Email
            if (obj.EmailOrPhone.Contains('@'))
            {
                employer = await employerRepository.GetByEmailAsync(obj.EmailOrPhone);

            }
            // Còn không thì tức là số điện thoại
            else
            {
                employer = await employerRepository.GetByPhoneAsync(obj.EmailOrPhone);
            }

            // Nếu tìm thấy Employer và đúng mật khẩu
            if (employer != null && employer.Password.Equals(obj.Password.ToHash256()))
            {
                // Nếu tài khoản nhà tuyển dụng đã được kích hoạt
                if (employer.IsActivated)
                {
                    AccountToken accountToken = new()
                    {
                        Id = employer.Id,
                        Username = employer.Username,
                        Email = employer.Email,
                    };
                    string token = tokenService.GenerateToken(accountToken);
                    logger.LogInformation("User {Email} logged in at {Time}.", accountToken.Email, DateTime.UtcNow);

                    LoginEmployerModel loginModel = new()
                    {
                        Fullname = employer.Fullname,
                        Photo = (!string.IsNullOrEmpty(employer.Photo) ? employer.Photo : "uploads/admin/default_avatar.jpg"),
                        Token = token,
                        //Username = employer.Username,
                        //Email = employer.Email
                    };
                    return BestCVResponse.Success(loginModel);
                }
                errors.Add("Tài khoản của bạn chưa được kích hoạt.");
                return BestCVResponse.BadRequest(errors);
            }
            errors.Add("Sai thông tin tài khoản hoặc mật khẩu.");
            return BestCVResponse.BadRequest(errors);
        }



        public async Task ActivateAccount(string email)
        {
            var emp = await employerRepository.FindByEmail(email);
            emp.IsActivated = true;
            await employerRepository.UpdateAsync(emp);
            await employerRepository.SaveChangesAsync();
        }



        public EmailMessage<EmployerConfirmEmailBody> SendEmailAsync(Employer emp, EmployerMeta empMeta)
        {
            var host = configuration["SectionUrls:EmployerVerifiedNotificationPage"];

            var message = new EmailMessage<EmployerConfirmEmailBody>()
            {
                ToEmails = new List<string> { emp.Email },
                CcEmails = new List<string> { },
                BccEmails = new List<string> { },
                Subject = "Xác thực đăng ký tài khoản nhà tuyển dụng hệ thống Jobi",
                Model = new EmployerConfirmEmailBody()
                {
                    Fullname = emp.Fullname,
                    Otp = empMeta.Value,
                    ActiveLink = $"{host}/{empMeta.Value}/{empMeta.Description}",
                    Time = DateTime.Now.Year
                }
            };
            return message;
        }

        public EmailMessage<ForgotPasswordEmailBody> SendEmailForgotPasswordAsync(Employer emp, EmployerMeta empMeta)
        {
            var host = configuration["SectionUrls:EmployerVerifiedForgotPasswordNotificationPage"];

            var message = new EmailMessage<ForgotPasswordEmailBody>()
            {
                ToEmails = new List<string> { emp.Email },
                CcEmails = new List<string> { },
                BccEmails = new List<string> { },
                Subject = "Thay đổi mật khẩu tài khoản nhà tuyển dụng hệ thống Jobi",
                Model = new ForgotPasswordEmailBody()
                {
                    Fullname = emp.Fullname,
                    ActiveLink = $"{host}/{empMeta.Value}/{empMeta.Description}",
                    Time = DateTime.Now.Year,
                    validExpried = EmployerMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT,

                }
            };
            return message;
        }



        public async Task<EmployerMeta> GetEmailByVerifyCode(ConfirmEmailEmployerDTO obj)
        {
            var data = (await employerMetaRepository.FindByConditionAsync(x => x.Value == obj.Value && x.Description == obj.Hash)).FirstOrDefault();
            return data;
        }



        public async Task<(BestCVResponse, EmailMessage<EmployerConfirmEmailBody>?)> ReSendEmail(string email)
        {
            // list employer meta data
            var dataMeta = (await employerMetaRepository.FindByConditionAsync(x => x.Name == email));
            // employer entiey
            var employer = (await employerRepository.FindByConditionAsync(x => x.Email.Equals(email))).FirstOrDefault();


            if (dataMeta.Count > 0)
            {
                // check email sent in day
                int countSent = 0;
                foreach (var row in dataMeta)
                {
                    if (row.CreatedTime.Date.Equals(DateTime.Now.Date))
                    {
                        countSent++;
                    }
                }

                // create new row meta 
                var randomString = StringExtension.RandomString(8);
                dataMeta[0].Id = 0;
                dataMeta[0].CreatedTime = DateTime.Now;
                dataMeta[0].Value = randomString;
                dataMeta[0].Description = (employer.Email + "-" + randomString).ToHash256();

                // thêm vào bảng employerMeta 
                await employerMetaRepository.CreateAsync(dataMeta[0]);
                // lưu thay đổi trong bảng employerMeta
                await employerMetaRepository.SaveChangesAsync();


                // check email sent times
                var message = SendEmailAsync(employer, dataMeta[0]);
                if (countSent >= EmployerMetaConstants.MAXIMUM_SEND_CONFIRM_EMAIL_PER_DAY)
                {
                    return (BestCVResponse.Error(), message);
                }
                else
                {
                    return (BestCVResponse.Success(), message);
                }
            }
            else
            {
                return (BestCVResponse.NotFound("", email), null);
            }
        }
 

        public async Task<int> CountResetPassword(long employerId)
        {
            return await employerMetaRepository.CountResetPassword(employerId);
        }

        public async Task<bool> CheckEmailIsActive(string email)
        {
            return await employerRepository.CheckEmailIsActive(email);
        }

        public async Task<Employer?> GetByEmailAsync(string email)
        {
            return  await employerRepository.GetByEmailAsync(email);          
        }

        public async Task<bool> CheckKeyValid (string code , string hash)
        {
            var result = false;
            var dataMeta = await employerMetaRepository.CheckVerifyCode(code, hash);
            if (dataMeta != null)
            {
                var time = DateTime.Now - dataMeta.CreatedTime;
                if(time < TimeSpan.FromHours(EmployerMetaConstants.FORGOT_PASSWORD_TOKEN_EXPRIED))
                {
                    result = true;
                }
            }
            return result;
        }

        public async Task<BestCVResponse> ResetNewPassword(string code, string hash,string password)
        {
            var dataMeta = await employerMetaRepository.CheckVerifyCode(code, hash);
            var employer = await employerRepository.GetByIdAsync(dataMeta.EmployerId);
            employer.Password = password.ToHash256();
            await employerRepository.UpdateAsync(employer);
            await employerRepository.SaveChangesAsync();
            dataMeta.Active = false;
            await employerMetaRepository.UpdateAsync(dataMeta);
            await employerMetaRepository.SaveChangesAsync();
            return BestCVResponse.Success();
        }

        public async Task<object> ListEmployerAggregates(DTParameters parameter)
        {
            return await employerRepository.ListEmployerAggregates(parameter);
        }

        public async Task<BestCVResponse> AdminDetailAsync(long id)
        {
            var data = await employerRepository.GetByIdAsync(id);
            if (data != null)
            {
                var model = mapper.Map<EmployerDetailDTO>(data);
                return BestCVResponse.Success(model);
            }
            return BestCVResponse.NotFound("Không tìm thấy nhà tuyển dụng",data);
        }

        public async Task<BestCVResponse> ChangePasswordAdminAsync(ChangePasswordDTO obj)
        {
            var newEmployer = await employerRepository.GetByIdAsync(obj.Id);
            if (newEmployer == null)
            {
                return BestCVResponse.NotFound("Không tìm thấy nhà tuyển dụng", newEmployer);
            }
            if (newEmployer.Password == obj.NewPassword.ToHash256())
            {
                return BestCVResponse.Error("Mật khẩu mới không được trùng với mật khẩu đã đặt");

            }
            newEmployer.Password = obj.NewPassword.ToHash256();

            var isUpdated = await employerRepository.ChangePasswordAsync(newEmployer);
            if (isUpdated)
            {
                await employerRepository.SaveChangesAsync();
                return BestCVResponse.Success(isUpdated);
            }
            return BestCVResponse.Error("Thay đổi mật khẩu không thành công");
        }

        public async Task<BestCVResponse> QuickActivatedAsync(long id)
        {
            var isUpdated = await employerRepository.QuickActivatedAsync(id);
            if (isUpdated)
            {
                await employerRepository.SaveChangesAsync();
                return BestCVResponse.Success(isUpdated);
            }
            return BestCVResponse.BadRequest("Kích hoạt nhà tuyển dụng không thành công");
        }
    }
}
