<Query Kind="Expression">
  <Connection>
    <ID>54bf9502-9daf-4093-88e8-7177c12aaaaa</ID>
    <NamingService>2</NamingService>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\ChinookDemoDb.sqlite</AttachFileName>
    <DisplayName>Demo database (SQLite)</DisplayName>
    <DriverData>
      <PreserveNumeric1>true</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.Sqlite</EFProvider>
      <MapSQLiteDateTimes>true</MapSQLiteDateTimes>
      <MapSQLiteBooleans>true</MapSQLiteBooleans>
    </DriverData>
  </Connection>
</Query>

Albums

//query syntax to list all records in an entity (table, collection)

from arowoncollection in Albums
select arowoncollection

//method syntax to list all records in an entity

Albums
   .Select (arowoncollection => arowoncollection)

//Where
//filter method
//the conditions are set up as you would in C#
//beware that LINQPad may not like some C# syntax (DateTime)
//beware that LINQ is converted to SQL which may not like certain C# syntax because SQL could not convert

//syntax 
//notice that the method syntax makes use of the Lambda xpression
//Lambdas are common when performing LINQ with the method syntax
//.Where(Lambda expression)
//.Where(x => condition [logical operator condition2...])
//.Where(x => Bytes > 350000)

Tracks
	.Where(x => x.Bytes > 1000000000)
	
from x in Tracks
where x.Bytes > 1000000000
select x

//find all the albums of the artist Queen
//concerns: the artist name is in another table
//in an sql select you would use an inner join
//in linq you do not need to specify you inner join
//instead use the navigational properties of you entity to generate the relationship
Albums
	.Where(x => x.Artist.Name.Contains("Queen"))
	
