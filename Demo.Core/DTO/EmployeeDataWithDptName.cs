using Demo.EF.Entity;

namespace Demo.Core.DTO
{

    public class EmployeeDataWithDptName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Salary { get; set; }
        public string DepartmentWithEmpName { get; set; }
        //public int DepartmentId { get; set; }
        //public virtual DepartmentWithEmpName DepartmentWithEmpName { get; set; }
        //public string DepartmentName { get; set; }
    }
}
