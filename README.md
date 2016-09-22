# SQL CLR Jalali Date Time Utility

The aim of this project is to make it more easier for developers to work with jalali (shamsi - شمسی) dates.

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

* Run the Deployment Script in SQL Server Management Studio (from SqlCLR.JalaliDateUtility\Dist\DeploymentScript.sql) to generate functions in your database. Note that you have to run this script in SQLCMD Mode. So From SQL Management Studio, From Query Menu, Select SQLCMD Mode and then execute the query.
Please be sure that you have changed the DatabaseName value to your database name.


## 🕹 Usage

###### ★ Convert Gregorian To Jalali:

Suppose that GETDATE() Method in sql server returns 2016/09/22 15:04:33

```sql
select dbo.GregorianToJalali(GETDATE(),'yy') -- returns 95
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy') -- returns 1395
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy-MM') -- returns 1395-07
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy-M') -- returns 1395-7
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy/MM/dd') -- returns 1395/07/01
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy/MM/dd hh:mm tt') -- returns 1395/07/01 03:04 ب ظ
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy/MM/dd hh:mm:ss tt') -- returns 1395/07/01 03:04:33 ب ظ
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy/MM/dd HH:mm') -- returns 1395/07/01 15:04
```

```sql
select dbo.GregorianToJalali(GETDATE(),'yyyy MMMM dddd') -- returns 1395 پنج شنبه مهر
```


###### ★ Convert Jajali Date To Gregorian:

```sql
select dbo.JalaliToGregorian('95-06-11','-') --returns 2016-09-01 00:00:00.000
```

```sql
select dbo.JalaliToGregorian('1395/06/11','/') --returns 2016-09-01 00:00:00.000
```

###### ★ Some times you need to have the first and last day of a persian month in gregorian date (specially in reporting)

```sql
select dbo.GetJalaliLastDayOfMonth(GETDATE()) --returns 2016-10-21 00:00:00.000 which is equal to 1395/07/30
```

```sql
select dbo.GetJalaliFirstDayOfMonth(GETDATE()) --returns 2016-09-22 00:00:00.000 which is equal to 1395/07/01
```
