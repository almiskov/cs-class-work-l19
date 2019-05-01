using System;

namespace Reminder.Reciever.Core
{
	public interface IReminderReciever
	{
		void Run();
		event EventHandler<MessageRecievedEventArgs> MessageRecieved;
	}
}
