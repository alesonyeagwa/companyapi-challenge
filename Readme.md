# Company API

## About
This is a simple API application developed with ASP.NET Core with the target framework being .NET 5.0. It features user account creation, token-based authorization, and the creation and management of company details.

## All Features
- User account registration
- Creation and management of 
- Access to published posts by other users
- Create and manage comments on published posts
- Access to all published posts by a user
- Access to all published comments by a user

## Installation
- Clone repository
```
$ git clone https://github.com/alesonyeagwa/companyapi-challenge.git
```
This project targets .NET 5.0 which is not available on some visual studio versions. A simple and short guide to installing the latest preview version of VStudio can be seen [here] (https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-5-preview-1/)

Packages used should install automatically after the project is opened.

A local DB should be created; This can be done with the following steps:
1. Open Visual Studio 2019
2. Go to `View` > `SQL Server Object Explorer`
3. Drilldown to `SQL Server` > `(localdb)\MSSQLLocalDB`
4. Right-click "`Database`" Folder
5. Click "`Add New Database`"
6. Name it as "`TestDB`" (or a preferred name; the appsettings.json file must be updated accordingly) and click OK

You may choose to use a different DB server. In that case, the connection string in appsettings.json under the CompanyAPI project must be updated correctly.

After the DB has been set up, to generate the database tables for the project, open up the Package Manager Console by going to `Tools` > `NuGet Package Manager` > `Package Manager Console`. In the PM console enter:

```
$ Update-Database
```
This should run the existing migrations and then create the tables in the database after `Done` is shown in the console.

Tests can then be executed by going to `Test` > `Run all tests` to ensure that the setup is complete (All tests should succeed).

The default project (CompanyAPI) can then be executed and a browser window should open up with the URL to the API.

Swagger has been set up with this project, therefore, navigating to `https://localhost:<port>/swagger` should present a simple API documentation for this project.

A simple client (written with VueJS) to interact with the API can be found [here] (https://github.com/alesonyeagwa/companyapi-client-challenge)

## Issues

If you discover an issue within this project, please you're more than welcome to submit a pull request, or if you're not feeling up to it - create an issue so someone else can pick it up.

## License

This project is an open-sourced software licensed under the [MIT license](https://opensource.org/licenses/MIT).
