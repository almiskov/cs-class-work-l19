using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Storage.InMemory.EventArgs;

namespace Reminder.Storage.Core.Tests
{
	[TestClass]
	public class StatusUpdatedEventArgsTests
	{
		[TestMethod]
		public void Check_That_Constructor_Sets_Properties_Correctly()
		{
			ReminderItem expectedReminder = new ReminderItem();

			var eventArgs = new StatusUpdatedEventArgs(expectedReminder);

			Assert.AreEqual(expectedReminder, eventArgs.Reminder);
		}
	}
}
