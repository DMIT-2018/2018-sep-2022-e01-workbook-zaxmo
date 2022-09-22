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
	//conversions
	//collections we will look at are Iqueryable, IEnumerable, and List
	
	//display all albums and their tracks
	//display the album title, artist name and album tracks
	//for each track show the song name and play time 
	//show only albums with 25 or more tracks
	
	List<AlbumTracks> albumlist = Albums
						.Where(a => a.Tracks.Count() >= 25)
						.Select(a => new AlbumTracks
						{
							Title = a.Title,
							Artist = a.Artist.Name,
							Songs = a.Tracks
								.Select (tr => new SongItem
								{
									Song = tr.Name,
									Playtime = tr.Milliseconds / 1000.0
								})
								.ToList()
						})
						.ToList()
						.Dump()
						;
	//using .FirstOrDefault()
	//first saw in CPSC1517 when checking to see if a record existed in a BLL service method
	
	//find the first album by Deep Purple
	var artistparam = "Deep Purple";
	var resultsFOD = Albums
						.Where(a => a.Artist.Name.Equals(artistparam))
						.Select(a => a)
						.OrderBy(a => a.ReleaseYear)
						.FirstOrDefault()
	//					.Dump()
						;
	if (resultsFOD != null)
	{
		resultsFOD.Dump();	
	}
	else
	{
		Console.WriteLine($"No albums found for artist {artistparam}");
	}
	
	var customerCountries = Customers
								.OrderBy(c => c.Country)
								.Select(c => c.Country)
								.Distinct()
								.Dump();
	
	
	//.Take() and .Skip()
	//in CPSC1517 when you want to return the supplied Paginator
	//the query method was to return only the needed records for the display not the entire collection
	//A) the query was executed returning a collection of size X
	//B) obtained the total count (x) of return records
	//C) calculated the number of records to skip (pagenumber -1) * pagesize
	//D) on the return method statement you used returns variablename.Skip(rowsSkipped).Take(pagesize).ToList()
	
	//Union
	//rules in linq are the same as SQL
	//result is the same as sql, combine separate collections into one
	//syntax (queryA).Union(queryB)[.Union(query...)]
	//rules:
	//	number of columns are the same
	//	column datatypes must be the same
	//	ordering should be done as a method after the last union
	
	var resultsUnionA = (Albums
						.Where(x => x.Tracks.Count() == 0)
						.Select(x => new
						{
							Title = x.Title,
							TotalTracks = 0,
							TotalCost = 0.00m,
							AverageLength = 0.00d
						})
						)
					.Union(Albums
						.Where(x => x.Tracks.Count() > 0)
						.Select(x => new
						{
							Title = x.Title,
							TotalTracks = x.Tracks.Count(),
							TotalCost = x.Tracks.Sum(tr => tr.UnitPrice),
							AverageLength = x.Tracks.Average(tr => tr.Milliseconds)
						})
						)
						.OrderBy(x => x.TotalTracks)
						.Dump()
						;
}

public class SongItem
{
	public string Song{get;set;}
	public double Playtime{get;set;}
}
public class AlbumTracks
{
	public string Title{get;set;}
	public string Artist{get;set;}
	public IEnumerable<SongItem> Songs{get;set;}
}

// You can define other methods, fields, classes and namespaces here