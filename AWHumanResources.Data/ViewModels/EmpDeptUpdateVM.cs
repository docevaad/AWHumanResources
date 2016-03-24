using System;

namespace AWHumanResources.Data.ViewModels
{
    public class EmpDeptUpdateVM
    {
        public int BusinessEntityID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime CurrentDeptEndDate { get; set; }
        public DateTime NewDeptStartDate { get; set; }
    }
}
