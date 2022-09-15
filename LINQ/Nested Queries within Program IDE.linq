<Query Kind="Program">
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

void Main()
{
	//nested queries
	//sometimes referred to as subqueries
	//query within a query...
	
	//list all sales support employees showing their fullname (last, first), title, and phone
	//for each employee show a list of customers they support
	//show the customer fullname (last, first), city, and state
	
	//employee 1, title, phone
	//	customer 2000, city, state
	//	customer 2109, city, state
	//	customer 5000, city, state
	//employee 2, title, phone
	//	customer 3041, city, state
	
	//there are 2 separate lists that need to be within one final dataset collection
	//list of employees
	//list of employee customers
	//concern: the lists are intermixed
	
	//C# point of view in a class definition
	//first: this is a composite class
	//the class is describing an employee
	//each instance of the employee will have a list of employee customers
	
	//class EmployeeList
	//	fullname (property)
	//	title (property)
	//	phone (property)
	//	collection of customers (property: List<T>)
	
	//class CustomerList
	//	fullname (property)
	//	city (property)
	//	state (property)
	
	var results = Employees
					.Where (e => e.Title.Contains("Sales Support"))
					.Select (e => new EmployeeItem
						{
							FullName = e.LastName + ", " + e.FirstName,
							Title = e.Title,
							Phone = e.Phone,
							CustomerList = e.SupportRepCustomers
											.Select(c => new CustomerItem
											{
												FullName = c.LastName + ", " + c.FirstName,
												City = c.City,
												State = c.State
											}
											)
						}
					);
	results.Dump();
}

public class CustomerItem
{
	public string FullName {get;set;}
	public string City {get;set;}
	public string State {get;set;}
}

public class EmployeeItem
{
	public string FullName {get;set;}
	public string Title {get;set;}
	public string Phone {get;set;}
	public IEnumerable<CustomerItem> CustomerList {get;set;}
}