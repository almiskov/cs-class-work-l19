using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Reminder.Parsing.Tests
{
	[TestClass]
	public class ParsedMessageTests
	{
		[TestMethod]
		public void Check_That_Constructor_Sets_Properties_Correctly()
		{
			DateTimeOffset expectedDate = DateTimeOffset.MinValue;
			string expectedMessage = "Hello";

			var parsedMessage = new ParsedMessage()
			{
				Date = expectedDate,
				Message = expectedMessage
			};

			Assert.AreEqual(expectedDate, parsedMessage.Date);
			Assert.AreEqual(expectedMessage, parsedMessage.Message);
		}
	}
}
