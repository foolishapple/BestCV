using AutoMapper;
using BestCV.Application.Models.AdminAccounts;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Constants;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly IAdminAccountRepository _adminAccountRepository;
        private readonly IAdminAccountRoleRepository _adminAccountRoleRepository;
        private readonly IAdminAccountMetaRepository _adminAccountMetaRepository;
        private readonly ILogger<AdminAccountService> _logger;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        public AdminAccountService(IAdminAccountRepository adminAccountRepository, ILoggerFactory loggerFactory, IMapper mapper, ITokenService tokenService, IAdminAccountRoleRepository adminAccountRoleRepository, IConfiguration config , IAdminAccountMetaRepository adminAccountMetaRepository )
        {
            _adminAccountRepository = adminAccountRepository;
            _logger = loggerFactory.CreateLogger<AdminAccountService>();
            _mapper = mapper;
            _tokenService = tokenService;
            _adminAccountRoleRepository = adminAccountRoleRepository;
            _config = config;
            _adminAccountMetaRepository = adminAccountMetaRepository;
        }

        public async Task<BestCVResponse> CreateAsync(InsertAdminAccountDTO obj)
        {
            var adminAccount = MappingInsertAccount(obj);
            List<string> errors = await Validate(adminAccount);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            using (var trans = await _adminAccountRepository.BeginTransactionAsync())
            {
                try
                {
                    await _adminAccountRepository.CreateAsync(adminAccount);
                    await _adminAccountRepository.SaveChangesAsync();
                    var roles = obj.Roles.Select(c => new AdminAccountRole()
                    {
                        Active = true,
                        AdminAccountId = adminAccount.Id,
                        CreatedTime = DateTime.Now,
                        RoleId = c
                    });
                    await _adminAccountRoleRepository.CreateListAsync(roles);
                    await _adminAccountRoleRepository.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                catch
                {
                    await trans.RollbackAsync();
                    throw new Exception();
                }
            }
            return BestCVResponse.Success(obj);
        }

        public async Task<BestCVResponse> CreateListAsync(IEnumerable<InsertAdminAccountDTO> objs)
        {
            List<string> errors = new List<string>();
            var adminAccounts = objs.Select(c => MappingInsertAccount(c));
            foreach (var item in adminAccounts)
            {
                errors.AddRange(await Validate(item));
            }
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _adminAccountRepository.CreateListAsync(adminAccounts);
            await _adminAccountRepository.SaveChangesAsync();
            return BestCVResponse.Success(objs);
        }

        public async Task<BestCVResponse> GetAllAsync()
        {
            var data = await _adminAccountRepository.ListAllAggregate();
            return BestCVResponse.Success(data);
        }
      
        public async Task<BestCVResponse> GetByIdAsync(long id)
        {
            var adminAccount = await _adminAccountRepository.DetailAggregate(id);
            return BestCVResponse.Success(adminAccount);
        }
       
        public async Task<BestCVResponse> SoftDeleteAsync(long id)
        {
            var data = await _adminAccountRepository.SoftDeleteAsync(id);
            if (data)
            {
                if (await _adminAccountRepository.SaveChangesAsync() > 0)
                {
                    return BestCVResponse.Success();
                }
            }
            return BestCVResponse.Error();
        }
     
        public async Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<long> objs)
        {
            foreach (var id in objs)
            {
                await _adminAccountRepository.SoftDeleteAsync(id);
            }
            await _adminAccountRepository.SaveChangesAsync();
            return BestCVResponse.Success(objs);
        }
        
        public async Task<BestCVResponse> UpdateAsync(UpdateAdminAccountDTO obj)
        {
            AdminAccount updateAccount = await MappingUpdateAccount(obj);
            List<string> errors = await Validate(updateAccount);
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            using (var trans = await _adminAccountRepository.BeginTransactionAsync())
            {
                try
                {
                    await _adminAccountRepository.UpdateAsync(updateAccount);
                    await _adminAccountRepository.SaveChangesAsync();
                    var roles = await _adminAccountRoleRepository.FindByConditionAsync(c => c.Active && c.AdminAccountId == updateAccount.Id);
                    IEnumerable<long> listDelete = roles.Where(c => !obj.Roles.Contains(c.RoleId)).Select(c => c.Id);
                    List<AdminAccountRole> listAdd = obj.Roles.Select(c => new AdminAccountRole()
                    {
                        Active = true,
                        AdminAccountId = updateAccount.Id,
                        CreatedTime = DateTime.Now,
                        RoleId = c
                    }).Where(c => !roles.Select(g => g.RoleId).Contains(c.RoleId)).ToList();
                    if (listDelete.Count() > 0)
                    {
                        await _adminAccountRoleRepository.HardDeleteListAsync(listDelete);
                    }
                    if (listAdd.Count() > 0)
                    {
                        await _adminAccountRoleRepository.CreateListAsync(listAdd);
                    }
                    await _adminAccountRoleRepository.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                catch
                {
                    await trans.RollbackAsync();
                    throw new Exception();
                }
            }

            return BestCVResponse.Success(obj);
        }
      
        public async Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateAdminAccountDTO> obj)
        {
            List<AdminAccount> updateAdminAccounts = new List<AdminAccount>();
            List<string> errors = new List<string>();
            foreach (var item in obj)
            {
                var adminAccount = await MappingUpdateAccount(item);
                updateAdminAccounts.Add(adminAccount);
                errors.AddRange(await Validate(adminAccount));
            };
            if (errors.Count > 0)
            {
                return BestCVResponse.BadRequest(errors);
            }
            await _adminAccountRepository.UpdateListAsync(updateAdminAccounts);
            await _adminAccountRepository.SaveChangesAsync();
            return BestCVResponse.Success(obj);
        }
     
        public string ToSearchString(AdminAccount account)
        {
            return $"{account.Email.ToLower()}\n" +
                $"{account.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss")}\n" +
                $"{account.FullName.RemoveVietnamese().ToLower()}\n" +
                $"{account.Phone.RemoveVietnamese().ToLower()}\n" +
                $"{account.UserName.RemoveVietnamese().ToLower()}\n";
        }
      
        public AdminAccount MappingInsertAccount(InsertAdminAccountDTO obj)
        {
            var adminAccount = _mapper.Map<AdminAccount>(obj);
            adminAccount.Id = 0;
            adminAccount.Active = true;
            adminAccount.CreatedTime = DateTime.Now;
            adminAccount.Password = obj.Password.ToHash256();
            if (string.IsNullOrEmpty(adminAccount.Photo))
            {
                adminAccount.Photo = AdminAccountConst.DEFAULT_PHOTO;
            }
            adminAccount.Search = ToSearchString(adminAccount);
            return adminAccount;
        }
       
        public async Task<AdminAccount> MappingUpdateAccount(UpdateAdminAccountDTO obj)
        {
            var account = await _adminAccountRepository.GetByIdAsync(obj.Id);
            if (account == null)
            {
                throw new Exception($"Not found admin account id: {obj.Id}");
            }
            string oldPass = account.Password;
            var updateAccount = _mapper.Map(obj, account);
            if (!string.IsNullOrEmpty(obj.Password))
            {
                updateAccount.Password = obj.Password.ToHash256();
            }
            else
            {
                updateAccount.Password = oldPass;
            }
            if (string.IsNullOrEmpty(updateAccount.Photo))
            {
                updateAccount.Photo = AdminAccountConst.DEFAULT_PHOTO;
            }
            updateAccount.Search = ToSearchString(updateAccount);
            return updateAccount;
        }
       
        public async Task<List<string>> Validate(AdminAccount obj)
        {
            List<string> errors = new List<string>() { };
            if (await _adminAccountRepository.UserNameIsExist(obj.UserName, obj.Id))
            {
                errors.Add($"Tên đăng nhập đã tồn tại:{obj.UserName}");
            }
            if (await _adminAccountRepository.EmailIsExisted(obj.Email, obj.Id))
            {
                errors.Add($"Email đã tồn tại: {obj.Email}");
            }
            if (await _adminAccountRepository.PhoneIsExisted(obj.Phone, obj.Id))
            {
                errors.Add($"Số điện thoại đã tồn tại:{obj.Phone}");
            }
            return errors;
        }
       
        public async Task<BestCVResponse> SignIn(SignInAdminAccountDTO obj)
        {
            List<string> errors = new List<string>();
            AdminAccount? adminAccount = await _adminAccountRepository.FindByUserName(obj.UserName);
            if (adminAccount == null)
            {
                adminAccount = await _adminAccountRepository.FindByEmail(obj.UserName);
                if (adminAccount == null)
                {
                    errors.Add("Tên đăng nhập không đúng xin vui lòng thử lại sau.");
                    return BestCVResponse.BadRequest(errors);
                }
            }
            if (adminAccount.Password == obj.Password.ToHash256())
            {
                string accessToken = _tokenService.GenerateToken(new AccountToken()//get access token
                {
                    Email = adminAccount.Email,
                    Id = adminAccount.Id,
                    Username = adminAccount.UserName
                }, DateTime.Now.AddMinutes(int.Parse(_config["Jwt:AdminExpireMinutes"]))); 
                return BestCVResponse.Success(new SignInResponse()//Change resources
                {
                    AccessToken = accessToken
                });
            }
            else
            {
                errors.Add("Mật khẩu không đúng xin vui lòng thử lại sau.");
                return BestCVResponse.BadRequest(errors);
            }
        }
      
        public async Task<BestCVResponse> UpdatePassword(ChangePasswordAdminAccountDTO obj)
        {
            var adminAccount = await _adminAccountRepository.GetByIdAsync(obj.Id);

            if (adminAccount == null)
            {
                return BestCVResponse.NotFound($"Not found admin account id:{obj.Id}", obj);
            }
            if (adminAccount.Password != obj.OldPassword.ToHash256())
            {
                return BestCVResponse.BadRequest(new List<string>() { "Mật khẩu cũ không chính xác." });
            }
            adminAccount.Password = obj.NewPassword.ToHash256();
            await _adminAccountRepository.UpdatePassWord(adminAccount);
            return BestCVResponse.Success(adminAccount);
        }
       
        public async Task<AdminAccount?> DetailSignIn(SignInAdminAccountDTO obj)
        {
            obj.Password = obj.Password.ToHash256();
            var data = await _adminAccountRepository.DetailSignIn(obj.UserName, obj.Password);
            return data;
        }
       
        public async Task<(BestCVResponse, EmailMessage<ForgotPasswordEmailBody>?)> ForgotPassword(string email)
        {
            var acc = await _adminAccountRepository.FindByEmail(email);
            var database = await _adminAccountMetaRepository.BeginTransactionAsync();
            using (database)
            {
                try
                {
                    if (acc.Id > 0)
                    {
                        var randomString = StringExtension.RandomString(8);
                        var adminAccountMeta = new AdminAccountMeta()
                        {
                            Id = 0,
                            Active = true,
                            Name = acc.UserName,
                            CreatedTime = DateTime.Now,
                            AdminAccountId = acc.Id,
                            Key = AdminAccountMetaConstants.FORGOT_PASSWORD_EMAIL_KEY,
                            Value = randomString,
                            Description = (acc.Email + "-" + randomString).ToHash256()
                        };

                        // thêm vào bảng AdminAccountMeta 
                        await _adminAccountMetaRepository.CreateAsync(adminAccountMeta);
                        // lưu thay đổi trong bảng AdminAccountMeta
                        await _adminAccountMetaRepository.SaveChangesAsync();
                        if (adminAccountMeta.Id > 0)
                        {
                            // setup email 
                            var message = SendEmailForgotPasswordAsync(acc, adminAccountMeta);
                            // hoàn thành transaction
                            await _adminAccountMetaRepository.EndTransactionAsync();
                            return (BestCVResponse.Success(), message);
                        }
                        else
                        {
                            await _adminAccountMetaRepository.RollbackTransactionAsync();
                            return (BestCVResponse.Error("Email chưa kích hoạt. "), null);
                        }
                    }
                    else
                    {
                        await _adminAccountMetaRepository.RollbackTransactionAsync();
                        return (BestCVResponse.Error("Email chưa kích hoạt."), null);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Có lỗi khi gửi email");
                    return (BestCVResponse.Error(), null);
                }
            }

        }
       
        public EmailMessage<ForgotPasswordEmailBody> SendEmailForgotPasswordAsync(AdminAccount acc, AdminAccountMeta accMeta)
        {
            var host = _config["SectionUrls:AdminVerifiedForgotPasswordNotificationPage"];

            var message = new EmailMessage<ForgotPasswordEmailBody>()
            {
                ToEmails = new List<string> { acc.Email },
                CcEmails = new List<string> { },
                BccEmails = new List<string> { },
                Subject = "Thay đổi mật khẩu tài khoản Admin hệ thống Jobi",
                Model = new ForgotPasswordEmailBody()
                {
                    Fullname = acc.FullName,
                    ActiveLink = $"{host}/{accMeta.Value}/{accMeta.Description}",
                    Time = DateTime.Now.Year,
                    validExpried = AdminAccountMetaConstants.FORGOT_PASSWORD_SENT_EMAIL_TIME_LIMIT,

                }
            };
            return message;
        }
       

        public async Task<int> CountResetPassword(long AdminAccountId)
        {
            return await _adminAccountMetaRepository.CountResetPassword(AdminAccountId);
        }
      
        public async Task<bool> CheckEmailIsActive(string email)
        {
            return await _adminAccountRepository.CheckEmailIsActive(email);
        }
      
        public async Task<AdminAccount?> GetByEmailAsync(string email)
        {
            return await _adminAccountRepository.FindByEmail(email);
        }
       
        public async Task<bool> CheckKeyValid(string code, string hash)
        {
            var result = false;
            var dataMeta = await _adminAccountMetaRepository.CheckVerifyCode(code, hash);
            if (dataMeta != null)
            {
                var time = DateTime.Now - dataMeta.CreatedTime;
                if (time < TimeSpan.FromHours(AdminAccountMetaConstants.FORGOT_PASSWORD_TOKEN_EXPRIED))
                {
                    result = true;
                }
            }
            return result;
        }
      
        public async Task<BestCVResponse> ResetNewPassword(string code, string hash, string password)
        {
            var dataMeta = await _adminAccountMetaRepository.CheckVerifyCode(code, hash);
            var adminAccount = await _adminAccountRepository.GetByIdAsync(dataMeta.AdminAccountId);
            adminAccount.Password = password.ToHash256();
            await _adminAccountRepository.UpdateAsync(adminAccount);
            await _adminAccountRepository.SaveChangesAsync();
            dataMeta.Active = false;
            await _adminAccountMetaRepository.UpdateAsync(dataMeta);
            await _adminAccountMetaRepository.SaveChangesAsync();
            return BestCVResponse.Success();
        }
        public async Task<bool> IsAdminRole (long id)
        {
            return await _adminAccountRoleRepository.IsAdminRole(id);
        }

    }
}
