using AutoMapper;
using BestCV.Application.Models.Company;
using BestCV.Application.Models.Employer;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Entities;
using BestCV.Core.Utilities;
using BestCV.Domain.Aggregates.Company;
using BestCV.Domain.Aggregates.Employer;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Implement;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployerRepository employerRepository;
        private readonly ILogger<CompanyService> logger;
        private readonly IMapper mapper;
        private static IDictionary<string, byte[]> _companyfiles = new Dictionary<string, byte[]>();


        public CompanyService(
            ICompanyRepository _CompanyRepository,
            IEmployerRepository _employerRepository,
            ILoggerFactory _loggerFactory,
            IMapper _mapper)
        {
            companyRepository = _CompanyRepository;
            employerRepository = _employerRepository;
            logger = _loggerFactory.CreateLogger<CompanyService>();
            mapper = _mapper;
        }

        public async Task<BestCVResponse> CreateAsync(InsertCompanyDTO obj)
        {
            var isNameExist = await companyRepository.IsNameExist(obj.Name.Trim());
            if (isNameExist)
            {
                return BestCVResponse.Error("Tên đã tồn tại.");

            }
            var newObj = mapper.Map<Company>(obj);
            newObj.Id = 0;
            newObj.CreatedTime = DateTime.Now;
            newObj.Active = true;
            newObj.Description = obj.Description.ToEscape();
            newObj.Search = obj.Name + "_" + obj.EmailAddress + "_" +
                            obj.Phone + "_" + obj.Website + "_" +
                            obj.TaxCode + "_" + obj.FoundedIn + "_" +
                            obj.CompanySizeId + "_" + obj.FacebookLink + "_" +
                            obj.TwitterLink + "_" + obj.LinkedinLink + "_" +
                            obj.TiktokLink + "_" + obj.YoutubeLink;

            await companyRepository.CreateAsync(newObj);
            var result = await companyRepository.SaveChangesAsync();
            if (result > 0)
            {
                return BestCVResponse.Success(newObj);

            }
            else
            {
                return BestCVResponse.Error("Thêm mới công ty không thành công.");
            }
        }

        public Task<BestCVResponse> CreateListAsync(IEnumerable<InsertCompanyDTO> objs)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> GetByIdAsync(int id)
        {
            var data = await companyRepository.GetByIdAsync(id);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            var result = mapper.Map<CompanyDTO>(data);

            return BestCVResponse.Success(result);
        }

        public async Task<BestCVResponse> GetDetailByEmnployerId(long CompanyId)
        {
            var data = await companyRepository.GetDetailByEmnployerId(CompanyId);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", data);
            }
            var model = mapper.Map<CompanyDTO>(data);
            return BestCVResponse.Success(model);
        }

        public Task<bool> IsNameExist(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<object> ListCompanyAggregate(DTParameters parameters)
        {
            return await companyRepository.ListCompanyAggregates(parameters);
        }

        public Task<BestCVResponse> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BestCVResponse> SoftDeleteListAsync(IEnumerable<int> objs)
        {
            throw new NotImplementedException();
        }


        public async Task<BestCVResponse> UpdateAsync(UpdateCompanyDTO obj)
        {
            var company = await companyRepository.GetDetailByEmnployerId(obj.EmployerId);
            if (company == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu. ", obj);
            }
            var model = mapper.Map(obj, company);
            await companyRepository.UpdateAsync(model);
            await companyRepository.SaveChangesAsync();
            return BestCVResponse.Success(model);
        }

        public Task<BestCVResponse> UpdateListAsync(IEnumerable<UpdateCompanyDTO> obj)
        {
            throw new NotImplementedException();
        }

        public async Task<BestCVResponse> DetailAdmin(int id)
        {
            var company = await companyRepository.GetByIdAsync(id);
            var companyDetail = mapper.Map<CompanyAdminDTO>(company);
            var employer = await employerRepository.GetByIdAsync(companyDetail.EmployerId);

            companyDetail.Employer = mapper.Map<EmployerDetailDTO>(employer);
            return BestCVResponse.Success(companyDetail);
        }


        public async Task<BestCVResponse> ExportExcel(List<CompanyAggregates> data)
        {
            if (data.Count > 0)
            {
                var fileName = "DS_To_Chuc_Tuyen_Dung_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xlsx";
                XSSFWorkbook wb = new XSSFWorkbook();
                // Tạo ra 1 sheet
                ISheet sheet = wb.CreateSheet();
                sheet.DisplayGridlines = false;
                var rowTitle = sheet.CreateRow(0);
                CellRangeAddress regionTitle = new CellRangeAddress(0, 1, 0, 7);
                sheet.AddMergedRegion(regionTitle);
                ICell cellTitle = rowTitle.CreateCell(0);
                cellTitle.SetCellValue("DANH SÁCH TỔ CHỨC TUYỂN DỤNG");
                // Ghi tiêu đề cột ở row 1
                var row1 = sheet.CreateRow(2);
                ICell cell = row1.CreateCell(0);
                cell.SetCellValue("STT");
                ICell cell1 = row1.CreateCell(1);
                cell1.SetCellValue("TÊN TỔ CHỨC");
                ICell cell2 = row1.CreateCell(2);
                cell2.SetCellValue("MÃ SỐ THUẾ");
                ICell cell3 = row1.CreateCell(3);
                cell3.SetCellValue("ĐỊA CHỈ");
                ICell cell4 = row1.CreateCell(4);
                cell4.SetCellValue("WEBSITE");
                ICell cell5 = row1.CreateCell(5);
                cell5.SetCellValue("QUY MÔ");
                ICell cell6 = row1.CreateCell(6);
                cell6.SetCellValue("NĂM THÀNH LẬP");

                ICell cell7 = row1.CreateCell(7);
                cell7.SetCellValue("NGÀY TẠO");
                //style row title
                ICellStyle styleTitle = wb.CreateCellStyle();
                styleTitle.Alignment = HorizontalAlignment.Center;
                styleTitle.VerticalAlignment = VerticalAlignment.Center;
                //style border 
                ICellStyle styleBorder = wb.CreateCellStyle();
                styleBorder.BorderBottom = BorderStyle.Thin;
                styleBorder.BottomBorderColor = HSSFColor.Black.Index;
                styleBorder.BorderTop = BorderStyle.Thin;
                styleBorder.TopBorderColor = HSSFColor.Black.Index;
                styleBorder.BorderLeft = BorderStyle.Thin;
                styleBorder.LeftBorderColor = HSSFColor.Black.Index;
                styleBorder.BorderRight = BorderStyle.Thin;
                styleBorder.RightBorderColor = HSSFColor.Black.Index;
                //style stt
                ICellStyle styleStt = wb.CreateCellStyle();
                styleStt.Alignment = HorizontalAlignment.Center;
                styleStt.BorderBottom = BorderStyle.Thin;
                styleStt.BottomBorderColor = HSSFColor.Black.Index;
                styleStt.BorderTop = BorderStyle.Thin;
                styleStt.TopBorderColor = HSSFColor.Black.Index;
                styleStt.BorderLeft = BorderStyle.Thin;
                styleStt.LeftBorderColor = HSSFColor.Black.Index;
                styleStt.BorderRight = BorderStyle.Thin;
                styleStt.RightBorderColor = HSSFColor.Black.Index;
                //style row 1
                ICellStyle styleRow1 = wb.CreateCellStyle();
                styleRow1.FillForegroundColor = HSSFColor.PaleBlue.Index;
                styleRow1.FillPattern = FillPattern.SolidForeground;
                styleRow1.BorderBottom = BorderStyle.Thin;
                styleRow1.BottomBorderColor = HSSFColor.Black.Index;
                styleRow1.BorderTop = BorderStyle.Thin;
                styleRow1.TopBorderColor = HSSFColor.Black.Index;
                styleRow1.BorderLeft = BorderStyle.Thin;
                styleRow1.LeftBorderColor = HSSFColor.Black.Index;
                styleRow1.BorderRight = BorderStyle.Thin;
                styleRow1.RightBorderColor = HSSFColor.Black.Index;
                //font
                var boldFont = wb.CreateFont();
                boldFont.IsBold = true;
                styleRow1.SetFont(boldFont);
                var boldFontTitle = wb.CreateFont();
                boldFontTitle.IsBold = true;
                boldFontTitle.FontHeightInPoints = 18;
                styleTitle.SetFont(boldFontTitle);
                //set style row 1row1
                cell.CellStyle = styleRow1;
                cell1.CellStyle = styleRow1;
                cell2.CellStyle = styleRow1;
                cell3.CellStyle = styleRow1;
                cell4.CellStyle = styleRow1;
                cell5.CellStyle = styleRow1;
                cell6.CellStyle = styleRow1;
                cell7.CellStyle = styleRow1;
                cellTitle.CellStyle = styleTitle;
                //style date time 
                IDataFormat dataDateFormatCustom = wb.CreateDataFormat();
                ICellStyle styleDateTime = wb.CreateCellStyle();
                styleDateTime.DataFormat = dataDateFormatCustom.GetFormat("yyyy-MM-dd HH:mm:ss");
                styleDateTime.BorderBottom = BorderStyle.Thin;
                styleDateTime.BottomBorderColor = HSSFColor.Black.Index;
                styleDateTime.BorderTop = BorderStyle.Thin;
                styleDateTime.TopBorderColor = HSSFColor.Black.Index;
                styleDateTime.BorderLeft = BorderStyle.Thin;
                styleDateTime.LeftBorderColor = HSSFColor.Black.Index;
                styleDateTime.BorderRight = BorderStyle.Thin;
                styleDateTime.RightBorderColor = HSSFColor.Black.Index;
                //ghi dữ liệu
                int rowIndex = 3;
                int sttCount = 1;
                foreach (var item in data)
                {
                    var newRow = sheet.CreateRow(rowIndex);
                    var countNumberCell = newRow.CreateCell(0);
                    countNumberCell.SetCellValue(sttCount);
                    countNumberCell.CellStyle = styleStt;
                    var fullNameCell = newRow.CreateCell(1);
                    fullNameCell.SetCellValue(item.Name);
                    fullNameCell.CellStyle = styleBorder;
                    var userNameCell = newRow.CreateCell(2);
                    userNameCell.SetCellValue(item.TaxCode);
                    userNameCell.CellStyle = styleBorder;
                    var levelCell = newRow.CreateCell(3);
                    levelCell.SetCellValue(item.AddressDetail);
                    levelCell.CellStyle = styleBorder;
                    var statusCell = newRow.CreateCell(4);
                    statusCell.SetCellValue(item.Website);
                    statusCell.CellStyle = styleBorder;
                    var sizeCell = newRow.CreateCell(5);
                    sizeCell.SetCellValue(item.CompanySizeName);
                    sizeCell.CellStyle = styleBorder;
                    var foundedCell = newRow.CreateCell(6);
                    foundedCell.SetCellValue(item.FoundedIn);
                    foundedCell.CellStyle = styleBorder;
                    var createdTimeCell = newRow.CreateCell(7);
                    createdTimeCell.SetCellValue(item.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss"));
                    createdTimeCell.CellStyle = styleDateTime;

                    sttCount++;
                    rowIndex++;
                }
                var rowSign = sheet.CreateRow(rowIndex + 1);
                CellRangeAddress regionSign = new CellRangeAddress(rowIndex + 1, rowIndex + 1, 6, 7);
                sheet.AddMergedRegion(regionSign);
                ICell cellSign = rowSign.CreateCell(6);
                cellSign.SetCellValue("Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year);
                ICellStyle styleSign = wb.CreateCellStyle();
                styleSign.VerticalAlignment = VerticalAlignment.Center;
                styleSign.Alignment = HorizontalAlignment.Center;
                cellSign.CellStyle = styleSign;
                //auto size column
                sheet.AutoSizeColumn(0);
                for (int i = 1; i <= 7; i++)
                {
                    sheet.AutoSizeColumn(i);
                    sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + sheet.GetColumnWidth(i) / 2);
                }
                string handle = "ListCompanyExport" + DateTime.Now.Ticks.ToString();
                using (var memory = new MemoryStream())
                {
                    wb.Write(memory);
                    var byte_array = memory.ToArray();
                    //TempData[handle] = byte_array;
                    _companyfiles[handle] = byte_array;
                }
                var obj = new
                {
                    FileGuid = handle,
                    FileName = fileName,
                };
                return BestCVResponse.Success(obj);
            }
            else
            {
                return BestCVResponse.BadRequest("Failed to export Excel");
            }
        }

        public byte[] DownloadExcel(string fileGuid, string fileName)
        {
            if (_companyfiles.ContainsKey(fileGuid))
            {
                var data = _companyfiles[fileGuid];
                _companyfiles.Remove(fileGuid);
                return data;
            }
            else
            {
                return null;
            }
        }


        public async Task<BestCVResponse> GetCompanyAggregatesById(int companyId)
        {
            var data = await companyRepository.GetCompanyAggregatesById(companyId);
            if (data == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }

            return BestCVResponse.Success(data);
        }

 
        public async Task<BestCVResponse> SearchCompanyHomePageAsync(SearchingCompanyParameters parameter)
        {
            var data = await companyRepository.SearchCompanyHomePageAsync(parameter);
            if (data.DataSource == null)
            {
                return BestCVResponse.NotFound("Không có dữ liệu", data);
            }
            return BestCVResponse.Success(data);
        }
    }
}
