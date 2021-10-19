using NUnit.Framework;
using System;

namespace Andtech.Jot.Tests
{
	public class SpecialDateTests
	{

		[Test]
		public void ParseToday()
		{
			var actual = DayUtility.ParseDate("today");

			var timeSpan = actual.Subtract(DateTime.Today);

			Assert.AreEqual(0, timeSpan.Days);
		}

		[Test]
		public void PerseYesterday()
		{
			var actual = DayUtility.ParseDate("yesterday");

			var timeSpan = actual.Subtract(DateTime.Today);

			Assert.AreEqual(-1, timeSpan.Days);
		}

		[Test]
		public void ParseTomorrow()
		{
			var actual = DayUtility.ParseDate("tomorrow");

			var timeSpan = actual.Subtract(DateTime.Today);

			Assert.AreEqual(1, timeSpan.Days);
		}
	}
}
