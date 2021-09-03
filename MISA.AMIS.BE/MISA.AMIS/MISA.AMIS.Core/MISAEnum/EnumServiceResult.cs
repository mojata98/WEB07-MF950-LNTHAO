using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.MISAEnum
{
    /// <summary>
    /// Trạng thái phản hồi khi request
    /// </summary>
    public enum EnumServiceResult
    {
        /// <summary>
        /// Thao tác thành côn
        /// </summary>
        Success = 200,

        /// <summary>
        /// Thêm thành công
        /// </summary>
        Created = 201,

        /// <summary>
        /// Không có dữ liệu
        /// </summary>
        NoContent = 204,

        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Không được cấp quyền
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// Bị cấm truy cập
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// Không tìm thấy trang
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Lỗi bên phía server
        /// </summary>
        InternalServerError = 500
    }
}
