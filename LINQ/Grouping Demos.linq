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

//grouping

//when you create a group it builds 2 components
//A) key component (deciding criteria value(s)) defining the group
//		reference this component using the groupname.Key

//		1 value for key: groupname.Key
//		n values for key: groupname.Key.propertyname
// (property < - > field < - > attribute < - > value)
//B) data of the group (raw instances of the collection)

//ways to group
//A) by a single column (field, attribute, property)	groupname.Key
//B) by a set of columns (anonymous dataset)			groupname.Key.property
//C) by use of an entity (entity name/navproperty)		groupname.Key.property

//concept processing
//start with a pile of data (original collection prior to grouping
//specify the grouping property
//result of the group operation will be to "place the data into smaller piles"
//the piles are dependant on the grouping property(ies) value(s)
//the grouping property(ies) become the Key
//the individual instances are the data in the smaller piles
//the entire individual instance of the original collection is placed in the smaller pile
//manipulate each of the smaller piles using linq commands

//grouping is different than ordering
//ordering is the final re-sequencing of a collection for display
//grouping re-organizes a collection into separate, usually smaller collection for further processing (ie. aggregates)

//grouping is an excellent way to organize your data especially if you need to process data on a property that is not a relative key
//			such as a foreign key which forms a "natural" group using the navigational properties

//display albums by ReleaseYear
//this request does not need grouping
//this request is an ordering of output : OrderBy
//this ordering effects only display

Albums
	.OrderBy(a => a.ReleaseYear)
	
//display all albums grouped by releaseyear
//explicit request to break up the display into desired "piles"

Albums
	.GroupBy(a => a.ReleaseYear)
	
//processing on the groups created by the Group command
//display the number of albums produced each year
//list only year which have more than 10 albums

Albums
	.GroupBy(a => a.ReleaseYear)
	.Where(eachgroupPile => eachgroupPile.Count() > 10) //filtering against each group pile
	.Select(eachgroupPile => new
	{
		Year = eachgroupPile.Key,
		NumOfAlbums = eachgroupPile.Count()
	})
	//.Where(eachgroupPile => eachgroupPile.NumOfAlbums > 10) //filtering against the output of the Select() command
	
	
//use a multiple set of properties to form the group
//unclude a nested query to report on the small pile group

//display albums grouped by ReleaseLabel, ReleaseYear. display the ReleaseYear and number of albums
//list only the years with 10 or more albums released
//for each album display the title, artist, number of tracks on the album and release year

Albums
	.GroupBy(a => new {a.ReleaseLabel, a.ReleaseYear})
	.Where(egP => egP.Count() > 2) 
	.Select(eachgroupPile => new
	{
		Label = eachgroupPile.Key.ReleaseLabel,
		Year = eachgroupPile.Key.ReleaseYear,
		NumOfAlbums = eachgroupPile.Count(),
		AlbumItems = eachgroupPile
				.Select(egPInstance => new
				{
					Title = egPInstance.Title,
					Artist = egPInstance.Artist.Name,
					TrackCount = egPInstance.Tracks
						.Select(x => x),
					YearOfAlbum = egPInstance.ReleaseYear
				})
	})