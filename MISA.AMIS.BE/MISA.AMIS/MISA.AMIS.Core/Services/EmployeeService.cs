using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using MISA.AMIS.Core.Resources;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _serviceResult = new ServiceResult();
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xuất dữ liệu ra file Excell
        /// </summary>
        /// <param name="folder">folder chứa dữ liệu xuất khẩu</param>
        /// <returns>ServiceResult - lưu trạng thái kết quả sau khi xử lý nghiệp vụ và thao tác với db</returns>
        /// CreatedBy: LNT (02/09)
        public ServiceResult ExportEmployees(string folder)
        {
            try
            {
                // tạo file với đường dẫn wwwroot, tên là danh_sach_nhan_vien
                var file = new FileInfo(Path.Combine(folder, "Danh_sach_nhan_vien.xlsx"));

                using (var package = new ExcelPackage(file))
                {
                    // tên worksheet
                    var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");

                    // custom Title
                    workSheet.Cells["A1:I1"].Merge = true;
                    workSheet.Cells["A1:I1"].Value = "DANH SÁCH NHÂN VIÊN";
                    workSheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells["A1:I1"].Style.Font.Name = "Arial";
                    workSheet.Cells["A1:I1"].Style.Font.Size = 16;
                    workSheet.Cells["A1:I1"].Style.Font.Bold = true;
                    workSheet.Cells["A2:I2"].Merge = true;

                    // custom thead bảng excel
                    workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(3).Style.Font.Bold = true;
                    workSheet.Row(3).Style.Font.Name = "Arial";
                    workSheet.Row(3).Style.Font.Size = 10;

                    // set color cho thead
                    workSheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#d8d8d8"));

                    // thead
                    workSheet.Cells[3, 1].Value = "STT";
                    workSheet.Cells[3, 2].Value = "Mã nhân viên";
                    workSheet.Cells[3, 3].Value = "Tên nhân viên";
                    workSheet.Cells[3, 4].Value = "Giới tính";
                    workSheet.Cells[3, 5].Value = "Ngày sinh";
                    workSheet.Cells[3, 6].Value = "Chức danh";
                    workSheet.Cells[3, 7].Value = "Tên đơn vị";
                    workSheet.Cells[3, 8].Value = "Số tài khoản";
                    workSheet.Cells[3, 9].Value = "Tên ngân hàng";

                    // lấy danh sách nhân viên
                    var employees = _employeeRepository.Get();
                    int row = 4;
                    int count = 1;
                    foreach (var employee in employees)
                    {
                        // bind data lên column
                        workSheet.Cells[row, 1].Value = count;
                        workSheet.Cells[row, 2].Value = employee.EmployeeCode;
                        workSheet.Cells[row, 3].Value = employee.FullName;
                        workSheet.Cells[row, 4].Value = employee.GenderName;
                        workSheet.Cells[row, 5].Value = Convert.ToDateTime(employee.DateOfBirth).ToString("dd/MM/yyyy");
                        workSheet.Cells[row, 6].Value = employee.PositionName;
                        workSheet.Cells[row, 7].Value = employee.DepartmentName;
                        workSheet.Cells[row, 8].Value = employee.BankCode;
                        workSheet.Cells[row, 9].Value = employee.BankName;

                        // custom column stt và column dob
                        workSheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                        // custom cỡ chữ cho row
                        workSheet.Row(row).Style.Font.Name = "Times New Roman";
                        workSheet.Row(row).Style.Font.Size = 11;
                        row++;
                        count++;
                    }

                    // length file excel
                    string modelRange = "A3:I" + (employees.Count() + 3).ToString();
                    var modelTable = workSheet.Cells[modelRange];

                    // thêm border
                    modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    // fix width cho từng column
                    workSheet.Column(1).AutoFit();
                    workSheet.Column(2).AutoFit();
                    workSheet.Column(3).AutoFit();
                    workSheet.Column(4).AutoFit();
                    workSheet.Column(5).AutoFit();
                    workSheet.Column(6).AutoFit();
                    workSheet.Column(7).AutoFit();
                    workSheet.Column(8).AutoFit();
                    workSheet.Column(9).AutoFit();
                    package.Save();
                }
                _serviceResult.MISACode = MISAEnum.EnumServiceResult.Success;
                _serviceResult.Message = ResourceCore.Success_Export_Employee;
                return _serviceResult;
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceCore.Exception_ErrorMsg,
                };
                _serviceResult.Data = msg;
                _serviceResult.MISACode = MISAEnum.EnumServiceResult.InternalServerError;
                return _serviceResult;
            }
        }
        #endregion
    }
}

