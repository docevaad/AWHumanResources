USE AdventureWorks2012
GO
IF OBJECT_ID('HumanResources.vEmployeeWithPayHist','V') IS NOT NULL
	DROP VIEW HumanResources.vEmployeeWithPayHist;
GO
CREATE VIEW HumanResources.vEmployeeWithPayHist
AS
SELECT	e.BusinessEntityID, 
		p.Title, 
		p.FirstName, 
		p.MiddleName, 
		p.LastName, 
		p.Suffix, 
		eph.PayFrequency, 
		eph.Rate, 
		eph.RateChangeDate, 
		edh.ShiftID, 
		edh.StartDate as DeptStartDate, 
		edh.DepartmentID, 
		d.Name as DepartmentName, 
		d.GroupName as DeptGroupName
FROM AdventureWorks2012.HumanResources.Employee as e
JOIN AdventureWorks2012.Person.Person as p on e.BusinessEntityID = p.BusinessEntityID
JOIN (SELECT t1.BusinessEntityID, t1.ModifiedDate, t1.PayFrequency, t1.Rate, t1.RateChangeDate
	  FROM AdventureWorks2012.HumanResources.EmployeePayHistory as t1
	  JOIN (SELECT BusinessEntityID,MAX(RateChangeDate) as MaxDate
			FROM AdventureWorks2012.HumanResources.EmployeePayHistory
			GROUP BY BusinessEntityID) as t2 on t1.BusinessEntityID = t2.BusinessEntityID AND 
			                                    t1.RateChangeDate = t2.MaxDate) as eph on e.BusinessEntityID = eph.BusinessEntityID
JOIN AdventureWorks2012.HumanResources.EmployeeDepartmentHistory as edh on e.BusinessEntityID = edh.BusinessEntityID AND
																		   edh.EndDate IS NULL
JOIN AdventureWorks2012.HumanResources.Department as d on d.DepartmentID = edh.DepartmentID;
