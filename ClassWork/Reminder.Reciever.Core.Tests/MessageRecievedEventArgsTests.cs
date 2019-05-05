using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reminder.Reciever.Core.Tests
{
	[TestClass]
	public class MessageRecievedEventArgsTests
	{
		[TestMethod]
		public void Check_That_Constructor_Sets_Properties_Correctly()
		{
			string expectedContactId = "1234";
			string expectedMessage = "Hello";

			var eventArgs = new MessageRecievedEventArgs(
				expectedContactId, expectedMessage);

			Assert.AreEqual(expectedContactId, eventArgs.ContactId);
			Assert.AreEqual(expectedMessage, eventArgs.Message);
		}
	}
}
