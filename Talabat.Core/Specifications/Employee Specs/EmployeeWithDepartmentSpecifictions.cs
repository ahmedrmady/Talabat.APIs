using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Employee_Specs
{
    public class EmployeeWithDepartmentSpecifictions:BaseSpecifications<Employee>
    {

        public EmployeeWithDepartmentSpecifictions():base()
        {
            Includes.Add(e => e.Department);
        }

        public EmployeeWithDepartmentSpecifictions(int id ) : base(e=>e.Id==id)
        {
            Includes.Add(e=>e.Department);
                
        }


    }
}
