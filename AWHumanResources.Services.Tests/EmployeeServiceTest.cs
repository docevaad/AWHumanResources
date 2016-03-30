using AWHumanResources.Data.ViewModels;
using System;
using System.Collections.Generic;
using Tortuga.Chain;
using Xunit;

namespace AWHumanResources.Services.Tests
{
    public class EmployeeServiceTest
    {
        private readonly SqlServerDataSource m_DataSource;

        public EmployeeServiceTest()
        {
            m_DataSource = new SqlServerDataSource(System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorksSqlServer"].ConnectionString);
        }

        [Fact]
        public async void UpdateEmployeePayHistTest()
        {
            // Setup
            int employeeId = 1;
            var input = new EmpPayUpdateRequest
            {
                PayFrequency = 1,
                Rate = 45.45M,
                RateChangeDate = DateTime.Now
            };
            var classUnderTest = new EmployeeService(m_DataSource);

            // Act
            var output = await classUnderTest.UpdateEmployeePayHistAsync(employeeId, input);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(employeeId, output.BusinessEntityID);
            Assert.Equal(input.PayFrequency, output.PayFrequency);
            Assert.Equal(input.Rate, output.Rate);

            // Clean-up
            var cleanUpObj = new Dictionary<string, object>();
            cleanUpObj["BusinessEntityID"] = employeeId;
            cleanUpObj["PayFrequency"] = input.PayFrequency;
            cleanUpObj["Rate"] = input.Rate;
            cleanUpObj["RateChangeDate"] = input.RateChangeDate;
            m_DataSource.Delete("HumanResources.EmployeePayHistory", cleanUpObj).Execute();
        }

        [Fact]
        public async void UpdateEmployeeDepartmentTest()
        {
            // Setup
            int employeeId = 1;
            var input = new EmpDeptUpdateRequest
            {
                DepartmentID = 2,
                CurrentDeptEndDate = DateTime.Now.AddDays(-1),
                NewDeptStartDate = DateTime.Now
            };
            var classUnderTest = new EmployeeService(m_DataSource);

            // Act
            var output = await classUnderTest.UpdateEmployeeDepartmentAsync(employeeId, input);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(employeeId, output.BusinessEntityID);
            Assert.Equal(input.DepartmentID, output.DepartmentID);

            // Clean-up
            var shiftIDSql =
                @"SELECT ShiftID
                FROM AdventureWorks2012.HumanResources.EmployeeDepartmentHistory
                WHERE BusinessEntityID=@BusinessEntityID AND
                      DepartmentID=@DepartmentID";
            var shiftID = m_DataSource.Sql(shiftIDSql, new { BusinessEntityID = employeeId, DepartmentID = input.DepartmentID }).ToInt32().Execute();
            var repairSql =
                @"DELETE FROM AdventureWorks2012.HumanResources.EmployeeDepartmentHistory
                  WHERE BusinessEntityID=@BusinessEntityID AND
                        DepartmentID=@DepartmentID AND
                        ShiftID=@ShiftID AND
                        StartDate=@StartDate;
                  UPDATE AdventureWorks2012.HumanResources.EmployeeDepartmentHistory
                  SET EndDate=NULL
                  WHERE BusinessEntityID=@BusinessEntityID AND
                        EndDate=@EndDate;";
            var cleanUpObj = new Dictionary<string, object>();
            cleanUpObj["BusinessEntityID"] = employeeId;
            cleanUpObj["DepartmentID"] = input.DepartmentID;
            cleanUpObj["StartDate"] = input.NewDeptStartDate.Date;
            cleanUpObj["ShiftID"] = shiftID;
            cleanUpObj["EndDate"] = input.CurrentDeptEndDate.Date;
            m_DataSource.Sql(repairSql, cleanUpObj).Execute();
        }
    }
}
