using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy ra tất cả thông tin của đối tượng trong database
        /// </summary>
        /// <returns>Danh sách đối tượng trong db</returns>
        /// CreatedBy: LNT (27/08)
        public IEnumerable<TEntity> Get();

        /// <summary>
        /// Lấy ra thông tin của 1 đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID của đối tượng muốn lấy</param>
        /// <returns>Thông tin đối tượng muốn tìm theo Id</returns>
        /// CreatedBy: LNT (27/08)
        public TEntity GetById(Guid entityId);

        /// <summary>
        /// Thêm mới 1 đối tượng vào trong db
        /// </summary>
        /// <param name="entity">object muốn thêm</param>
        /// <returns>Số bản ghi được thêm vào db</returns>
        /// CreatedBy: LNT (27/08)
        public int Add(TEntity entity);

        /// <summary>
        /// Cập nhật thông tin đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID đối tượng muốn cập nhật</param>
        /// <param name="entity">Dữ liệu muốn cập nhật</param>
        /// <returns>Số bản ghi đối tượng được sửa trong db</returns>
        /// CreatedBy: LNT (27/08)
        public int Update(TEntity entity, Guid entityId);

        /// <summary>
        /// Xóa 1 đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID đối tượng muốn xóa</param>
        /// <returns>Số bản ghi đối tượng được xóa trong db</returns>
        /// CreatedBy: LNT (27/08)
        public int Delete(Guid entityId);

        /// <summary>
        /// Lấy ra đối tượng theo Property
        /// </summary>
        /// <param name="entity">Đối tượng muôn lấy</param>
        /// <param name="property">Property muốn tìm kiếm</param>
        /// <returns>Đối tượng được lấy ra theo điều kiện Property</returns>
        /// CreatedBy: LNT (27/08)
        public TEntity GetByProperty(TEntity entity, PropertyInfo property);
    }
}
