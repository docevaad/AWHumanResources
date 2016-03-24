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
        private readonly string m_EmpWithPayHistTableName = "HumanResources.vEmployeeWithPayHist";
        private readonly string m_EmpPayHistTableName = "HumanResources.EmployeePayHistory";
        private readonly string m_EmpDeptHistTableName = "HumanResources.EmployeeDepartmentHistory";

        public EmployeeService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource;
        }

        public IEnumerable<EmployeeViewVM> GetAll()
        {
            return m_DataSource.From(m_EmpWithPayHistTableName)
                .ToCollection<EmployeeViewVM>()
                .Execute();
        }

        public IEnumerable<EmployeeViewVM> GetEmployeesByDepartmentId(int departmentId)
        {
            return m_DataSource.From(m_EmpWithPayHistTableName, new { DepartmentID = departmentId })
                .ToCollection<EmployeeViewVM>()
                .Execute();
        }

        public EmployeeViewVM GetEmployeeById(int id)
        {
            return m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = id })
                    .ToObject<EmployeeViewVM>()
                    .Execute();
        }

        public EmployeeViewVM UpdateEmployeePayHist(EmpPayUpdateVM vm)
        {
            EmployeePayHistDto emp = m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = vm.BusinessEntityID })
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

                m_DataSource.Insert(m_EmpPayHistTableName, ephDto).Execute();
                return m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = vm.BusinessEntityID })
                .ToObject<EmployeeViewVM>()
                .Execute();
            }

            return null; 
        }

        public EmployeeViewVM UpdateEmployeeDepartment(EmpDeptUpdateVM vm)
        {
            EmployeeViewDto emp = m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = vm.BusinessEntityID })
                .ToObject<EmployeeViewDto>()
                .Execute();

            if (emp != null)
            {
                DepartmentDto deptDto = m_DataSource.From("HumanResources.Department", new { DepartmentID = vm.DepartmentID })
                    .ToObject<DepartmentDto>()
                    .Execute();
                
                if (deptDto != null)
                {
                    var updateObj = new Dictionary<string, object>();
                    // Table Keys
                    updateObj["BusinessEntityID"] = emp.BusinessEntityID;
                    updateObj["DepartmentID"] = emp.DepartmentID;
                    updateObj["ShiftID"] = emp.ShiftID;
                    updateObj["StartDate"] = emp.DeptStartDate;

                    // Value to update
                    updateObj["EndDate"] = vm.CurrentDeptEndDate;
                    m_DataSource.Update(m_EmpDeptHistTableName, updateObj).Execute();
                
                    var empDeptHist = new EmployeeDeptHistDto
                    {
                        DepartmentID = deptDto.DepartmentID,
                        BusinessEntityID = vm.BusinessEntityID,
                        ShiftID = emp.ShiftID,
                        StartDate = vm.NewDeptStartDate,
                        EndDate = null,
                        ModifiedDate = DateTime.Now
                    };

                    m_DataSource.Insert(m_EmpDeptHistTableName, empDeptHist).Execute();
                    return m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = vm.BusinessEntityID })
                        .ToObject<EmployeeViewVM>()
                        .Execute();
                }
                return null;
            }
            return null;
        }
    }
}
