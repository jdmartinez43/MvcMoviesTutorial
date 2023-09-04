# C sharp MVC ASP.NET Tutorial

## Getting Started
### Requirements (VS Code Environment)
- VS Code with ASP.NET 7.0 
- Tutorial Videos: https://www.youtube.com/playlist?list=PLdo4fOcmZ0oXCPdC3fTFA3Z79-eVH3K-s

Create Web app 
```
dotnet new mvc -o [WebAppName]
code -r [WebAppNAme]
```
Run app
```
dotnet dev-certs https --trust
```
The port used can be found in the `/Properties/launchSettings.json` file.

## MVC Pattern
Model View Architectural patterns are used to separate an app into 3 components: Model, View, and Controller
- Model: Represents data of the app. Contains validation logic to manipulate data. Typically retrieves, stores, updates, and deletes data by interacting with a database
- View: The UI that displays content, such as the model data
- Controller: Handles browser requests, retrieves model data, and calls views to return a response. The kind of like middle man between Model and View by handling Users responses and passes that data to the model.

## Controllers
Formatted as [ObjectName]Controller and put in the Controllers directory. With dotnet development you format each endpoint like below:
```
// [HTTP_REQUEST_TYPE]: /[Controller]/[ActionName]/[Parameters]
public return-type Function(**args)
{
    // code
}
```
The URL logic is the default but can be modified within the `Program.cs` file by changing the App's ControllerRoute pattern.
### Parameters
You can specify __query__ and __route__ parameters in an endpoint's function. A request must match the parameters from a specified pattern in the `Program.cs` file. See the `Welcome` function for an example

## View
IN .NET development, we use Razor-based view templates that allow us to use C# with html using .cshtml files. Manage these files within the View directory. 
The `Views/Shared/_Layout.cshtml` file contains Layout template files that store that layout of the whole site in one place. 
The `ViewData` dictionary is used to manipulate aspects of the webpage. See examples of it in action in `Views` .cshtml files. 

## Model 
Link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-7.0&tabs=visual-studio-code
Stored in the Model, they work with the Entity Framework Core to work with databases. These classes are called Plain Old CLR Objects, used to defined the properties of the data stored in the database. (Think POJO's from 122B)
### Setup
Run the following commands to setup the model. Make sure you are within the directory of where the .csproj is.
```
dotnet tool uninstall --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```
To set up scaffolding tool run this command
```
dotnet aspnet-codegenerator controller -name [NameController] -m ModelName -dc [DataContextDirectory] --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider [DatabaseType such as 'sqlite']
```
This Scaffolding creates:
- A controller based on the input name [NameController]
- Razor view files for Create, Delete, Details, Edit, and Index pages: `Views/Movies/*.cshtml`
- A database context class: Data/[ModelName]
This Scaffolding updates:
- Registers the Database context in the Program.cs file
- Adds a database connection string to the `appsettings.json` file

### Migrations
Run these commands to create/update a database used to match the data model.
```
dotnet ef migrations add [DescriptionOfMigration]
dotnet ef database update // runs the latest migration from the Up method to create/update the database
// dotnet ef databse remove <- runs the teardown function from the Down method to delete the database or parts of it
```
### Dependency Injection
idk i have to read this over again but its to make it easier to keep components separate
### Strongly typed models and the @model directive
The ViewData dictionary is used so controllers can pass data or objects to a View. Within View files, the @model is used to specify the expected object we want. In the `/Views/Movies/Details.cshtml`, we see the `@model MvcMovie.Models.Movie` to declare that the `model` object in this file is expected to behave using data from the Movie POCO.  
To enumerate over Model objects, Use the `IEnumerable<ModelName>`. An example of this in use with a for loop is in the Index.cshtml file 

## Databases in an ASP.NET Core MVC app
Link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-7.0
The project's Context file handles connecting to the database within the `Program.cs` file.
To learn more about connections, learn here https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0

### SQLite
For managing and viewing sqlite db's: https://sqlitebrowser.org/
Notes:
- SQLite allows adding a column but not removing and changing a column; migrations created to remove or change a column succeed but updates fail. To rebuild a table, you must:
- Creating a new table.
- Copying data from the old table to the new table.
- Dropping the old table.
- Renaming the new table.

ASP.NET Core reads the `ConnectionString` from the `appsettings.json` file to find the sqlite database file to work with. Working with database's involving the Entity Framework involves using DbContext (your context class) to interact with your Model's. For Reference: https://learn.microsoft.com/en-us/ef/ef6/fundamentals/working-with-dbcontext.

### Seed database
View `SeedData.cs` in the Models folder to see how to populate database's. 

### Drop database
View `DeleteData.cs` in the models folder to drop all records stored in the database

## Controllers and Views in ASP.NET Core
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-7.0