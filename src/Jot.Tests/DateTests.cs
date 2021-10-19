using NUnit.Framework;
using System;

namespace Andtech.Jot.Tests
{
	public class DateTests
	{

		[Test]
		public void ParseNumerical()
		{
			var actual = DayUtility.ParseDate("2021/8/14");
			var expected = new DateTime(2021, 8, 14);

			Assert.AreEqual(expected, actual);
		}
	}
}
