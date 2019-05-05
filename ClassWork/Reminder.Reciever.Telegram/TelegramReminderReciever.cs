using System;
using System.Net;
using Reminder.Reciever.Core;
using Telegram.Bot;

namespace Reminder.Reciever.Telegram
{
	public class TelegramReminderReciever : IReminderReciever
	{
		internal TelegramBotClient botClient;

		public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

		public TelegramReminderReciever(string token, WebProxy webProxy = null)
		{
			botClient = new TelegramBotClient(token, webProxy);
		}

		public void Run()
		{
			botClient.OnMessage += BotClient_OnMessage;
			botClient.StartReceiving();
		}

		private void BotClient_OnMessage(object sender, global::Telegram.Bot.Args.MessageEventArgs e)
		{
			if(e.Message.Type == global::Telegram.Bot.Types.Enums.MessageType.Text)
			{
				OnMessageRecieved(
					this,
					new MessageRecievedEventArgs(
						e.Message.Chat.Id.ToString(),
						e.Message.Text));
			}
		}

		protected internal virtual void OnMessageRecieved(object sender, MessageRecievedEventArgs e)
		{
			MessageRecieved?.Invoke(sender, e);
		}
	}
}
