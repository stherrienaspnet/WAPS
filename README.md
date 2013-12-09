WAPS
====

.NET WebApi Angular Performance Seed where performance is the primary goal. 
For this seed application we do not intend to use Entity Framework because of the performance concern and prefer to use only Stored procedure. The data mapping is executed by Dapper as its perform so fast compare to other competitor.

The starting point of this project came from a really nice article at codeproject from Mister Patelsan.
http://www.codeproject.com/Articles/630986/Cross-Platform-Authentication-With-ASP-NET-Web-API

Here is a list of component actually used in this seed:

SimpleInjector: a very fast IOC Container, 
Dapper: a very Fast ORM,
Angular.js: a very Fast & Powerfull UI Framework,
SQL Server - Stored Procedure: a very Powerfull query execution mecanism,
D3.js - NVD3: a very powerfull Javascript Graphic Library (will be used soon)



