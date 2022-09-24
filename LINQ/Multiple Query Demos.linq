<Query Kind="Statements">
  <Connection>
    <ID>3ab931f7-c603-4b77-b1c8-4e591c518cc2</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WC320-15\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//problem
//one needs to have processed information from a collection to use against the same collection
//solution to this type of problem is to use multiple queries
//the early query(ies) will produce the needed information/criteria to execute against the same collection in later query(ies)
//basically we need to do some pre processing

//query one will generate data/information that will be used in the next query

//display the employees that have the most customers to support
//display the employee name and number of customers that employee supports

//what is NOT wanted is a list of all employees sorted by a number of customers supported
//one could create a list of all employees with the customer support count, ordered descending by support count BUT  this is NOT what was requested

//what information do i need
//i need to know the maximum number of customers that a particular employee is supporting
//i need to take that piece of data and compare to all employees

//get a list of employees and the count of the customers each supports
//from that list i can obtain the largest number
//using the number, review all the employees and their counts, reporting ONLY the busiest employees

var preProcessEmployeeList = Employees
								.Select(x => new
								{
									Name = x.FirstName + " " + x.LastName,
									CustomerCount = x.SupportRepCustomers.Count()
								})
								//.Dump()
								;
								
//var highCount = preProcessEmployeeList
//				.Max(x => x.CustomerCount)
//				//.Dump()
//				;
//
//var busyEmployees = preProcessEmployeeList
//					.Where(x => x.CustomerCount == highCount)
//					.Dump()
//					;
					
var busyEmployees = preProcessEmployeeList
					.Where(x => x.CustomerCount == preProcessEmployeeList.Max(x => x.CustomerCount))
					.Dump()
					;					
