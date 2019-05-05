using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reminder.Sender.Telegram.Tests
{
	[TestClass]
	public class TelegramReminderSenderTests
	{
		string tokenExample = "1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy";

		[TestMethod]
		public void Check_That_Constructor_Initializes_TelegramBotClient()
		{
			var sender = new TelegramReminderSender(tokenExample);

			Assert.IsNotNull(sender.botClient);
		}
	}
}
