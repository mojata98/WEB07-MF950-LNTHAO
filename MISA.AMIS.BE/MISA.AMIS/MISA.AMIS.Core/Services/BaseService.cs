using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using MISA.AMIS.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Base
    {
        #region Declare
        IBaseRepository<TEntity> _baseRepository;
        ServiceResult _serviceResult;
        // mảng chứa error
        List<string> messageArr = new List<string>();
        // check vaidate
        bool isValid = true;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { MISACode = MISAEnum.EnumServiceResult.Success };
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lý nghiệp vụ việc thêm mới 1 đối tượng vào db
        /// </summary>
        /// <param name="entity">Đối tượng truyền vào</param>
        /// <returns>ServiceResult - lưu trạng thái kết quả sau khi xử lý nghiệp vụ và thao tác với db </returns>
        /// CreatedBy: LNT (27/08)
        public virtual ServiceResult Add(TEntity entity)
        {
            entity.EntityState = MISAEnum.EnumEntityState.Add;
            var isValid = Validate(entity);
            if (isValid == false)
            {
                return _serviceResult;
            }
            else
            {
                _serviceResult.Data = _baseRepository.Add(entity);
                _serviceResult.MISACode = MISAEnum.EnumServiceResult.Created;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Xử lý nghiệp vụ việc sửa thông tin 1 đối tượng vào db
        /// </summary>
        /// <param name="entity">Đối tượng truyền vào</param>
        /// <param name="entityId">Id của đối tượng truyền vào</param>
        /// <returns>ServiceResult - lưu trạng thái kết quả sau khi xử lý nghiệp vụ và thao tác với db </returns>
        /// CreatedBy: LNT (27/08)
        public virtual ServiceResult Update(TEntity entity, Guid entityId)
        {
            entity.EntityState = MISAEnum.EnumEntityState.Update;
            var isValid = Validate(entity);
            if (isValid == false)
            {
                return _serviceResult;
            }
            else
            {
                _serviceResult.Data = _baseRepository.Update(entity, entityId);
                _serviceResult.MISACode = MISAEnum.EnumServiceResult.Success;
                return _serviceResult;
            }
        }

        /// <summary>
        /// Hàm xử lý nghiệp vụ, validate data
        /// </summary>
        /// <param name="entity">Đối tượng truyền vào</param>
        /// <returns>IsValid: True or False</returns>
        /// CreatedBy: LNT (27/08)
        public bool Validate(TEntity entity)
        {
            // đọc các property
            var properties = entity.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(entity) == null ? null : prop.GetValue(entity);

                // lấy ra name của property
                var attrNames = prop.GetCustomAttributes(typeof(Name), true);
                var fieldName = string.Empty;
                if (attrNames.Length > 0)
                {
                    fieldName = (attrNames[0] as Name).FieldName;
                }

                // kiểm tra xem có attr cần phải validate k
                isValid = IsRequired(prop, propValue, fieldName);

                // Kiểm tra trùng dữ liệu
                isValid = IsExist(entity, prop, propValue, fieldName);
            }
            _serviceResult.Data = messageArr;
            return isValid;
        }

        /// <summary>
        /// Check required
        /// </summary>
        /// <param name="prop">Property của đối tượng</param>
        /// <param name="propValue"> Value của property</param>
        /// <param name="fieldName">Tên của property</param>
        /// <returns>isValid : True or False</returns>
        /// CreatedBy: LNT (27/08)
        public bool IsRequired(PropertyInfo prop, object propValue, string fieldName)
        {
            if (prop.IsDefined(typeof(Required), false))
            {
                if (string.IsNullOrEmpty(Convert.ToString(propValue)) || propValue == null)
                {
                    messageArr.Add(string.Format(ResourceCore.Error_Required, fieldName));
                    _serviceResult.MISACode = MISAEnum.EnumServiceResult.BadRequest;
                    _serviceResult.Message = ResourceCore.Error_Validate;
                    isValid = false;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Check trùng dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <param name="prop">Property của đối tượng</param>
        /// <param name="propValue">Value của property</param>
        /// <param name="fieldName">Tên của property</param>
        /// <returns>isValid : True or False</returns>
        /// CreatedBy: LNT (27/08)
        public bool IsExist(TEntity entity, PropertyInfo prop, object propValue, string fieldName)
        {
            if (prop.IsDefined(typeof(CheckExist), false))
            {
                var entityCheckExist = _baseRepository.GetByProperty(entity, prop);
                if (entityCheckExist != null)
                {
                    isValid = false;
                    messageArr.Add(string.Format(ResourceCore.Error_Exist, fieldName));
                    _serviceResult.MISACode = MISAEnum.EnumServiceResult.BadRequest;
                    _serviceResult.Message = ResourceCore.Error_Validate;
                }
            }
            return isValid;
        }
        #endregion
    }
}

