<Query Kind="Expression">
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

//aggregates
//.Count() counts the numbers of instances in the collection
//.Sum(x => ...) sums a numeric field (numeric expression)
//.Min(x => ...) finds the minimum value of a collection for a field
//.Max(x => ...) finds the maximum value of a collection for a field
//.Average(x => ...) finds the average value of a numberic field in a collection

//IMPORTANT!!! 
//aggregates work only on a collection of values
//aggregates do not work on a single instance 

//Sum Min Max and Average must have at least one record in their collection
//Sum and Average must work on numeric fields and the field cannot be null

//syntax: method
//collectionset.aggregate(x => expression)
//collectionset.Select(...).aggregate()
//collectionset.Count()
//Count() does not contain an expresion

//for sum min max and average: the results is a single value

//you can use multiple aggregates on a single column
//.Sum(x => expression).Min(x => expression)

//find the average playing time of the tracks in our music collection

//thought process-----
//average is an aggregate
//what is the collection? the tracks table is a collection
//what is the expression? ms

Tracks.Average(x => x.Milliseconds) //each x has multiple fields
Tracks.Select(x => x.Milliseconds).Average() // a single list of numbers
//Tracks.Average() aborts because no specific field was referred to on the track record

//list all albums of the 60s showing the title artist and various aggregates for albums containing tracks
//for each album show the number of tracks, the total price of all tracks and the average playing length of the album tracks
//thought process-=-=-=-=

//start at albums
//can i get the artist name (.Artist)
//can i get a collection of tracks for an album (x.Tracks)
//can i get the number of tracks in the collection (.Count())
//can i get the total price of the tracks (.Sum())
//can i get the average of the length of tracks(.Average())

Albums
	.Where(x => x.Tracks.Count() > 0
		&& (x.ReleaseYear > 1959 && x.ReleaseYear < 1970))
	.Select(x => new
	{
		Title = x.Title,
		Artist = x.Artist.Name,
		NumberofTracks = x.Tracks.Count(),
		TotalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
		AverageTrackLength = x.Tracks.Select(tr => tr.Milliseconds).Average()
	})
	