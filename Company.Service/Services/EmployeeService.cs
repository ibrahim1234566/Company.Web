using AutoMapper;
using Company.Data.Models;
using Company.Repository.interfaces;
using Company.Repository.Repositories;
using Company.Service.Helper;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Employee.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(EmployeeDto employeeDto)
        {
            /*Employee employee = new Employee()
            {
                Address = employeeDto.Address,
                Age = employeeDto.Age,
                DepartmentId = employeeDto.DepartmentId,
                Email = employeeDto.Email,
                HiringDate = employeeDto.HiringDate,
                ImgeUrl = employeeDto.ImgeUrl,
                Name = employeeDto.Name,
                Salary = employeeDto.Salary,
                PhoneNumber = employeeDto.PhoneNumber  

            };*/
            employeeDto.ImgeUrl = DocumentSettings.UploadFile(employeeDto.Imge,"Images");
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.employeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {
            /* Employee employee = new Employee()
             {
                 Address = employeeDto.Address,
                 Age = employeeDto.Age,
                 DepartmentId = employeeDto.DepartmentId,
                 Email = employeeDto.Email,
                 HiringDate = employeeDto.HiringDate,
                 ImgeUrl = employeeDto.ImgeUrl,
                 Name = employeeDto.Name,
                 Salary = employeeDto.Salary,
                 PhoneNumber = employeeDto.PhoneNumber

             };*/
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.employeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var emp = _unitOfWork.employeeRepository.GetAll();
           /* var MappedEmployee =emp.Select(x=>new EmployeeDto
            {
               DepartmentId = x.DepartmentId,
               Address = x.Address,
               Salary = x.Salary,
               PhoneNumber = x.PhoneNumber,
               Name = x.Name,
               HiringDate= x.HiringDate,
               Age= x.Age,
               CreatedAt = x.CreatedAt,
               ImgeUrl=x.ImgeUrl,              


            });*/
           IEnumerable <EmployeeDto> MappedEmployee =_mapper.Map <IEnumerable<EmployeeDto>>(emp);

            return MappedEmployee;
        }

        public EmployeeDto GetById(int? id)
        {
            if (id is null)
            {
                return null;
            }
            var emp = _unitOfWork.employeeRepository.GetById(id.Value);
            if (emp is null)
            {

                return null;
            }
            /* EmployeeDto employeeDto = new EmployeeDto()
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
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(emp);
            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name) 
        {
            IEnumerable <Employee>  emp =_unitOfWork.employeeRepository.GetEmployeeByName(name);
            /* IEnumerable  <EmployeeDto> employeeDto = new EmployeeDto()
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
            IEnumerable<EmployeeDto> MappedEmployee = _mapper.Map<IEnumerable<EmployeeDto>>(emp);

            return MappedEmployee;


        }
       

        //public void Update(EmployeeDto employee)
        //{
        //    _unitOfWork.employeeRepository.Update(employee);
        //    _unitOfWork.Complete();
        //}
    }
}
