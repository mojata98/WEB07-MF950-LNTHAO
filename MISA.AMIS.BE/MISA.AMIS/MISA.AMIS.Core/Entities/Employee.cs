using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Employee : Base
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBy: LNT (25/08)
        //[CheckExist]
        [Name("ID nhân viên")]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// CreatedBy: LNT (25/08)
        [Required]
        [CheckExist]
        [Name("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// CreatedBy: LNT (25/08)
        [Required]
        [Name("Họ và tên")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public int? Gender { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        /// CreatedBy: LNT (25/08)
        [Required]
        [Name("Id phòng ban")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Số chứng minh thư
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public string PositionName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public string Address { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        /// CreatedBy: LNT (27/08)
        public string LandlineNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreatedBy: LNT (25/08)
        public string Email { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        /// CreatedBy: LNT (27/08)
        public string BankCode { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreatedBy: LNT (27/08)
        public string BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        /// CreatedBy: LNT (27/08)
        public string BankBranch { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreatedBy: LNT (25/08)
        [NotMap]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Code phòng ban
        /// </summary>
        /// CreatedBy: LNT (25/08)
        [NotMap]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên giới tính
        /// </summary>
        /// CreatedBy: LNT (25/08)
        [NotMap]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 0:
                        return "Nữ";
                    case 1:
                        return "Nam";
                    case 2:
                        return "Không xác định";
                    default:
                        return "";
                }
            }

            set { }
        }

    }
}
