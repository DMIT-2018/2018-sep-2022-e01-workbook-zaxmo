<Query Kind="Expression">
  <Connection>
    <ID>52026735-ce71-4858-bf56-e8084addad08</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WB320-19\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//using navigational properties and anonymous data set (collection)
//reference: Student Notes/Demo/eRestaurant/Linq: Query and Method Syntax

//Find all albums released in the 90's (1990 - 1999)
//order the albums by ascending year and then alphabetically by album title
//display the year, title, artist name, and release label

//concerns
//not all properties of album are to be displayed
//the order of the properties are to be displayed in a different sequence than the definition of the properties on the entity Album
//the artist name is not on the album table but is on the artist table

//use an anonymous data collection
//the anonymous data instance is defined within the Select by declared fields (properties)
//the order of the fields on the new defined instance will be done in specifying the properties of the anonymous data collection

Albums
.Where (x => x.ReleaseYear > 1989 && x.ReleaseYear < 2000)
.OrderBy (x => x.ReleaseYear)
.ThenBy (x => x.Title)
.Select (x => new
		{
			Year = x.ReleaseYear, 
			Title = x.Title, 
			Artist = x.Artist.Name, 
			Label = x.ReleaseLabel
			})
