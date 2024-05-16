using System.Collections.Generic;

namespace Demo.Core.DTO
{
    public class DepartmentWithEmpName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manger { get; set; }


        public ICollection<EmployeeDataWithDptName> Employees{get;set;}
    }
}
