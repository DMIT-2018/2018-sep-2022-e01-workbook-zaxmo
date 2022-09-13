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

//list all albums by release label
//any album with no label should be indicated as Unknown
//list Title, Label, and Artist Name
//order by ReleaseLabel

//understand the problem
//collection: albums
//selective data: anonymous data set
//label (nullalbe): either Unknown or label name

//design
//Albums
//Select (new{})
// fields:	title
//			label ???? ternary operator (condition(s) ? true value : false value)
//			Artist.Name

//coding and testing

/*
Albums
	.Select(x => new
	{
		Title = x.Title,
		Label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel,
		Artist = x.Artist.Name		
	})
	.OrderBy (x => x.Label)
*/	
	
//list all albums showing the title, artist name, year, and decade of release using oldies, 70s, 80s, 90s, modern
//order by decade

//< 1970
//	Oldies
//else 
//	( < 1980 then 70s)
//else
//	( < 1990 then 80s)
//else
//	( < 2000 then 90s)
//else
//	( > 1999 then modern

Albums
	.Select (x => new
	{
		Title = x.Title,
		Artist = x.Artist.Name,
		Year = x.ReleaseYear,
		Decade = x.ReleaseYear < 1970 ? "Oldies" :
					x.ReleaseYear < 1980 ? "70s" :
					x.ReleaseYear < 1990 ? "80s" :
					x.ReleaseYear < 2000 ? "90s" : "Modern"		
	})
	.OrderBy (x => x.Year)
