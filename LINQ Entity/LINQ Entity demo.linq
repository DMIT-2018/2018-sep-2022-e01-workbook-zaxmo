<Query Kind="Program">
  <Connection>
    <ID>6e4adbe0-76b6-4fcf-bc71-0e83d308e4e6</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.\SQLEXPRESS</Server>
    <DisplayName>Chinook-Entity</DisplayName>
    <Persist>true</Persist>
    <Database>Chinook</Database>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	//main is going to represent the web page post method
	string searcharg = "Deep";
	string searchby = "Artist";
	List<TrackSelection> tracklist = Track_FetchTracksBy(searcharg, searchby);
	tracklist.Dump();
}

// You can define other methods, fields, classes and namespaces here

#region CQRS Queries
public class TrackSelection
{
    public int TrackId {get; set;}
    public string SongName {get; set;}
    public string AlbumTitle{get; set;}
    public string ArtistName{get; set;}
    public int Milliseconds {get; set;}
    public decimal Price {get; set;}
}

public class PlaylistTrackInfo 
{
    public int TrackId {get; set;}
    public int TrackNumber {get; set;}
    public string SongName {get; set;}
    public int Milliseconds {get; set;}
}
#endregion

//pretend to be the class library project
#region TrackServices class

public List<TrackSelection> Track_FetchTracksBy(string searcharg, string searchby)
{
	if (string.IsNullOrWhiteSpace(searcharg))
	{
		throw new ArgumentNullException("No search value submitted");
	}
	if (string.IsNullOrWhiteSpace(searchby))
	{
		throw new ArgumentNullException("No search style submitted");
	}
	IEnumerable<TrackSelection> results = Tracks
										.Where(x => (x.Album.Artist.Name.Contains(searcharg) &&
													searchby.Equals("Artist")) || (x.Album.Title.Contains(searcharg)
													&& searchby.Equals("Album")))
										.Select(x => new TrackSelection
											{
												TrackId = x.TrackId,
												SongName = x.Name,
												AlbumTitle = x.Album.Title,
												ArtistName = x.Album.Artist.Name,
												Milliseconds = x.Milliseconds,
												Price = x.UnitPrice
											});
	return results.ToList();
}

#endregion