# Introduction

This repository contains some of the example queries from the book:

> *T-SQL Fundamentals Third Edition* by Itzik Ben-Gan

converted to Entity Framework Core.

# Example database

To create the example database, load the file `TSQLV4.sql` into SSMS and execute it.

You don't have to have full SQL Server installed for this. The `localdb` installation that comes with Visual Studio appears to work fine. In my case, I connect to `localdb` as follows when SSMS launches:

<img src="https://user-images.githubusercontent.com/20816/141017809-d1981648-c9b3-431c-86a5-b238566e80f8.png" width="400">

# Examples

Each converted query is in it's own C# project.

# TSqlEf project

The TSqlEf project contains the models and the database context. This project was setup as follows:

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TSQLV4" Microsoft.EntityFrameworkCore.SqlServer
