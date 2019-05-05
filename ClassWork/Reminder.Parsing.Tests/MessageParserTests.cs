using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reminder.Parsing.Tests
{
	[TestClass]
	public class MessageParserTests
	{
		#region Users input with time interval parsing and one method for unexpected cases

		[TestMethod]
		[DataRow("����� 5 �")]
		[DataRow("����� 5 ���")]
		[DataRow("����� 5 ������")]
		[DataRow("����� 5 � ������")]
		[DataRow("������ ����� 5 �")]
		[DataRow("����� 5 ��� ������")]
		[DataRow("������ ����� 5 ���")]
		[DataRow("����� 5 ������ ������")]
		[DataRow("������ ����� 5 ������")]
		[DataRow("10 ��� �������� ����� 5 ������")]
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
		[DataRow("����� 5 �")]
		[DataRow("����� 5 ���")]
		[DataRow("����� 5 �����")]
		[DataRow("����� 5 � ������")]
		[DataRow("������ ����� 5 �")]
		[DataRow("����� 5 ��� ������")]
		[DataRow("������ ����� 5 ���")]
		[DataRow("����� 5 ����� ������")]
		[DataRow("������ ����� 5 �����")]
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
		[DataRow("����� 5 �")]
		[DataRow("����� 5 ���")]
		[DataRow("����� 5 �����")]
		[DataRow("����� 5 � ������")]
		[DataRow("������ ����� 5 �")]
		[DataRow("����� 5 ��� ������")]
		[DataRow("������ ����� 5 ���")]
		[DataRow("����� 5 ����� ������")]
		[DataRow("������ ����� 5 �����")]
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
		[DataRow("����� 5 �")]
		[DataRow("����� 5 ����")]
		[DataRow("����� 5 ����")]
		[DataRow("����� 5 � ������")]
		[DataRow("������ ����� 5 �")]
		[DataRow("����� 5 ���� ������")]
		[DataRow("������ ����� 5 ����")]
		[DataRow("����� 5 ���� ������")]
		[DataRow("������ ����� 5 ����")]
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
		[DataRow("����� 5 �")]
		[DataRow("���� 5 ����")]
		[DataRow("����� 5 ����")]
		[DataRow("����� 5  �")] // two spaces
		[DataRow("������ ����� 5 ��")]
		[DataRow("����� 5 � ����� 5 �")]
		[DataRow("������")]
		[DataRow("����� 5 �����")]
		[DataRow("�� 5 ����")]
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
		[DataRow("������, ���! ���. 1 ����� 2 3 ����� 5 �")]
		[DataRow("������, ���! ����� 5 � ���. 1 ����� 2 3 ")]
		[DataRow("����� 3 ��� ������, ���! ���.    1 ����� 2 3 ")]
		[DataRow("������, ����� 5 ����� ���! ���. 1 ����� 2 3 ")]
		[DataRow("������, ���! ���. 1 ����� 2 ����� 5 ������ 3 ")]
		[DataRow("������, ���! ����� 5 ��� ���. 1 ����� 2 3")]
		public void Check_That_TryParseAsTimeInterval_Method_Parses_Message_From_Users_Input_Correctly(string usersInput)
		{
			// arrange
			string expectedMessage = "������, ���! ���. 1 ����� 2 3";

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
		[DataRow("20-12-2019 � 20:00")]
		[DataRow("������ 20-12-2019 20:00")]
		[DataRow("20-12-2019 ������ 20:00")]
		[DataRow("� 20:00 20-12-2019")]
		[DataRow("20:00 ������ 20-12-2019")]
		[DataRow("20:00 20-12-2019 ������")]
		[DataRow("20-12-2019 20:00 ������")]
		[DataRow("������ 20-12-2019 ������ 20:00")]
		[DataRow("20-12-2019 ������ 20:00 ������")]
		[DataRow("������ 20.12.2019 20:00 ������")]
		[DataRow("������ 20-12-2019 ������-������ 20:00 ������")]
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
		[DataRow("20-12-2019 � 20.00")]
		[DataRow("������20-12-2019 20:00")]
		[DataRow("20-12-2019 ������20:00")]
		[DataRow("20-12-201920:00 ������")]
		[DataRow("������ 20:12-2019 ������ 24:00")]
		[DataRow("33-12-2019 ������ 20:00 ������")]
		[DataRow("������ 20.00.2019 20:00 ������")]
		[DataRow("������20-12-2019������-������ 20:00 ������")]
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
		[DataRow("20:00 ������ 1, 34! ��� 20-12-2019 �����-�����")]
		[DataRow("������ 20-12-2019 1, 34! 20:00 ��� �����-�����")]
		[DataRow("������ 1, 34! ��� �����-����� 20:00 20-12-2019")]
		[DataRow("������ 1, 34! ��� 20-12-2019 20:00 �����-�����")]
		[DataRow("������ 1, 20-12-2019 34! ��� 20:00 �����-�����")]
		[DataRow("20-12-2019 ������ 1, 34! ��� �����-����� 20:00")]
		[DataRow("������ 20-12-2019 1, 34! ��� 20:00 �����-�����")]
		[DataRow("������ 1, 20:00 20-12-2019 34! ��� �����-�����")]
		[DataRow("������ 20:00 1, 34! ��� 20-12-2019 �����-�����")]
		[DataRow("20:00 ������ 1, 34! 20-12-2019 ��� �����-�����")]
		public void Check_That_TryParseTimeAndDate_Method_Parses_Message_From_Users_Input_Correctly(string usersInput)
		{
			// arrange
			string expectedMessage = "������ 1, 34! ��� �����-�����";

			// act
			MessageParser.TryParseTimeAndDate(usersInput, out ParsedMessage parsedMessage);

			// assert
			Assert.AreEqual(expectedMessage, parsedMessage.Message);
		}

		#endregion
	}
}
