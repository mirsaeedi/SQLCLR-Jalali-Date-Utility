# SQL CLR Jalali Date Time Utility

The aim of this project is to make it easy for developers to work with jalali (shamsi - شمسی) dates more easily.

* Support .Net Date and Time Formatting (More Information at this page: [Custom Date and Time Format Strings](https://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx) ).
* Developed with C# (SQL CLR)

 ## 💡 Installation

* [Enabling CLR Integration In SQL Server](https://msdn.microsoft.com/en-us/library/ms131048.aspx)

```sql
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'clr enabled', 1;  
GO  
RECONFIGURE;  
GO  
```

* Run the Deployment Scrtip (from Dist\DeploymentScript.sql) to generate functions in your database. Note that you have to run this script in SQLCMD Mode. So From SQL Management Studio, From Query Menu, Select SQLCMD Mode and then execute the query.
Please be sure that you have changed the DatabaseName value to your database name.


## 🕹 Usage

###### ★ Convert Gregorian To Jalali:

Suppose that GETDATE() Method in sql server returns 2016/09/22 15:44

```sql
select dbo.GregorianToJalali(GETDATE(),'yy') -- returns 95
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy') -- returns 1395
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy-MM') -- returns 1395/07
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy-M') -- returns 1395/7
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy/MM/dd') -- returns 1395/07/01
```




