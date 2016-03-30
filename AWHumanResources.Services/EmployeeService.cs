using AWHumanResources.Data;
using AWHumanResources.Data.DTOs;
using AWHumanResources.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public Task<List<EmployeeViewVM>> GetAllAsync()
        {
            return m_DataSource.From(m_EmpWithPayHistTableName)
                .ToCollection<EmployeeViewVM>()
                .ExecuteAsync();
        }

        /// <summary>
        /// Gets the employees by department identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public Task<List<EmployeeViewVM>> GetEmployeesByDepartmentIdAsync(int departmentId)
        {
            return m_DataSource.From(m_EmpWithPayHistTableName, new { DepartmentID = departmentId })
                .ToCollection<EmployeeViewVM>()
                .ExecuteAsync();
        }

        /// <summary>
        /// Gets the employee by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<EmployeeViewVM> GetEmployeeByIdAsync(int id)
        {
            return m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = id })
                    .ToObject<EmployeeViewVM>()
                    .ExecuteAsync();
        }

        /// <summary>
        /// Updates the employee pay hist.
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns></returns>
        public async Task<EmployeeViewVM> UpdateEmployeePayHistAsync(int employeeId, EmpPayUpdateRequest vm)
        {
            EmployeePayHistDto emp = await m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = employeeId })
                .ToObject<EmployeePayHistDto>()
                .ExecuteAsync();

            if (emp != null)
            {
                var ephDto = new EmployeePayHistDto
                {
                    BusinessEntityID = employeeId,
                    Rate = vm.Rate,
                    RateChangeDate = vm.RateChangeDate,
                    PayFrequency = vm.PayFrequency,
                    ModifiedDate = DateTime.Now
                };

                await m_DataSource.Insert(m_EmpPayHistTableName, ephDto).ExecuteAsync();
                return await m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = employeeId })
                .ToObject<EmployeeViewVM>()
                .ExecuteAsync();
            }

            return null;
        }

        /// <summary>
        /// Updates the employee department.
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns></returns>
        public async Task<EmployeeViewVM> UpdateEmployeeDepartmentAsync(int employeeId, EmpDeptUpdateRequest vm)
        {
            EmployeeViewDto emp = await m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = employeeId })
                .ToObject<EmployeeViewDto>()
                .ExecuteAsync();

            if (emp != null)
            {
                DepartmentDto deptDto = await m_DataSource.From(m_DepartmentTableName, new { DepartmentID = vm.DepartmentID })
                    .ToObject<DepartmentDto>()
                    .ExecuteAsync();
                
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
                    await m_DataSource.Update(m_EmpDeptHistTableName, updateObj).ExecuteAsync();
                
                    var empDeptHist = new EmployeeDeptHistDto
                    {
                        DepartmentID = deptDto.DepartmentID,
                        BusinessEntityID = employeeId,
                        ShiftID = emp.ShiftID,
                        StartDate = vm.NewDeptStartDate.Date,
                        EndDate = null,
                        ModifiedDate = DateTime.Now
                    };

                    m_DataSource.Insert(m_EmpDeptHistTableName, empDeptHist).Execute();
                    return await m_DataSource.From(m_EmpWithPayHistTableName, new { BusinessEntityID = employeeId })
                        .ToObject<EmployeeViewVM>()
                        .ExecuteAsync();
                }
                return null;
            }
            return null;
        }
    }
}
