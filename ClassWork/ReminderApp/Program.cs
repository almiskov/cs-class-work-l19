using System;
using Reminder.Reciever.Telegram;

namespace ReminderApp
{
	class Program
	{
		static void Main(string[] args)
		{
			const string token = "xxx";

			var reciever = new TelegramReminderReciever(token);

			Console.WriteLine(reciever.GetHelloFromBot());
		}
	}
}
