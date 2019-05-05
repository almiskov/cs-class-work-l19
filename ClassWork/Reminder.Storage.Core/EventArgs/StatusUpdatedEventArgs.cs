using Reminder.Storage.Core;

namespace Reminder.Storage.InMemory.EventArgs
{
	public class StatusUpdatedEventArgs : System.EventArgs
	{
		public ReminderItem Reminder { get; set; }

		public StatusUpdatedEventArgs(ReminderItem reminderItem)
		{
			Reminder = reminderItem;
		}
	}
}
