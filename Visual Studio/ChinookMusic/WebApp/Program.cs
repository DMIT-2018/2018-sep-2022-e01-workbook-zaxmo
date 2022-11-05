using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

#region Additional Namespaces
using ChinookSystem;
#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//supplied database connection due to the fact that we reated this web app to use idividual accounts
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//add another getconnectionstring to reference our database connectionstring

var connectionStringChinook = builder.Configuration.GetConnectionString("ChinookDB");

//given for the db connection to Defaultconnection string

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//code the dbconnection to the application db context for Chinook
//the implementation of the connect AND registration of the Chinook system services will be done in the ChinookSystem class library
//to accomplish this task we will be using an extension method
//the extension method will extend the IServiceCollection class
//the extension method requires a parameter options.UseSqlServer(xxx) where xxx is the connectionstring variable


builder.Services.ChinookSystemBackendDependencies(options => options.UseSqlServer(connectionStringChinook));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
