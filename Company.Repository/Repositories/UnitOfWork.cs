using Company.Data.Contexts;
using Company.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public UnitOfWork(CompanyDbContext Context)
        {
            departmetRepository = new DepartmetRepository(Context);
            employeeRepository = new EmployeeRepository(Context);
            _context = Context;
        }
        public IDepartmetRepository departmetRepository { get ; set; }
        public IEmployeeRepository employeeRepository { get; set ; }

        public int Complete()=>_context.SaveChanges();
        
    }
}
