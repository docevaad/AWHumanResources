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
        public void UpdateEmployeePayHistTest()
        {
            // Setup
             var input = new EmpPayUpdateVM
            {
                BusinessEntityID = 1,
                PayFrequency = 1,
                Rate = 45.45M,
                RateChangeDate = DateTime.Now
            };
            var classUnderTest = new EmployeeService(m_DataSource);
            
            // Act
            var output = classUnderTest.UpdateEmployeePayHist(input);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(input.BusinessEntityID, output.BusinessEntityID);
            Assert.Equal(input.PayFrequency, output.PayFrequency);
            Assert.Equal(input.Rate, output.Rate);

            // Clean-up
            var cleanUpObj = new Dictionary<string, object>();
            cleanUpObj["BusinessEntityID"] = input.BusinessEntityID;
            cleanUpObj["PayFrequency"] = input.PayFrequency;
            cleanUpObj["Rate"] = input.Rate;
            cleanUpObj["RateChangeDate"] = input.RateChangeDate;
            m_DataSource.Delete("HumanResources.EmployeePayHistory", cleanUpObj).Execute();
        }

        [Fact]
        public void UpdateEmployeeDepartmentTest()
        {
            // Setup
            var input = new EmpDeptUpdateVM
            {
                BusinessEntityID = 1,
                DepartmentID = 2,
                CurrentDeptEndDate = DateTime.Now.AddDays(-1),
                NewDeptStartDate = DateTime.Now
            };
            var classUnderTest = new EmployeeService(m_DataSource);

            // Act
            var output = classUnderTest.UpdateEmployeeDepartment(input);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(input.BusinessEntityID, output.BusinessEntityID);
            Assert.Equal(input.DepartmentID, output.DepartmentID);

            // Clean-up
            var shiftIDSql =
                @"SELECT ShiftID
                FROM AdventureWorks2012.HumanResources.EmployeeDepartmentHistory
                WHERE BusinessEntityID=@BusinessEntityID AND
                      DepartmentID=@DepartmentID";
            var shiftID = m_DataSource.Sql(shiftIDSql, new { BusinessEntityID = input.BusinessEntityID, DepartmentID = input.DepartmentID }).ToInt32().Execute();
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
            cleanUpObj["BusinessEntityID"] = input.BusinessEntityID;
            cleanUpObj["DepartmentID"] = input.DepartmentID;
            cleanUpObj["StartDate"] = input.NewDeptStartDate.Date;
            cleanUpObj["ShiftID"] = shiftID;
            cleanUpObj["EndDate"] = input.CurrentDeptEndDate.Date;
            m_DataSource.Sql(repairSql, cleanUpObj).Execute();
        }
    }
}
