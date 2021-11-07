# Introduction

This repository contains some of the example queries from the book:

> *T-SQL Fundamentals Third Edition* by Itzik Ben-Gan

converted to Entity Framework Core.

# Example database

To create the example database, load the file `TSQLV4.sql` into SSMS and execute it.

# Examples

Each converted query is in it's own C# project.

# TSqlEf project

The TSqlEf project contains the models and the database context. This project was setup as follows:

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TSQLV4" Microsoft.EntityFrameworkCore.SqlServer
