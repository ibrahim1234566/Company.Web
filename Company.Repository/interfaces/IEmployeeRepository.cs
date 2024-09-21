using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
       /* Employee GetById(int id);
        IEnumerable<Employee> GetAll();
        void Add(Employee employee);  
        void Update(Employee employee);
        void Delete(Employee employee);*/
    }
}
