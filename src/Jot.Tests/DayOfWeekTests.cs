using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Andtech.Jot.Tests
{

	public class DayOfWeekTests
	{

		private static readonly List<DayOfWeek> Days = new() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };

		[Test]
		public void ParseDayOfWeek()
		{
			Assert.AreEqual(DayOfWeek.Monday, DayUtility.ParseDayOfWeek("monday"));
			Assert.AreEqual(DayOfWeek.Tuesday, DayUtility.ParseDayOfWeek("tuesday"));
			Assert.AreEqual(DayOfWeek.Wednesday, DayUtility.ParseDayOfWeek("wednesday"));
			Assert.AreEqual(DayOfWeek.Thursday, DayUtility.ParseDayOfWeek("thursday"));
			Assert.AreEqual(DayOfWeek.Friday, DayUtility.ParseDayOfWeek("friday"));
			Assert.AreEqual(DayOfWeek.Saturday, DayUtility.ParseDayOfWeek("saturday"));
			Assert.AreEqual(DayOfWeek.Sunday, DayUtility.ParseDayOfWeek("sunday"));
		}

		[Test]
		public void ParseDayOfWeekShort()
		{
			Assert.AreEqual(DayOfWeek.Monday, DayUtility.ParseDayOfWeek("mon"));
			Assert.AreEqual(DayOfWeek.Tuesday, DayUtility.ParseDayOfWeek("tue"));
			Assert.AreEqual(DayOfWeek.Wednesday, DayUtility.ParseDayOfWeek("wed"));
			Assert.AreEqual(DayOfWeek.Thursday, DayUtility.ParseDayOfWeek("thu"));
			Assert.AreEqual(DayOfWeek.Thursday, DayUtility.ParseDayOfWeek("thur"));
			Assert.AreEqual(DayOfWeek.Thursday, DayUtility.ParseDayOfWeek("thurs"));
			Assert.AreEqual(DayOfWeek.Friday, DayUtility.ParseDayOfWeek("fri"));
			Assert.AreEqual(DayOfWeek.Saturday, DayUtility.ParseDayOfWeek("sat"));
			Assert.AreEqual(DayOfWeek.Sunday, DayUtility.ParseDayOfWeek("sun"));
		}

		[Test]
		public void ParseDayOfWeekSpecial()
		{
			Assert.AreEqual(DayOfWeek.Tuesday, DayUtility.ParseDayOfWeek("tues"));
			Assert.AreEqual(DayOfWeek.Thursday, DayUtility.ParseDayOfWeek("thur"));
			Assert.AreEqual(DayOfWeek.Thursday, DayUtility.ParseDayOfWeek("thurs"));
		}

		[Test]
		public void ParseThisDay()
		{
			AreEqual(-1);
			AreEqual(-2);
			AreEqual(-3);
			AreEqual(-4);
			AreEqual(-5);
			AreEqual(-6);

			void AreEqual(int offset)
			{
				var today = DateTime.Today.DayOfWeek;
				var i = Days.IndexOf(today);
				var j = (i + Days.Count + offset) % Days.Count;
				var other = Days[j];

				var actual = DayUtility.ParseDate($"{other}");
				var timeSpan = actual.Subtract(DateTime.Today);

				Assert.AreEqual(offset, timeSpan.Days);
			}
		}

		[Test]
		public void ParseLastDay()
		{
			AreEqual(-7);
			AreEqual(-8);
			AreEqual(-9);
			AreEqual(-10);
			AreEqual(-11);
			AreEqual(-12);
			AreEqual(-13);

			void AreEqual(int offset)
			{
				var today = DateTime.Today.DayOfWeek;
				var i = Days.IndexOf(today);
				var j = (i + 2 * Days.Count + offset) % Days.Count;
				var other = Days[j];

				var actual = DayUtility.ParseDate($"last {other}");
				var timeSpan = actual.Subtract(DateTime.Today);

				Assert.AreEqual(offset, timeSpan.Days);
			}
		}
	}
}
