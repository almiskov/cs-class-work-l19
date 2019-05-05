using System;
using System.Net;
using Reminder.Domain;
using Reminder.Logger.Console;
using Reminder.Reciever.Telegram;
using Reminder.Sender.Telegram;
using Reminder.Storage.InMemory;

namespace ReminderApp
{
	class Program
	{
		static void Main(string[] args)
		{
			const string token = "xxx";

			var wpIndonesia = new WebProxy("202.169.239.66", 8080);
			var wpMongolia = new WebProxy("202.131.229.10", 8080);
			var wpGermany = new WebProxy("138.201.223.250", 31288);

			var reciever = new TelegramReminderReciever(token, wpIndonesia);
			var sender = new TelegramReminderSender(token, wpIndonesia);
			var storage = new InMemoryReminderStorage();
			var logger = new ConsoleLogger();

			var domain = new ReminderDomain(storage, reciever, sender, logger);

			domain.Run();

			Console.ReadLine();
		}
	}
}
