using Reminder.Sender.Core;
using System.Net;
using Telegram.Bot;

namespace Reminder.Sender.Telegram
{
	public class TelegramReminderSender : IReminderSender
	{
		internal TelegramBotClient botClient;

		public TelegramReminderSender(string token, WebProxy webProxy = null)
		{
			botClient = new TelegramBotClient(token, webProxy);
		}

		public void Send(string contactId, string message)
		{
			var chatId = new global::Telegram.Bot.Types.ChatId(long.Parse(contactId));

			botClient.SendTextMessageAsync(chatId, message);
		}
	}
}
