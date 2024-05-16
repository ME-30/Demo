using AutoMapper;
using Demo.Core.DTO;
using Demo.EF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentDto>();
               
            CreateMap<Department, DepartmentWithEmpName>()
            .ForMember(d => d.Employees, o => o.MapFrom(s => s.Employees.ToList()));


            CreateMap<Emp, EmployeeDataWithDptName>()
                .ForMember(d=>d.DepartmentWithEmpName,o => o.MapFrom(s => s.Department.Name));
            //CreateMap<EmployeeDataWithDptName, Emp>()
            //     .ForMember(d => d.Department, o => o.MapFrom(s => s.DepartmentWithEmpName));
            CreateMap<Emp, EmpDto>()
                .ForMember(d => d.Department , o => o.MapFrom(s => s.Department.Name));
            CreateMap<EmpDto, Emp>()
                .ForMember(d => d.Department , o => o.MapFrom(s => s.Department.Name));
        }
    }
}
