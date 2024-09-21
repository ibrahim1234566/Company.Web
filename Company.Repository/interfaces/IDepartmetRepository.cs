using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.interfaces
{
    public interface IDepartmetRepository:IGenericRepository<Department>
    {
       /* Department GetById(int id);
        IEnumerable<Department> GetAll();
        void Add(Department department);
        void Update(Department department);
        void Delete(Department department);*/
    }
}
