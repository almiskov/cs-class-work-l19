using System;
using System.Net;
using Reminder.Domain;
using Reminder.Reciever.Telegram;
using Reminder.Sender.Telegram;
using Reminder.Storage.InMemory;

namespace ReminderApp
{
	class Program
	{
		static void Main(string[] args)
		{
			const string token = "ххх";

			WebProxy wpIndonesia = new WebProxy("202.169.239.66", 8080);
			WebProxy wpMongolia = new WebProxy("202.131.229.10", 8080);
			WebProxy wpGermany = new WebProxy("138.201.223.250", 31288);

			var reciever = new TelegramReminderReciever(token, wpIndonesia);
			var sender = new TelegramReminderSender(token, wpIndonesia);
			var storage = new InMemoryReminderStorage();

			var domain = new ReminderDomain(storage, reciever, sender);

			domain.AddingSucceded += (s, e) =>
				Console.WriteLine(
					$"Reminder \"{e.Reminder.Message}\" added at {e.Reminder.Date} from {e.Reminder.ContactId}");

			domain.SendingSucceded += (s, e) =>
				Console.WriteLine(
					$"Reminder \"{e.Reminder.Message}\" for {e.Reminder.ContactId} has sent");

			domain.SendingFailed += (s, e) =>
				Console.WriteLine(
					$"Reminder \"{e.Reminder.Message}\" for {e.Reminder.ContactId} has not been sent. Exception: {e.Exception}");

			domain.Run();

			Console.ReadLine();
		}
	}
}
