using System;
using System.Collections.Generic;

namespace Andtech.Jot
{

	public enum Month
	{
		January,
		February,
		March,
		April,
		May,
		June,
		July,
		August,
		September,
		October,
		November,
		December
	}

	public static class Constants
	{
		public static readonly List<string> Months = new List<string>
		{
			"January",
			"February",
			"March",
			"April",
			"May",
			"June",
			"July",
			"August",
			"September",
			"October",
			"November",
			"December"
		};
		private static readonly List<DayOfWeek> Days = new List<DayOfWeek>() {
			DayOfWeek.Monday,
			DayOfWeek.Tuesday,
			DayOfWeek.Wednesday,
			DayOfWeek.Thursday,
			DayOfWeek.Friday,
			DayOfWeek.Saturday,
			DayOfWeek.Sunday
		};

		public static Month GetMonth(int index) => (Month)index;

		public static string GetDisplayMonthAbbreviated(Month month)
		{
			switch (month)
			{
				case Month.January:
					return "Jan";
				case Month.February:
					return "Feb";
				case Month.March:
					return "Mar";
				case Month.April:
					return "Apr";
				case Month.May:
					return "May";
				case Month.June:
					return "June";
				case Month.July:
					return "July";
				case Month.August:
					return "Aug";
				case Month.September:
					return "Sep";
				case Month.October:
					return "Oct";
				case Month.November:
					return "Nov";
				case Month.December:
					return "Dec";
			}

			return default;
		}

		public static string GetDisplayDayOfWeekAbbreviated(DayOfWeek dayOfweek) => dayOfweek.ToString().Substring(0, 3);
	}
}
