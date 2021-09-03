using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region DECLARE

        #endregion

        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ nhân viên trong db
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// CreateBy: LNT (27/08)
        public override IEnumerable<Employee> Get()
        {
            try
            {
                var employees = _dbConnection.Query<Employee>("Proc_GetEmployees", commandType: CommandType.StoredProcedure);
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Lọc danh sách nhân viên theo các tiêu chí và phân trang
        /// </summary>
        /// <param name="pageIndex">Index của trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi hiển thị trên 1 trang</param>
        /// <param name="keysearch">Mã nhân viên, Họ và tên, SĐT cần tìm kiếm</param>
        /// <returns>Danh sách các bản ghi theo điều kiện lọc</returns>
        /// CreateBy: LNT (27/08)
        public object GetByPaging(int pageIndex, int pageSize, string keysearch)
        {
            keysearch = keysearch == null ? string.Empty : keysearch;
            var param = new DynamicParameters();
            param.Add("@keysearch", keysearch);
            param.Add("@pageIndex", pageIndex);
            param.Add("@pageSize", pageSize);
            param.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@totalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employees = _dbConnection.Query<Employee>("Proc_GetEmployeesByPaging", param: param, commandType: CommandType.StoredProcedure);
            var totalPage = param.Get<int>("@totalPage");
            var toatalRecord = param.Get<int>("@totalRecord");

            var employeesFilter = new
            {
                TotalPage = totalPage,
                TotalRecord = toatalRecord,
                Data = employees,
            };
            return employeesFilter;
        }

        /// <summary>
        /// Tự sinh mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới dạng string</returns>
        /// CreateBy: LNT (27/08)
        public string NewCode()
        {
            // 3. lấy dữ liệu
            // lấy ra mã nhân viên trên cùng
            var topEmployeeCode = _dbConnection.QueryFirstOrDefault<string>("Proc_GetTopEmployeeCode", commandType: CommandType.StoredProcedure);
            var valueNewEmployeeCode = 0;
            if (topEmployeeCode != null)
            {
                // cắt chuỗi
                var valueEmployeeCode = int.Parse(topEmployeeCode.ToString().Split("-")[1]);
                // gán giá trị
                if (valueNewEmployeeCode < valueEmployeeCode)
                {
                    valueNewEmployeeCode = valueEmployeeCode;
                }
            }
            else
            {
                topEmployeeCode = "NV-0";
                // cắt chuỗi
                var valueEmployeeCode = int.Parse(topEmployeeCode.ToString().Split("-")[1]);
                // gán giá trị
                if (valueNewEmployeeCode < valueEmployeeCode)
                {
                    valueNewEmployeeCode = valueEmployeeCode;
                }
            }
            string newEmployeeCode = "NV-" + (valueNewEmployeeCode + 1);
            return newEmployeeCode;
        }
        #endregion
    }
}
