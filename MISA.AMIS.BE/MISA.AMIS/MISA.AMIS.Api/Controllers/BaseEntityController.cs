using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Api.Resources;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using MISA.AMIS.Core.MISAEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {
        #region Declare
        IBaseRepository<TEntity> _baseRepository;
        IBaseService<TEntity> _baseService;
        #endregion

        #region Constructor
        public BaseEntityController(IBaseRepository<TEntity> baseRepository, IBaseService<TEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra dữ liệu của toàn bộ đối tượng trong db
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy:LNT(26/08)
        [HttpGet]
        public IActionResult GetEntites()
        {
            try
            {
                var employee = new Employee() { Address = "ha noi" };
                var entities = _baseRepository.Get();
                if (entities.Count() > 0)
                {

                    var msg = new
                    {
                        devMsg = ResourceApi.Dev_SuccessMsg,
                        userMsg = ResourceApi.User_SuccessMsg_GetData,
                        data = entities, 
                        statusCode = EnumServiceResult.Success
                    };
                    return StatusCode(200, msg);
                }
                else
                {
                    var msg = new
                    {
                        devMsg = ResourceApi.Dev_SuccessMsg_NoContent,
                        userMsg = ResourceApi.User_SuccessMsg_NoContent,
                        statusCode = EnumServiceResult.NoContent
                    };
                    return StatusCode(200, msg);
                }
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceApi.Exception_ErrorMsg,
                    statusCode = EnumServiceResult.InternalServerError
                };
                return StatusCode(500, msg);
            }
        }

        /// <summary>
        /// Lấy ra dữ liệu 1 đối tượng theo ID
        /// </summary>
        /// <param name="entityId">Id đối tượng muốn lấy ra</param>
        /// <returns>Đối tượng lấy theo Id</returns>
        /// CreatedBy: LNT(26/08)
        [HttpGet("{entityId}")]
        public IActionResult GetEntitesById(Guid entityId)
        {
            try
            {
                var entity = _baseRepository.GetById(entityId);
                if (entity != null)
                {
                    var msg = new
                    {
                        devMsg = ResourceApi.Dev_SuccessMsg,
                        userMsg = ResourceApi.User_SuccessMsg_GetData,
                        data = entity,
                        statusCode = EnumServiceResult.Success
                    };
                    return StatusCode(200, msg);
                }
                else
                {
                    var msg = new
                    {
                        devMsg = ResourceApi.Dev_SuccessMsg_NoContent,
                        userMsg = ResourceApi.User_SuccessMsg_NoContent,
                        statusCode = EnumServiceResult.NoContent
                    };
                    return StatusCode(200, msg);
                }
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceApi.Exception_ErrorMsg,
                    statusCode = EnumServiceResult.InternalServerError
                };
                return StatusCode(500, msg);
            }
        }

        /// <summary>
        /// Thêm 1 đối tượng vào db
        /// </summary>
        /// <param name="entity">dữ liệu về đối tượngmuốn thêm</param>
        /// <returns>Số bản ghi được thêm vào trong db</returns>
        /// CreatedBy: LNT (26/08)
        [HttpPost]
        public IActionResult InsertEntity(TEntity entity)
        {
            try
            {
                var serviceResult = _baseService.Add(entity);
                if (serviceResult.MISACode == Core.MISAEnum.EnumServiceResult.Created)
                {
                    var msg = new
                    {
                        devMsg = ResourceApi.Dev_SuccessMsg,
                        userMsg = ResourceApi.User_SuccessMsg_Add,
                        statusCode = EnumServiceResult.Created
                    };
                    return StatusCode(201, msg);
                }
                else
                {
                    var msg = new
                    {
                        devMsg = serviceResult.Message,
                        userMsg = serviceResult.Data,
                        statusCode = EnumServiceResult.BadRequest
                    };
                    return StatusCode(400, msg);
                }
            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceApi.Exception_ErrorMsg,
                    statusCode = EnumServiceResult.InternalServerError
                };
                return StatusCode(500, msg);
            }
        }

        /// <summary>
        /// Sửa thông tin theo ID
        /// </summary>
        /// <param name="entityId">ID của đối tượng muốn sửa</param>
        /// <param name="entity">Dữ liệu đối tượng muốn sửa</param>
        /// <returns>Số bản ghi được sửa trong db</returns>
        /// CreatedBy:LNT(26/08)
        [HttpPut("{entityId}")]
        public IActionResult UpdateEntity(TEntity entity, Guid entityId)
        {
            try
            {
                // check Id 
                var entityCurrent = _baseRepository.GetById(entityId);
                if (entityCurrent != null)
                {
                    var serviceResult = _baseService.Update(entity, entityId);
                    if (serviceResult.MISACode == Core.MISAEnum.EnumServiceResult.Success)
                    {
                        var msg = new
                        {
                            devMsg = ResourceApi.Dev_SuccessMsg,
                            userMsg = ResourceApi.User_SuccessMsg_Update,
                            rowUpdate = serviceResult.Data,
                            statusCode = EnumServiceResult.Success
                        };
                        return StatusCode(200, msg);
                    }
                    else
                    {
                        var msg = new
                        {
                            devMsg = serviceResult.Message,
                            userMsg = serviceResult.Data,
                            statusCode = EnumServiceResult.BadRequest
                        };
                        return StatusCode(400, msg);
                    }
                }
                else
                {
                    var msg = new
                    {
                        devMsg = ResourceApi.Dev_NotValid_IdEmployee,
                        userMsg = ResourceApi.NotValid_IdEmployee,
                        statusCode = EnumServiceResult.NotFound
                    };
                    return StatusCode(404, msg);
                }

            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceApi.Exception_ErrorMsg,
                    statusCode = EnumServiceResult.InternalServerError
                };
                return StatusCode(500, msg);
            }
        }

        /// <summary>
        /// Xóa 1 đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID của đối tượng</param>
        /// <returns>Số dòng được xóa trong db</returns>
        /// CreatedBy:LNT(26/08)
        [HttpDelete("{entityId}")]
        public IActionResult DeleteEntity(Guid entityId)
        {
            try
            {
                // check Id
                var entityCurrent = _baseRepository.GetById(entityId);
                if (entityCurrent != null)
                {
                    var result = _baseRepository.Delete(entityId);
                    if (result != 0)
                    {
                        var msg = new
                        {
                            devMsg = ResourceApi.Dev_SuccessMsg,
                            userMsg = ResourceApi.User_SuccessMsg_Delete,
                            rowUpdate = result,
                            statusCode = EnumServiceResult.Success
                        };
                        return StatusCode(200, msg);
                    }
                    else
                    {
                        var msg = new
                        {
                            devMsg = ResourceApi.Dev_SuccessMsg,
                            userMsg = ResourceApi.User_SuccessMsg_Update,
                            rowUpdate = result,
                            statusCode = EnumServiceResult.NoContent
                        };
                        return StatusCode(204, msg);
                    }
                }
                else
                {
                    var msg = new
                    {
                        devMsg = ResourceApi.Dev_NotValid_IdEmployee,
                        userMsg = ResourceApi.NotValid_IdEmployee,
                        statusCode = EnumServiceResult.InternalServerError
                    };
                    return StatusCode(500, msg);
                }

            }
            catch (Exception ex)
            {
                var msg = new
                {
                    devMsg = ex.Message,
                    userMsg = ResourceApi.Exception_ErrorMsg,
                    statusCode = EnumServiceResult.InternalServerError
                };
                return StatusCode(500, msg);
            }
        }
        #endregion
    }
}

