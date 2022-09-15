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
	//find songs by partial song name
	//display the album title, song, artist name

	//assume a value was entered into the web page
	//assume that a post button was pressed
	//assume Main() is the post event
	
	string inputValue = "dance";
	List<SongList> songCollection = SongsByPartialName(inputValue);
	songCollection.Dump();
}

// You can define other methods, fields, classes and namespaces here

//C# really enjoys strongly typed data fields
//whether these fields are primitive data types (int, double..)  or developer defined datatypes (class)

public class SongList
{
	public string Album{get;set;}
	public string Song{get;set;}
	public string Artist{get;set;}
}

//imagine the following method exists in a serice in  your BLL
//this method receives the web page parameter value for the query
//this method will need to return a collection

List<SongList> SongsByPartialName(string partialSongName)
{
	IEnumerable<SongList> songCollection = Tracks
						.Where(t => t.Name.Contains(partialSongName))
						.OrderBy(t => t.Name)
						.Select(t => new SongList
							{
								Album = t.Album.Title,
								Song = t.Name,
								Artist = t.Album.Artist.Name
							}
						);
	return songCollection.ToList();
}