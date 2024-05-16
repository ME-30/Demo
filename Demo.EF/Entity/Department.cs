using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Demo.EF.Entity
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manger { get; set; }
        //[JsonIgnore]
        public List<Emp> Employees { get; set; }
    }
}
