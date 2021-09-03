using MISA.AMIS.Core.MISAEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Kết quả khi xử lý nghiệp vụ
    /// CreatedBy: LNT (25/08)
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// Kết quả nhận vào
        /// CreatedBy: LNT (25/08)
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Thông điệp nhận vào
        /// CreatedBy: LNT (25/08)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Check sự hợp lệ
        /// CreatedBy: LNT (25/08)
        /// </summary>
        public EnumServiceResult MISACode { get; set; }

    }
}
