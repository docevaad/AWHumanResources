using AWHumanResources.Data;
using AWHumanResources.Data.DTOs;
using AWHumanResources.Data.ViewModels;
using System;
using System.Collections.Generic;
using Tortuga.Chain;

namespace AWHumanResources.Services
{
    public class EmployeeService
    {
        private readonly SqlServerDataSource m_DataSource;
        private readonly string HRvEWPHTable = "HumanResources.vEmployeeWithPayHist";
        private readonly string EmpPayHistTable = "HumanResources.vEmployeePayHistory";

        public EmployeeService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource;
        }

        public List<EmployeeViewDto> GetAll()
        {
            return m_DataSource.From(HRvEWPHTable)
                .ToCollection<EmployeeViewDto>()
                .Execute();
        }

        public List<EmployeeViewDto> GetEmployeesByDepartment(string department)
        {
            return m_DataSource.From(HRvEWPHTable, new { DepartmentName = department })
                .ToCollection<EmployeeViewDto>()
                .Execute();
        }

        public EmployeeViewDto GetEmployeeById(int id)
        {
            return m_DataSource.From(HRvEWPHTable, new { BusinessEntityID = id })
                .ToObject<EmployeeViewDto>()
                .Execute();
        }

        public EmployeePayHistDto UpdateEmployeePayHist(EmpPayUpdateVM vm)
        {
            EmployeePayHistDto emp = m_DataSource.From(HRvEWPHTable, new { BusinessEntityID = vm.BusinessEntityID })
                .ToObject<EmployeePayHistDto>()
                .Execute();

            if (emp != null)
            {
                var ephDto = new EmployeePayHistDto
                {
                    BusinessEntityID = vm.BusinessEntityID,
                    Rate = vm.Rate,
                    RateChangeDate = vm.RateChangeDate,
                    PayFrequency = vm.PayFrequency,
                    ModifiedDate = DateTime.Now
                };

                m_DataSource.Insert(EmpPayHistTable, ephDto).Execute();
                return m_DataSource.From(HRvEWPHTable, new { BusinessEntityID = vm.BusinessEntityID })
                .ToObject<EmployeePayHistDto>()
                .Execute();
            }

            //Maybe create an exception for this.
            return null;
        }

        public EmployeePayHistDto UpdateEmployeeDepartment(EmpDeptUpdateVM vm)
        {
            EmployeeViewDto emp = m_DataSource.From(HRvEWPHTable, new { BusinessEntityID = vm.BusinessEntityID })
                .ToObject<EmployeeViewDto>()
                .Execute();

            if (emp != null)
            {
                DepartmentDto deptDto = m_DataSource.From("HumanResources.Department", new { Name = vm.Department })
                    .ToObject<DepartmentDto>()
                    .Execute();
                
                if (deptDto != null)
                {
                    var empDeptHist = new EmployeeDeptHistDto
                    {
                        DepartmentID = deptDto.DepartmentID,
                        BusinessEntityID = vm.BusinessEntityID,
                        ShiftID = emp.ShiftID,
                        StartDate = vm.StartDate,
                        EndDate = null,
                        ModifiedDate = DateTime.Now
                    };

                    m_DataSource.Insert("HumanResources.EmployeeDepartmentHistory", empDeptHist).Execute();
                    return m_DataSource.From(HRvEWPHTable, new { BusinessEntityID = vm.BusinessEntityID })
                        .ToObject<EmployeePayHistDto>()
                        .Execute();
                }
                return null;
            }

            return null;
        }
    }
}
