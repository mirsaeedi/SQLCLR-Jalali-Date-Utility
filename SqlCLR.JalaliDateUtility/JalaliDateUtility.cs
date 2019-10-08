using System;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public class JalaliDateUtility
{
    #region SQL Functions

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static SqlString GregorianToJalali(DateTime? dateTime, string format)
    {
        if (!dateTime.HasValue)
            return null;

        var pc = new PersianCalendar();

        try
        {
            var year = pc.GetYear(dateTime.Value);
            var month = pc.GetMonth(dateTime.Value);
            var dayOfMonth = pc.GetDayOfMonth(dateTime.Value);
            var dayOfWeek = pc.GetDayOfWeek(dateTime.Value);

            var result = "";

            result = FormatSeconds(format, dateTime.Value);
            result = FormatMinutes(result, dateTime.Value);
            result = FormatHours(result, dateTime.Value);
            result = FormatDesignator(result, dateTime.Value);

            result = FormatYears(result, year);
            result = FormatMonths(result, month);
            result = FormatDays(result, dayOfMonth,dayOfWeek);

            return result;
        }
        catch (Exception e)
        {
            return e.ToString();
        }

        return null;
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static SqlDateTime GetJalaliFirstDayOfMonth(DateTime? dateTime)
    {
        if (!dateTime.HasValue)
            return SqlDateTime.Null;

        var pc = new PersianCalendar();

        try
        {
            var year = pc.GetYear(dateTime.Value);
            var month = pc.GetMonth(dateTime.Value);

            return pc.ToDateTime(year, month, 1, 0, 0, 0, 0);

        }
        catch (Exception)
        {
        }

        return SqlDateTime.Null;

    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static SqlDateTime GetJalaliLastDayOfMonth(DateTime? dateTime)
    {
        if (!dateTime.HasValue)
            return SqlDateTime.Null;

        var pc = new PersianCalendar();

        try
        {
            var year = pc.GetYear(dateTime.Value);
            var month = pc.GetMonth(dateTime.Value);
            var lastDay = pc.GetDaysInMonth(year, month);

            return pc.ToDateTime(year, month, lastDay, 0, 0, 0, 0);

        }
        catch (Exception)
        {
        }

        return SqlDateTime.Null;

    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static SqlDateTime JalaliToGregorian(string perisanDateTime, char seperator)
    {
        if (perisanDateTime == null)
            return SqlDateTime.Null;

        var parts = perisanDateTime.Split(seperator);

        var pc = new PersianCalendar();

        try
        {
            var year = int.Parse(parts[0].Length == 2 ? "13" + parts[0] : parts[0]);
            var month = int.Parse(parts[1]);
            var day = int.Parse(parts[2]);

            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
        catch (Exception)
        {
        }

        return SqlDateTime.Null;

    }

    #endregion

    #region Private Functions

    private static string FormatDesignator(string input, DateTime dateTime)
    {
        return input
            .Replace("tt", dateTime.Hour >= 12 ? "ب ظ" : "ق ظ")
            .Replace("t", dateTime.Hour >= 12 ? "ب" : "ق");
    }

    private static string FormatYears(string input, int year)
    {
        return input
            .Replace("yyyy", year.ToString("D4"))
            .Replace("yy", year.ToString().Substring(2, 2));
    }

    private static string FormatMonths(string input, int month)
    {
        return input
            .Replace("MMMM", GetJalaliMonthName(month))
            .Replace("MM", month.ToString("D2"))
            .Replace("M", month.ToString());
    }

    private static string FormatDays(string input, int day, DayOfWeek dayOfWeek)
    {
        return input
            .Replace("dddd", GetJalaliDayName(dayOfWeek))
            .Replace("dd", day.ToString("D2"))
            .Replace("d", day.ToString());
    }

    public static string FormatSeconds(string input, DateTime dateTime)
    {
        return input
            .Replace("ss", dateTime.ToString("ss"))
            .Replace("s", dateTime.ToString("s"));
    }

    public static string FormatHours(string input, DateTime dateTime)
    {
        return input
            .Replace("hh", dateTime.ToString("hh"))
            .Replace("h", dateTime.ToString("%h"))
            .Replace("HH", dateTime.ToString("HH"))
            .Replace("H", dateTime.ToString("%H"));
    }

    public static string FormatMinutes(string input, DateTime dateTime)
    {
        return input
            .Replace("mm", dateTime.ToString("mm"))
            .Replace("m", dateTime.ToString("%m"));
    }

    private static string GetJalaliDayName(DayOfWeek day)
    {
        if (day == DayOfWeek.Saturday)
            return "شنبه";

        if (day == DayOfWeek.Sunday)
            return "یک شنبه";

        if (day == DayOfWeek.Monday)
            return "دوشنبه";

        if (day == DayOfWeek.Tuesday)
            return "سه شنبه";

        if (day == DayOfWeek.Wednesday)
            return "چهارشنبه";

        if (day == DayOfWeek.Thursday)
            return "پنج شنبه";

        if (day == DayOfWeek.Thursday)
            return "جمعه";

        return null;

    }

    private static string GetJalaliMonthName(int month)
    {
        if (month == 1)
            return "فروردین";

        if (month == 2)
            return "اردیبهشت";

        if (month == 3)
            return "خرداد";

        if (month == 4)
            return "تیر";

        if (month == 5)
            return "مرداد";

        if (month == 6)
            return "شهریور";

        if (month == 7)
            return "مهر";

        if (month == 8)
            return "آبان";

        if (month == 9)
            return "آذر";

        if (month == 10)
            return "دی";

        if (month == 11)
            return "بهمن";

        if (month == 12)
            return "اسفند";

        return null;

    }

    #endregion
}
