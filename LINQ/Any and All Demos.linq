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

//any and all
//these filter tests return a true or false condition
//they work at the complete collection level

//Genres.Count().Dump();
//25

//show genres that have tracks which are not on any playlist
Genres
	.Where(g => g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0))
	.Select(g => g)
	//.Dump()
	;
	
//show genres that have all their tracks appearing at least once on a playlist
Genres
	.Where(g => g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0))
	.Select(g => g)
	//.Dump()
	;
	
//there maybe times that using a !Any() -> All(!relationship and !All -> Any(!relationship)
//using All and Any in comparing 2 collections
//if your collection is NOT a complex record there is a LINQ method called .Except that can be used to solve your query

//compare the track collection of 2 people using All and Any

//Roberto Almeida and Michelle Brooks

var almeida = PlaylistTracks
				.Where(x => x.Playlist.UserName.Contains("AlmeidaR"))
				.Select(x => new
				{
					Song = x.Track.Name,
					Genre = x.Track.Genre.Name,
					ID = x.Track.TrackId,
					Artist = x.Track.Album.Artist.Name
				})
				.Distinct()
				.OrderBy(x => x.Song)
				//.Dump()
				;


var brooks = PlaylistTracks
				.Where(x => x.Playlist.UserName.Contains("BrooksM"))
				.Select(x => new
				{
					Song = x.Track.Name,
					Genre = x.Track.Genre.Name,
					ID = x.Track.TrackId,
					Artist = x.Track.Album.Artist.Name
				})
				.Distinct()
				.OrderBy(x => x.Song)
				//.Dump()
				;
				
//list the tracks that both Roberto and Michelle like
//compare 3 datasets together, data in list A that is also in list B
//assume Roberto is list A and Michelle is list B
//list A is what you report from
//list B is what you compare to

//What songs does Roberto like but not Michelle
var c1 = almeida
			.Where(rob => !brooks.Any(mic => mic.ID == rob.ID))
			.OrderBy(rob => rob.Song)
//			.Dump()
			;
			

var c2 = almeida
			.Where(rob => brooks.All(mic => mic.ID != rob.ID))
			.OrderBy(rob => rob.Song)
//			.Dump()
			;
			
		
//what songs do both Roberto and Michelle like
var c3 = brooks
			.Where(mic => almeida.Any(rob => rob.ID == mic.ID))
			.OrderBy(mic => mic.Song)
			.Dump()
			;
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			