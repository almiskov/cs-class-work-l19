using Reminder.Logger.Core;

namespace Reminder.Logger.Console
{
	public class ConsoleLogger : ILogger
	{
		public void Log(string message)
		{
			System.Console.WriteLine(message);
		}
	}
}
