using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Tự sinh mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới dạng string</returns>
        /// CreatedBy: LNT (27/08)
        string NewCode();

        /// <summary>
        /// Lọc danh sách nhân viên theo các tiêu chí và phân trang
        /// </summary>
        /// <param name="pageIndex">Index của trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi hiển thị trên 1 trang</param>
        /// <param name="positionId">Id của vị trí cần tìm kiếm</param>
        /// <returns>Danh sách các bản ghi theo điều kiện lọc</returns>
        /// CreateBy: LNT (27/08)
        object GetByPaging(int pageIndex, int pageSize, string keysearch);

    }
}
