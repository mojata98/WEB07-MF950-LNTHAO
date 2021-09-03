using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseEntityController<Department>
    {
        #region Declare
        IBaseRepository<Department> _baseRepository;
        IBaseService<Department> _baseService;
        #endregion

        #region Constructor
        public DepartmentsController(IBaseRepository<Department> baseRepository, IBaseService<Department> baseService) : base(baseRepository, baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion
    }
}
