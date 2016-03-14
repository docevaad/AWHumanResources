using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWHumanResources.Data
{
    public class EmployeeDto
    {
        public int BusinessEntityID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobName { get; set; }
        public string Department { get; set; }
    }
}
