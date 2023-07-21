using Microsoft.AspNetCore.Mvc;


namespace AspT3.Controllers
{
    [ApiController]
    [Route("employee")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet] // Path = "/"
        public List<Employee> GetUsers()
        {
            using var context = new EmployeeDbContext();
            return context.Employee.ToList();
        }


        [HttpPost]
        public Object Create(Employee emp)
        {
            using var context = new EmployeeDbContext();
            context.Add(emp);
            int v = context.SaveChanges();

            return new { Message = "Inserted", Rows = v };
        }

        [HttpPut("{id}")]
        public Object Update(int id, Employee empReq)
        {
            using var context = new EmployeeDbContext();
            var employee = (from emp in context.Employee
                        where emp.Id == empReq.Id
                        select emp).FirstOrDefault();

            if (employee != null)
            {
                employee.Name = empReq.Name;
                employee.Email = empReq.Email;


                int v = context.SaveChanges();
                return new { Message = "Updated" };
            }
            return new { Message = "Not found" };
        }

        [HttpDelete("{id}")]
        public Object Delete(int id)
        {
            using var context = new EmployeeDbContext();
            var employee = (from emp in context.Employee
                        where emp.Id == id
                        select emp).FirstOrDefault();

            if (employee != null)
            {
                context.Remove(employee);
                context.SaveChanges();
                return new { Message = "Deleted" };
            }
            return new { Message = "Not found" };
        }

        [HttpGet("{id}")]
        public Employee? FindById(int id)
        {
            using var context = new EmployeeDbContext();
            return (from emp in context.Employee
                    where emp.Id == id
                    select emp).FirstOrDefault();
        }
    }
}