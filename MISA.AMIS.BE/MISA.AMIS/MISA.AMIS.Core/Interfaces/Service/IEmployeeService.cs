using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Xuất dữ liệu ra file Excell
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        /// CreatedBy: LNT (27/08)
        ServiceResult ExportEmployees(string folder);
    }
}
