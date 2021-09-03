using MISA.AMIS.Core.MISAEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    #region Attribute
    /// <summary>
    /// Cờ check việc bắt buộc nhập
    /// CreatedBy: LNT (25/08)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }

    /// <summary>
    /// Cờ check việc trùng dữ liệu
    /// CreatedBy: LNT (25/08)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckExist : Attribute
    {

    }

    /// <summary>
    /// Cờ check khóa chính
    /// CreatedBy: LNT (25/08)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }

    /// <summary>
    /// Cờ check định dạng email
    /// CreatedBy: LNT (25/08)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckEmail : Attribute
    {

    }

    /// <summary>
    /// Cờ check việc map property
    /// CreatedBy: LNT (25/08)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMap : Attribute
    {

    }

    /// <summary>
    /// Name của property
    /// CreatedBy: LNT (25/08)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Name : Attribute
    {
        public string FieldName = string.Empty;
        public Name(string fieldName)
        {
            FieldName = fieldName;
        }
    }


    #endregion
    public class Base
    {
        #region Property
        /// <summary>
        /// Trạng thái của object
        /// CreatedBy: LNT (25/08)
        /// </summary>
        [NotMap]
        public EnumEntityState EntityState { get; set; } = EnumEntityState.Add;

        /// <summary>
        /// Người tạo bản ghi
        /// CreatedBy: LNT (25/08)
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo bản ghi
        /// CreatedBy: LNT (25/08)
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa gần nhất
        /// CreatedBy: LNT (25/08)
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa gần nhất
        /// CreatedBy: LNT (25/08)
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        #endregion
    }
}

