using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Department : Base
    {
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Code của phòng ban
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên của phòng ban
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mô tả phòng ban
        /// CreatedBy: LNT (25/08)
        /// </summary>
        public string Description { get; set; }
    }
}
