using Chronic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Andtech.Jot
{

	public static class DayUtility
	{

		private static readonly string[] daysOfWeek = new string[] { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
		private static readonly string[] shortDaysOfWeek = new string[] { "mon", "tue", "wed", "thu", "fri", "sat", "sun" };
		private static readonly string[] specialDaysOfWeek = new string[] { "tues", "thur", "thurs" };
		private static readonly Regex thisDayRegex;
		private static readonly Regex lastDayRegex;

		static DayUtility()
		{
			var codes = daysOfWeek.Select(AppendPlural)
				.Concat(shortDaysOfWeek.Select(AppendPlural))
				.Concat(specialDaysOfWeek);
			var dayExpression = string.Join("|", codes);

			thisDayRegex = new Regex($@"^({dayExpression})$");
			lastDayRegex = new Regex($@"^last (?<day>({dayExpression}))$");

			string AppendPlural(string x) => $"{x}s?";
		}

		public static DateTime ParseDate(string value)
		{
			value = value.ToLowerInvariant();
			value = value.Trim();

			var thisDayMatch = thisDayRegex.Match(value);
			if (thisDayMatch.Success)
			{
				value = $"last {value}";
			}
			else
			{
				var lastDayMatch = lastDayRegex.Match(value);
				if (lastDayMatch.Success)
				{
					value = $"2 weeks ago {lastDayMatch.Groups["day"]}";
				}
			}

			var parser = new Parser();
			Span parseObj;
			DateTime parsedDateTime;
			parseObj = parser.Parse(value);
			parsedDateTime = parseObj.Start ?? default;

			return parsedDateTime;
		}

		public static DayOfWeek ParseDayOfWeek(string value)
		{
			value = value.ToLowerInvariant();
			value = value.Trim();

			switch (value)
			{
				case "monday":
				case "mon":
					return DayOfWeek.Monday;
				case "tuesday":
				case "tue":
				case "tues":
					return DayOfWeek.Tuesday;
				case "wednesday":
				case "wed":
					return DayOfWeek.Wednesday;
				case "thursday":
				case "thu":
				case "thur":
				case "thurs":
					return DayOfWeek.Thursday;
				case "friday":
				case "fri":
					return DayOfWeek.Friday;
				case "saturday":
				case "sat":
					return DayOfWeek.Saturday;
				case "sunday":
				case "sun":
					return DayOfWeek.Sunday;
			}

			return default;
		}
	}
}
