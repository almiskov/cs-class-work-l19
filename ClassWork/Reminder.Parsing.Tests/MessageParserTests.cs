using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reminder.Parsing.Tests
{
	[TestClass]
	public class MessageParserTests
	{
		#region Users input with time interval parsing and one method for unexpected cases

		[TestMethod]
		[DataRow("через 5 с")]
		[DataRow("через 5 сек")]
		[DataRow("через 5 секунд")]
		[DataRow("через 5 с привет")]
		[DataRow("привет через 5 с")]
		[DataRow("через 5 сек привет")]
		[DataRow("привет через 5 сек")]
		[DataRow("через 5 секунд привет")]
		[DataRow("привет через 5 секунд")]
		[DataRow("10 раз отжаться через 5 секунд")]
		public void Check_That_TryParseAsTimeInterval_Method_Parses_DateTime_From_Users_Input_With_Seconds_Correctly(string usersInput)
		{
			// act
			DateTimeOffset minDateTime = DateTimeOffset.Now + TimeSpan.FromSeconds(5);

			MessageParser.TryParseAsTimeInterval(usersInput, out ParsedMessage parsedMessage);

			DateTimeOffset maxDateTime = DateTimeOffset.Now + TimeSpan.FromSeconds(5);

			// assert
			bool isDateTimeInExpectedInterval =
				parsedMessage.Date > minDateTime &&
				parsedMessage.Date < maxDateTime;

			Assert.IsTrue(isDateTimeInExpectedInterval);
		}

		[TestMethod]
		[DataRow("через 5 м")]
		[DataRow("через 5 мин")]
		[DataRow("через 5 минут")]
		[DataRow("через 5 м привет")]
		[DataRow("привет через 5 м")]
		[DataRow("через 5 мин привет")]
		[DataRow("привет через 5 мин")]
		[DataRow("через 5 минут привет")]
		[DataRow("привет через 5 минут")]
		public void Check_That_TryParseAsTimeInterval_Method_Parses_DateTime_From_Users_Input_With_Minutes_Correctly(string usersInput)
		{
			// act
			DateTimeOffset minDateTime = DateTimeOffset.Now + TimeSpan.FromMinutes(5);

			MessageParser.TryParseAsTimeInterval(usersInput, out ParsedMessage parsedMessage);

			DateTimeOffset maxDateTime = DateTimeOffset.Now + TimeSpan.FromMinutes(5);

			// assert
			bool isDateTimeInExpectedInterval =
				parsedMessage.Date > minDateTime &&
				parsedMessage.Date < maxDateTime;

			Assert.IsTrue(isDateTimeInExpectedInterval);
		}

		[TestMethod]
		[DataRow("через 5 ч")]
		[DataRow("через 5 час")]
		[DataRow("через 5 часов")]
		[DataRow("через 5 ч привет")]
		[DataRow("привет через 5 ч")]
		[DataRow("через 5 час привет")]
		[DataRow("привет через 5 час")]
		[DataRow("через 5 часов привет")]
		[DataRow("привет через 5 часов")]
		public void Check_That_TryParseAsTimeInterval_Method_Parses_DateTime_From_Users_Input_With_Hours_Correctly(string usersInput)
		{
			// act
			DateTimeOffset minDateTime = DateTimeOffset.Now + TimeSpan.FromHours(5);

			MessageParser.TryParseAsTimeInterval(usersInput, out ParsedMessage parsedMessage);

			DateTimeOffset maxDateTime = DateTimeOffset.Now + TimeSpan.FromHours(5);

			// assert
			bool isDateTimeInExpectedInterval =
				parsedMessage.Date > minDateTime &&
				parsedMessage.Date < maxDateTime;

			Assert.IsTrue(isDateTimeInExpectedInterval);
		}

		[TestMethod]
		[DataRow("через 5 д")]
		[DataRow("через 5 день")]
		[DataRow("через 5 дней")]
		[DataRow("через 5 д привет")]
		[DataRow("привет через 5 д")]
		[DataRow("через 5 день привет")]
		[DataRow("привет через 5 день")]
		[DataRow("через 5 дней привет")]
		[DataRow("привет через 5 дней")]
		public void Check_That_TryParseAsTimeInterval_Method_Parses_DateTime_From_Users_Input_With_Days_Correctly(string usersInput)
		{
			// act
			DateTimeOffset minDateTime = DateTimeOffset.Now + TimeSpan.FromDays(5);

			MessageParser.TryParseAsTimeInterval(usersInput, out ParsedMessage parsedMessage);

			DateTimeOffset maxDateTime = DateTimeOffset.Now + TimeSpan.FromDays(5);

			// assert
			bool isDateTimeInExpectedInterval =
				parsedMessage.Date > minDateTime &&
				parsedMessage.Date < maxDateTime;

			Assert.IsTrue(isDateTimeInExpectedInterval);
		}

		[TestMethod]
		[DataRow("через 5 н")]
		[DataRow("черз 5 день")]
		[DataRow("через 5 длей")]
		[DataRow("через 5  д")] // two spaces
		[DataRow("привет через 5 се")]
		[DataRow("через 5 д через 5 д")]
		[DataRow("привет")]
		[DataRow("через 5 чесов")]
		[DataRow("чз 5 дней")]
		public void Check_That_TryParseAsTimeInterval_Method_Returns_False_And_NULL_From_Unexpected_Users_Input(string usersInput)
		{
			// act
			bool mustBeFalse = MessageParser.TryParseAsTimeInterval(usersInput, out ParsedMessage parsedMessage);

			// assert
			Assert.IsNull(parsedMessage);
			Assert.IsFalse(mustBeFalse);
		}

		#endregion

		#region  Users input with time interval gives expected message

		[TestMethod]
		[DataRow("Привет, раз! два. 1 через 2 3 через 5 с")]
		[DataRow("Привет, раз! через 5 с два. 1 через 2 3 ")]
		[DataRow("через 3 дня Привет, раз! два.    1 через 2 3 ")]
		[DataRow("Привет, через 5 минут раз! два. 1 через 2 3 ")]
		[DataRow("Привет, раз! два. 1 через 2 через 5 секунд 3 ")]
		[DataRow("Привет, раз! через 5 сек два. 1 через 2 3")]
		public void Check_That_TryParseAsTimeInterval_Method_Parses_Message_From_Users_Input_Correctly(string usersInput)
		{
			// arrange
			string expectedMessage = "Привет, раз! два. 1 через 2 3";

			// act
			MessageParser.TryParseAsTimeInterval(usersInput, out ParsedMessage parsedMessage);
			
			// assert
			Assert.AreEqual(expectedMessage, parsedMessage.Message);
		}

		#endregion

		#region Expected users input with date and time parsing and one method for unexpected cases

		[TestMethod]
		[DataRow("20-12-2019-20:00")]
		[DataRow("20-12-2019 20:00")]
		[DataRow("20-12-2019 в 20:00")]
		[DataRow("Привет 20-12-2019 20:00")]
		[DataRow("20-12-2019 Привет 20:00")]
		[DataRow("в 20:00 20-12-2019")]
		[DataRow("20:00 Привет 20-12-2019")]
		[DataRow("20:00 20-12-2019 Привет")]
		[DataRow("20-12-2019 20:00 привет")]
		[DataRow("Привет 20-12-2019 Привет 20:00")]
		[DataRow("20-12-2019 Привет 20:00 привет")]
		[DataRow("Привет 20.12.2019 20:00 привет")]
		[DataRow("Привет 20-12-2019 Привет-привет 20:00 привет")]
		public void Check_That_TryParseTimeAndDate_Method_Parses_DateTime_From_Users_Input_Correctly(string usersInput)
		{
			// arrange
			DateTimeOffset expectedDate = DateTimeOffset.Parse("2019-12-20T20:00");

			// act
			MessageParser.TryParseTimeAndDate(usersInput, out ParsedMessage parsedMessage);

			// assert
			Assert.AreEqual(expectedDate, parsedMessage.Date);
		}

		[TestMethod]
		[DataRow("20-12-2019")]
		[DataRow("20:00")]
		[DataRow("20-12-2019 в 20.00")]
		[DataRow("Привет20-12-2019 20:00")]
		[DataRow("20-12-2019 Привет20:00")]
		[DataRow("20-12-201920:00 привет")]
		[DataRow("Привет 20:12-2019 Привет 24:00")]
		[DataRow("33-12-2019 Привет 20:00 привет")]
		[DataRow("Привет 20.00.2019 20:00 привет")]
		[DataRow("Привет20-12-2019Привет-привет 20:00 привет")]
		public void Check_That_TryParseTimeAndDate_Method_Returns_False_And_NULL_If_Input_Is_Incorrect(string usersInput)
		{
			// act
			bool mustBeFalseBool = MessageParser.TryParseTimeAndDate(usersInput, out ParsedMessage parsedMessage);

			// assert
			Assert.IsNull(parsedMessage);
			Assert.IsFalse(mustBeFalseBool);
		}

		#endregion

		#region Users input with date and time gives expected message

		[TestMethod]
		[DataRow("20:00 Привет 1, 34! лес 20-12-2019 почва-трава")]
		[DataRow("Привет 20-12-2019 1, 34! 20:00 лес почва-трава")]
		[DataRow("Привет 1, 34! лес почва-трава 20:00 20-12-2019")]
		[DataRow("Привет 1, 34! лес 20-12-2019 20:00 почва-трава")]
		[DataRow("Привет 1, 20-12-2019 34! лес 20:00 почва-трава")]
		[DataRow("20-12-2019 Привет 1, 34! лес почва-трава 20:00")]
		[DataRow("Привет 20-12-2019 1, 34! лес 20:00 почва-трава")]
		[DataRow("Привет 1, 20:00 20-12-2019 34! лес почва-трава")]
		[DataRow("Привет 20:00 1, 34! лес 20-12-2019 почва-трава")]
		[DataRow("20:00 Привет 1, 34! 20-12-2019 лес почва-трава")]
		public void Check_That_TryParseTimeAndDate_Method_Parses_Message_From_Users_Input_Correctly(string usersInput)
		{
			// arrange
			string expectedMessage = "Привет 1, 34! лес почва-трава";

			// act
			MessageParser.TryParseTimeAndDate(usersInput, out ParsedMessage parsedMessage);

			// assert
			Assert.AreEqual(expectedMessage, parsedMessage.Message);
		}

		#endregion
	}
}
