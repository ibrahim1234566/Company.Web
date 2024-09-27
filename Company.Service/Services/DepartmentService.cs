using AutoMapper;
using Company.Data.Models;
using Company.Repository.interfaces;
using Company.Repository.Repositories;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Department.Dto;
using Company.Service.Interfaces.Employee.Dto;
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
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(DepartmentDto department)
        {
            /* var MappedDepartment = new Department
             {
                 Code = department.Code,
                 Name = department.Name,
                 CreatedAt = DateTime.Now,
             };*/
            //Employee employee = _mapper.Map<Employee>(employeeDto);
            Department department1 = _mapper.Map<Department>(department);
            _unitOfWork.departmetRepository.Add(department1);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto department)
        {
            Department department1 = _mapper.Map<Department>(department);
            _unitOfWork.departmetRepository.Delete(department1);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
        {
            //softDelete
           var dept = _unitOfWork.departmetRepository.GetAll()/*.Where(x=>x.IsDeleted==false)*/;
            /*var MappedDepatment = dept.Select(x => new DepartmentDto
            {
         Code=x.Code,   
         Name=x.Name,
         Id=x.Id,

            });*/
            //IEnumerable<DepartmentDto> MappedDepatment = _mapper.Map<IEnumerable<DepartmentDto>>(dept);
            return dept;
        }

        public DepartmentDto GetById(int? id)
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
            /* DepartmentDto departmetDto = new DepartmentDto()
             {
                 Address = emp.Address,
                 Age = emp.Age,
                 DepartmentId = emp.DepartmentId,
                 Email = emp.Email,
                 HiringDate = emp.HiringDate,
                 ImgeUrl = emp.ImgeUrl,
                 Name = emp.Name,
                 Salary = emp.Salary,
                 PhoneNumber = emp.PhoneNumber

             };*/
            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(dept);
            return departmentDto;
        }

       /* public void Update(DepartmentDto department)
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
        }*/
    }
}
