using Company.Data.Models;
using Company.Repository.interfaces;
using Company.Repository.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Department department)
        {
            var MappedDepartment = new Department
            {
                Code = department.Code,
                Name = department.Name,
                CreatedAt = DateTime.Now,
            };
            _unitOfWork.departmetRepository.Add(MappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(Department department)
        {
            _unitOfWork.departmetRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
        {
            //softDelete
           var dept = _unitOfWork.departmetRepository.GetAll()/*.Where(x=>x.IsDeleted==false)*/;
            return dept;
        }

        public Department GetById(int? id)
        {
            if(id is null)
            {
                return null;
            }
            var dept = _unitOfWork.departmetRepository.GetById(id.Value);
            if (dept is null)
            {

                return null;
            }
            return dept;
        }

        public void Update(Department department)
        {
            var dept = GetById(department.Id);
            if (dept.Name != department.Name) 
            {
                if(GetAll().Any(x=>x.Name==department.Name))
                {
                    throw new Exception("DepartmentNameDublication");

                }
            
            }
            dept.Name = department.Name;
            dept.Code = department.Code;
            _unitOfWork.departmetRepository.Update(dept);
            _unitOfWork.Complete(); 
        }
    }
}
