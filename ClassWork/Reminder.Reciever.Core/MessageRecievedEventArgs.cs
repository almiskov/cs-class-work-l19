using System;

namespace Reminder.Reciever.Core
{
	public class MessageRecievedEventArgs : EventArgs
	{
		public string ContactId { get; set; }
		public string Message { get; set; }

		public MessageRecievedEventArgs(string contactId, string message)
		{
			Message = message;
			ContactId = contactId;
		}
	}
}