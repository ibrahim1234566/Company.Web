using Company.Data.Models;
using Company.Repository.interfaces;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmetRepository _departmetRepository;

        public DepartmentService(IDepartmetRepository departmetRepository)
        {
            _departmetRepository = departmetRepository;
        }
        public void Add(Department department)
        {
            _departmetRepository.Add(department);
        }

        public void Delete(Department department)
        {
            _departmetRepository.Delete(department);
        }

        public IEnumerable<Department> GetAll()
        {
           var dept = _departmetRepository.GetAll();
            return dept;
        }

        public Department GetById(int? id)
        {
            if(id is null)
            {
                return null;
            }
            var dept = _departmetRepository.GetById(id.Value);
            if (dept is null)
            {

                return null;
            }
            return dept;
        }

        public void Update(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
