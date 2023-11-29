using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications.Employee_Specs;

namespace Talabat.APIs.Controllers
{
 
    public class EmployeeController : BaseApiController
    {
        private readonly IGenericRepositry<Employee> employeeRepositry;

        public EmployeeController(IGenericRepositry<Employee> _employeeRepositry)
        {
            employeeRepositry = _employeeRepositry;
        }



        [HttpGet] // GET : /api/Employee
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var spec = new EmployeeWithDepartmentSpecifictions();
            var Employees =  await employeeRepositry.GetAllWithSpecAsync(spec);
            return Ok(Employees);
        }


        [HttpGet("{id}")]   // GET : /api/Employee/id

        public async Task<ActionResult<Employee>> Employee(int id )
        {
            var spec = new EmployeeWithDepartmentSpecifictions(id);
            var employee = await employeeRepositry.GetWithSpecAsync(spec);
            return Ok(employee);
        }

    }
}
