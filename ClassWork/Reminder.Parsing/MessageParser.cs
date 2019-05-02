using System;
using System.Text.RegularExpressions;

namespace Reminder.Parsing
{
	public static class MessageParser
	{
		public static ParsedMessage Parse(string message)
		{
			ParsedMessage parsedMessage;

			if (TryParseExact(message, out parsedMessage))
				return parsedMessage;
			if (TryParseAsTimeInterval(message, out parsedMessage))
				return parsedMessage;

			return parsedMessage;
		}

		private static bool TryParseAsTimeInterval(string message, out ParsedMessage parsedMessage)
		{
			Regex regex = new Regex(@"через \d+ [смчд](\w*)", RegexOptions.IgnoreCase);

			if (regex.IsMatch(message) && regex.Matches(message).Count == 1)
			{
				parsedMessage = new ParsedMessage();

				// Getting message from input message and setting it

				string parsingMessage = regex.Replace(message, string.Empty).Trim();

				parsedMessage.Message =
					string.IsNullOrWhiteSpace(parsingMessage)
					? "Something"
					: parsingMessage;

				// Getting target date-time from input message and setting it

				string parsingInterval = regex.Match(message).Value;

				double interval = Convert.ToDouble(
					Regex.Match(message, @"\d+").Value);

				char unit = parsingInterval.Split(' ')[2][0]; // жёстко, но для этого паттерна норм =)

				TimeSpan timeSpan = TimeSpan.Zero;

				switch (unit)
				{
					case 'с':
						timeSpan = TimeSpan.FromSeconds(interval);
						break;
					case 'м':
						timeSpan = TimeSpan.FromMinutes(interval);
						break;
					case 'ч':
						timeSpan = TimeSpan.FromHours(interval);
						break;
					case 'д':
						timeSpan = TimeSpan.FromDays(interval);
						break;
				}

				parsedMessage.Date = DateTimeOffset.Now + timeSpan;

				return true;
			}
			else
			{
				parsedMessage = null;

				return false;
			}
		}

		private static bool TryParseExact(string message, out ParsedMessage parsedMessage)
		{
			parsedMessage = null;

			if (string.IsNullOrWhiteSpace(message))
				return false;

			int firstSpacePosition = message.IndexOf(" ");

			if (firstSpacePosition <= 0)
				return false;

			string firstWord = message.Substring(0, firstSpacePosition);

			bool dateIsOk = DateTimeOffset.TryParse(firstWord, out var date);

			if (!dateIsOk)
				return false;

			parsedMessage = new ParsedMessage()
			{
				Date = date,
				Message = message.Substring(firstSpacePosition).Trim()
			};

			return true;
		}
	}
}
