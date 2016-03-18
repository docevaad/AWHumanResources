using System;

namespace AWHumanResources.Data.ViewModels
{
    public class EmpPayUpdateVM
    {
        public int BusinessEntityID { get; set; }
        public byte PayFrequency { get; set; }
        public decimal Rate { get; set; }
        public DateTime RateChangeDate { get; set; }
    }
}