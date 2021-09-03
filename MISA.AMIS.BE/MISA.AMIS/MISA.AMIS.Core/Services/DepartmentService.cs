using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Declare
        ServiceResult _serviceResult;
        IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _serviceResult = new ServiceResult();
            _departmentRepository = departmentRepository;
        }
        #endregion


    }
}
