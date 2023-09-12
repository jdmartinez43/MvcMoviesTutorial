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

DataTypes and DataAnnotations are important to make sure we are interacting with data in the Model properly. Learn more here https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt#column-data-types

In the Movie.cs file, we see the Price property is configured to have precision of 14 and scale 2 for the Decimal DataType. 

## MoviesController.cs

[Tag helpers](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro?view=aspnetcore-7.0) let the server side code create and render html elements in Razor file dynamically. In this Movies project, we see how it is used for the `Edit`, `Details`, and `Delete` functionality, which works thanks to the pattern matching we defined in the `Program.cs`.

[Bind Attribute](https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application#overpost) is used to protext agianst overposting. We only include properties with the Bind attribute that we want to change

Http methods have their own attributes to let us know if we want to make a GET, POST, etc request. By default functions use [HttpGet]. Ideally we try to follow REST patterns, such as not modifying the application during a GET Request

## Search Functionality
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/search?view=aspnetcore-7.0
Read over the `Index` functions over how to create search queries within the `MoviesController.cs` 

### Linq
(Language Level Querying)[https://learn.microsoft.com/en-us/dotnet/standard/linq/] can be used with C# to access data. In the Index within the `MoviesController.cs` file, we define a query to search the database. The query contains a lambda expression to limit the data we want to obtain. One notable thing about the query is that LINQ queries don't run when they are __defined__, but when they are __called__. We receive the results after the value is iterated over or when we call the `ToListAsync()` function.

## Views
The `DisplayNameFor` HTML Helper inspects the `Title` property referenced in the lambda expression to determine the display name. Since the lambda expression is inspected rather than evaluated, you don't receive an access violation when `model`, `model.Movies`, or `model.Movies[0]` are `null` or empty. When the lambda expression is evaluated (for example, `@Html.DisplayFor(modelItem => item.Title)`), the model's property values are evaluated. 

The `!` after `model.Movies` is the null-forgiving operator, which is used to declare that Movies isn't null. It's a postfix operator used to supress all nullable warnings. It only affects compiler's static flow analysis by changing the null state of the expression. The expression `x!` evaluates to the result of the underlying expression `x`. By using the null-forgiving operator, you inform the compiler that passing null is expected and shouldn't be warned about.

## [Add a new field to an ASP.NET Core MVC app](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/new-field?view=aspnetcore-7.0&tabs=visual-studio)

We use the Entity Framework Code First Migrations in this section to:
- Add a new field to the model
- Migrate a new field to the database 

When we update any of the Models within the `Models/` directory, we need to rebuild the project which we can do with `dotnet build`. You also need to update the property binding list. This can be done by updating the `[Bind]` attribute for both the `Create` and `Edit` action methods like so 
```[Bind("Id,Title,ReleaseDate,Genre,Price,Rating")]```

We need to update our views to contain the Rating attribute. These changes are shown in the `Views/Movies/` files. We also must update our seed Data to contain Rating values.

One important thing we must do is modify our database schema since we have no added a new Rating feature. Some ways to fix this include:

1. Have the Entity Framework automatically drop and re-create the database based on the new model class schema. This approach is very convenient early in the development cycle when you're doing active development on a test database; it allows you to quickly evolve the model and database schema together. The downside, though, is that you lose existing data in the database — so you don't want to use this approach on a production database! Using an initializer to automatically seed a database with test data is often a productive way to develop an application. This is a good approach for early development and when using SQLite.

2. Explicitly modify the schema of the existing database so that it matches the model classes. The advantage of this approach is that you keep your data. You can make this change either manually or by creating a database change script.

3. Use Code First Migrations to update the database schema. We will use this option. 

Delete the Migrations folder and the .db file. Then run the following commands:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
``` 

To learn more about resetting migrations visit [here](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli#resetting-all-migrations) 


## [Validation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-7.0)

Core part of MVC is DRY - Dont Repeat Yourself

We can add Validation Rules to the Model by using Data Annotations to specify Data Types, formatting, etc. Our updates are shown in the `Movie.cs` file.

A significant benefit is that we didn't need to change a single line of code in the `MoviesController` class or in the `Create.cshtml` view in order to enable this validation UI. The controller and views you created earlier in this tutorial automatically picked up the validation rules that you specified by using validation attributes on the properties of the `Movie` model class. Test validation using the `Edit` action method, and the same validation is applied.

### How Validation Works
In the `MoviesController.cs` file, we see two Create functions: one for GET and POST requests. The HttpPost version calls ModelState.IsValid to check for any validation errors in the form. If it fails, it re-displays the form as there was a client-side error (meaning the requestee made a faulty request). If it succeeds, we add the movie into the database and redirects to the proper Index path. If you disable JavaScript in your browser, client-side validation is disabled. You can verify this by testing the HTTP Post `Create` method and stepping through the debugger how validation still works. The Create request redirects back to itself, as Validation rules applied within the `Movie.cs` are applied. This follows the DRY principle since we only need to worry about our logic on one place for it to be used everywhere, reducing redundancy. 

What's really nice about this approach is that neither the controller nor the Create view template knows anything about the actual validation rules being enforced or about the specific error messages displayed. The validation rules and the error strings are specified only in the Movie class. These same validation rules are automatically applied to the Edit view and any other views templates you might create that edit your model.

### DataType Attributes
DataAnnotations provide formatting attributes a built-in set of validation attributes. We see examples with the DataType.Date and DataType.Currency validations for ReleaseDate and Price, respectively.

The ApplyFormatInEditMode setting specifies that the formatting should also be applied when the value is displayed in a text box for editing. (You might not want that for some fields — for example, for currency values, you probably don't want the currency symbol in the text box for editing.)

## [Details and Delete methods of an ASP.NET Core app](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/details?view=aspnetcore-7.0)

### Details
The MVC scaffolding engine adds a comment with an example request for using this method. The URL contains 3 segments, the `Movies` controller, the `Details` method, and an `id` value. These segments are defined in the `Program.cs` file. 

Entity Framework makes it easy to search for data using the `FirstOfDefaultAsync` method. It verifies that a movie can be found before any operations are done. For example, a hacker could introduce errors into the site by changing the URL created by the links from `http://localhost:{PORT}/Movies/Details/1` to something like `http://localhost:{PORT}/Movies/Details/12345` (or some other value that doesn't represent an actual movie). If you didn't check for a null movie, the app would throw an exception. 

### Delete
There are 2 functions for Deletion, A GET `Delete` and a DELETE `DeleteConfirmed`. The GET function returns a view of the movie where you can submit data for a deletion.  Performing a delete operation in response to a GET request (or for that matter, performing an edit operation, create operation, or any other operation that changes data) opens up a __security hole__.

The common language runtime (CLR) requires overloaded methods to havea unique parameter signature(meaning same method name, diff parameters). However, here we have two delete methods (one for GET and POST each) that accept a single integer as a parameter. There are 2 approaches to fixing this problem:
-  We can give the methods different names (as the scaffolding mechanism did). However, this makes a small problem: ASP.NET maps segments of a URL to action methods by name, so if you rename a method, routing normally wouldn't be able to find the method. To remedy this, we add the `ActionName("Delete")` attribute to the `DeleteConfirmed` method. So that the mapping for the routing system will find the `DeleteConfirmed` method under the `Delete` action. 
- We can also artificially change the POST method's signature to include an extra (unused) parameter, resulting in a method signature like: 
```c#
// POST: Movies/Delete/6
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(int id, bool notUsed)
```


