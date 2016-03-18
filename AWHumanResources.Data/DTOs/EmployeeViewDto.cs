using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWHumanResources.Data
{
    public class EmployeeViewDto
    {
        public int BusinessEntityID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public byte PayFrequency { get; set; }
        public decimal Rate { get; set; }
        public DateTime RateChangeDate { get; set; }
        public int ShiftID { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DeptGroupName { get; set; }
    }
}
