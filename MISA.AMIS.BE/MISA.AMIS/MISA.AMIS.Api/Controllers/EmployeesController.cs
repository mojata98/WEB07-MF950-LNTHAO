using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Api.Resources;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region Declare
        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private const string _excelName = "Danh_sach_nhan_vien.xlsx";
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment) : base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tự sinh mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới dạng string</returns>
        /// CreateBy: LNT(27/08)
        [HttpGet("NewEmployeeCode")]
        public IActionResult AutoNewEmployeeCode()
        {
            try
            {
                var newEmployeeCode = _employeeRepository.NewCode();
                return StatusCode(200, newEmployeeCode);
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceApi.Exception_ErrorMsg,
                };
                return StatusCode(500, msg);
            }
        }

        /// <summary>
        /// Lọc danh sách nhân viên theo các tiêu chí và phân trang
        /// </summary>
        /// <param name="pageIndex">Index của trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi hiển thị trên 1 trang</param>
        /// <param name="keysearch">Mã nhân viên, Họ và tên, SĐT cần tìm kiếm</param>
        /// <returns>Danh sách các bản ghi theo điều kiện lọc</returns>
        /// CreateBy: LNT(14/08)
        [HttpGet("Filter")]
        public IActionResult GetEmployeesPaging(int pageIndex, int pageSize, string keysearch)
        {
            try
            {
                var employeesFilter = _employeeRepository.GetByPaging(pageIndex, pageSize, keysearch);
                if (employeesFilter != null)
                {
                    return StatusCode(200, employeesFilter);
                }
                else
                {
                    var msg = new
                    {
                        userMsg = ResourceApi.User_ErrorMsg_NoContent,
                    };
                    return StatusCode(204, msg);
                }
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceApi.Exception_ErrorMsg,
                };
                return StatusCode(500, msg);
            }
        }

        /// <summary>
        /// Xuất khẩu dữ liệu nhân viên
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : LNT(27/08)
        [HttpGet("Export")]
        public IActionResult ExportEmployee()
        {
            try
            {
                // đường dẫn đến wwwroot
                string folder = _hostingEnvironment.WebRootPath;
                // file download
                string urlDownload = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, _excelName);
                // tạo file với đường dẫn wwwroot, tên là danh_sach_nhan_vien
                var file = new FileInfo(Path.Combine(folder, _excelName));
                if (file.Exists)
                {
                    // xóa file cũ nếu đã tồn tại
                    file.Delete();
                    // khởi tạo ra file mới tên là danh_sach_nhan_vien
                    file = new FileInfo(Path.Combine(folder, _excelName));
                }
                var serviceResult = _employeeService.ExportEmployees(folder);
                serviceResult.Data = urlDownload;
                if (serviceResult.MISACode == Core.MISAEnum.EnumServiceResult.Success)
                {
                    return StatusCode(200, serviceResult);
                }
                else
                {
                    var msg = new
                    {
                        userMsg = ResourceApi.User_ErrorMsg_NoContent,
                    };
                    return StatusCode(204, msg);
                }
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceApi.Exception_ErrorMsg,
                };
                return StatusCode(500, msg);
            }
        }
        #endregion
    }
}
