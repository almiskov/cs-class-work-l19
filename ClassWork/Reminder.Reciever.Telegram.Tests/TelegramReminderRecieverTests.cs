using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reminder.Reciever.Telegram.Tests
{
	[TestClass]
	public class TelegramReminderRecieverTests
	{
		string tokenExample = "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy";

		[TestMethod]
		public void Check_That_Constructor_Initializes_TelegramBotClient()
		{
			TelegramReminderReciever reciever = new TelegramReminderReciever(tokenExample);

			Assert.IsNotNull(reciever.botClient);
		}

		[TestMethod]
		public void Check_That_OnMessageRecieved_Method_Calls_MessageRecieved_Event()
		{
			var reciever = new TelegramReminderReciever(tokenExample);

			bool eventHandlerCalled = false;

			reciever.MessageRecieved += (s, e) => eventHandlerCalled = true;

			reciever.OnMessageRecieved(
				null,
				new Core.MessageRecievedEventArgs(
					string.Empty,
					string.Empty));

			Assert.IsTrue(eventHandlerCalled);
		}

		[TestMethod]
		public void Check_That_Run_Method_Make_BotClient_StartRecieving()
		{
			var reciever = new TelegramReminderReciever(tokenExample);

			reciever.Run();

			bool isBotClientReceiving = reciever.botClient.IsReceiving;

			Assert.IsTrue(isBotClientReceiving);
		}
	}
}
