using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context):base(context)
        {
           _context = context;
        }

       /* public IEnumerable<Employee> GetEmployeeByAddress(sbyte address)
        {
            throw new NotImplementedException();
        }*/

        public IEnumerable<Employee> GetEmployeeByName(string name) 
            => _context.Employees.Where(x=>
            x.Name.Trim().ToLower().Contains(name.Trim().ToLower())||
             x.Email.Trim().ToLower().Contains(name.Trim().ToLower()) ||
             x.PhoneNumber.Trim().ToLower().Contains(name.Trim().ToLower()) 

            ).ToList();
        
        /* public void Add(Employee employee)=>_context.Add(employee);


public void Delete(Employee employee) => _context.Remove(employee); 



public IEnumerable<Employee> GetAll() => _context.Employees.ToList();



public Employee GetById(int id) => _context.Employees.FirstOrDefault(x=>x.Id==id);




public void Update(Employee employee) => _context.Update(employee);*/


    }
}
