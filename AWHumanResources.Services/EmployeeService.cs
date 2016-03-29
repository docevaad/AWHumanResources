using AWHumanResources.Data;
using AWHumanResources.Data.DTOs;
using AWHumanResources.Data.ViewModels;
using System;
using System.Collections.Generic;
using Tortuga.Chain;

namespace AWHumanResources.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeService
    {
        private readonly SqlServerDataSource m_DataSource;
        private readonly string m_EmpWithPayHistTableName = "HumanResources.vEmployeeWithPayHist";
        private readonly string m_EmpPayHistTableName = "HumanResources.EmployeePayHistory";
        private readonly string m_DepartmentTableName = "HumanResources.Department";
        private readonly string m_EmpDeptHistTableName = "HumanResources.EmployeeDepartmentHistory";

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public EmployeeService(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeViewVM> GetAll()
        {
            return m_DataSource.From(m_EmpWithPayHistTableName)
                .ToCollection<EmployeeViewVM>()
                .Execute();
        }

        /// <summary>
        /// Gets the employees by department identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public IEnumerable<EmployeeViewVM> GetEmployeesByDepartmentId(int departmentId)
        {
            return m_DataSource.From(m_EmpWithPayHistTableName, new { DepartmentID = departmentId })
                .ToCollection<EmployeeViewVM>()
                .Execute();
        }

        /// <summary>
        /// Gets the employee by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public EmployeeViewVM GetEmployeeById(int id)
        {
            return m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = id })
                    .ToObject<EmployeeViewVM>()
                    .Execute();
        }

        /// <summary>
        /// Updates the employee pay hist.
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the employee department.
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns></returns>
        public EmployeeViewVM UpdateEmployeeDepartment(EmpDeptUpdateVM vm)
        {
            EmployeeViewDto emp = m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = vm.BusinessEntityID })
                .ToObject<EmployeeViewDto>()
                .Execute();

            if (emp != null)
            {
                DepartmentDto deptDto = m_DataSource.From(m_DepartmentTableName, new { DepartmentID = vm.DepartmentID })
                    .ToObject<DepartmentDto>()
                    .Execute();
                
                if (deptDto != null)
                {
                    var updateObj = new Dictionary<string, object>();
                    // Table Keys
                    updateObj["BusinessEntityID"] = emp.BusinessEntityID;
                    updateObj["DepartmentID"] = emp.DepartmentID;
                    updateObj["ShiftID"] = emp.ShiftID;
                    updateObj["StartDate"] = emp.DeptStartDate.Date;

                    // Value to update
                    updateObj["EndDate"] = vm.CurrentDeptEndDate.Date;
                    m_DataSource.Update(m_EmpDeptHistTableName, updateObj).Execute();
                
                    var empDeptHist = new EmployeeDeptHistDto
                    {
                        DepartmentID = deptDto.DepartmentID,
                        BusinessEntityID = vm.BusinessEntityID,
                        ShiftID = emp.ShiftID,
                        StartDate = vm.NewDeptStartDate.Date,
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
