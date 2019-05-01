using System;
using System.Net;
using Reminder.Reciever.Core;
using Telegram.Bot;

namespace Reminder.Reciever.Telegram
{
	public class TelegramReminderReciever : IReminderReciever
	{
		private TelegramBotClient botClient;

		public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

		public TelegramReminderReciever(string token)
		{
			WebProxy wpIndonesia = new WebProxy("202.169.239.66", 8080);
			WebProxy wpMongolia = new WebProxy("202.131.229.10", 8080);
			WebProxy wpGermany = new WebProxy("138.201.223.250", 31288);

			botClient = new TelegramBotClient(token, wpIndonesia);
		}

		public string GetHelloFromBot()
		{
			global::Telegram.Bot.Types.User user = botClient.GetMeAsync().Result;

			return $"Hello from {user.Id}. My name is {user.FirstName} {user.LastName}";
		}

		public void Run()
		{
			throw new NotImplementedException();
		}
	}
}
