using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Storage.InMemory;
using Reminder.Domain.Model;
using Moq;
using Reminder.Sender.Core;
using Reminder.Reciever.Core;
using Reminder.Storage.Core;
using Reminder.Logger.Core;
using Reminder.Sender.Telegram;
using System.Net;
using Reminder.Reciever.Telegram;

namespace Reminder.Domain.Tests
{
	[TestClass]
	public class ReminderDomainTests
	{
		private Mock<IReminderStorage> mockStorage = new Mock<IReminderStorage>();
		private Mock<IReminderReciever> mockReciever = new Mock<IReminderReciever>();
		private Mock<IReminderSender> mockSender = new Mock<IReminderSender>();
		private Mock<ILogger> mockLogger = new Mock<ILogger>();

		[TestMethod]
		public void Check_That_Awaiting_Status_Turns_Into_Ready_When_It_Must_Turn()
		{
			// arrange
			ReminderItem reminder = new ReminderItem()
			{
				ContactId = "254536066",
				Date = DateTimeOffset.Now,
				Message = "Test message",
				Status = ReminderItemStatus.Awaiting
			};

			mockStorage.Setup(x => x.Add(reminder));

			// act
			using (var domain = new ReminderDomain(
				mockStorage.Object,
				mockReciever.Object,
				mockSender.Object,
				mockLogger.Object,
				TimeSpan.FromMilliseconds(10),
				TimeSpan.FromMilliseconds(1000)))
			{
				domain.Run();

				Thread.Sleep(50);
			}

			// assert
			Assert.AreEqual(ReminderItemStatus.Ready, reminder.Status);
		}

		[TestMethod]
		public void Check_That_Status_Is_Sent_If_Reminder_Has_Been_Sent()
		{
			// arrange
			ReminderItem reminder = new ReminderItem()
			{
				ContactId = "254536066",
				Date = DateTimeOffset.Now,
				Message = "Test message",
				Status = ReminderItemStatus.Awaiting
			};

			mockStorage.Setup(x => x.Add(reminder));

			// act
			using (var domain = new ReminderDomain(
				mockStorage.Object,
				mockReciever.Object,
				mockSender.Object,
				mockLogger.Object,
				TimeSpan.FromMilliseconds(10),
				TimeSpan.FromMilliseconds(10)))
			{
				domain.Run();

				Thread.Sleep(50);
			}

			// assert
			Assert.AreEqual(ReminderItemStatus.Sent, reminder.Status);
		}

		[TestMethod]
		public void Check_That_SendingSucceded_Raised_If_Reminder_Has_Been_Sent()
		{
			// arrange
			ReminderItem reminder = new ReminderItem()
			{
				ContactId = "254536066",
				Date = DateTimeOffset.Now,
				Message = "Test message",
				Status = ReminderItemStatus.Awaiting
			};

			var storage = new InMemoryReminderStorage();
			storage.Add(reminder);

			bool isCalled = false;

			// act
			using (var domain = new ReminderDomain(
				storage,
				mockReciever.Object,
				mockSender.Object,
				mockLogger.Object,
				TimeSpan.FromMilliseconds(10),
				TimeSpan.FromMilliseconds(10)))
			{
				domain.SendingSucceded += (s, e) => isCalled = true;

				domain.Run();

				Thread.Sleep(50);
			}

			// assert
			Assert.IsTrue(isCalled);
		}

		[TestMethod]
		public void Check_That_Status_Is_Failed_If_Sending_Failed()
		{
			// arrange
			ReminderItem reminder = new ReminderItem()
			{
				ContactId = "254536066",
				Date = DateTimeOffset.Now,
				Message = "Test message",
				Status = ReminderItemStatus.Awaiting
			};

			mockStorage.Setup(x => x.Add(reminder));
			mockSender.Setup(x => x.Send(reminder.ContactId, reminder.Message)).Throws(new Exception());

			// act
			using (var domain = new ReminderDomain(
				mockStorage.Object,
				mockReciever.Object,
				mockSender.Object,
				mockLogger.Object,
				TimeSpan.FromMilliseconds(10),
				TimeSpan.FromMilliseconds(10)))
			{
				domain.Run();

				Thread.Sleep(50);
			}

			// assert
			Assert.AreEqual(ReminderItemStatus.Failed, reminder.Status);
		}

		[TestMethod]
		public void Check_That_SendingFailed_Raised_If_Sending_Failed()
		{
			// arrange
			ReminderItem reminder = new ReminderItem()
			{
				ContactId = "254536066",
				Date = DateTimeOffset.Now,
				Message = "Test message",
				Status = ReminderItemStatus.Awaiting
			};

			var storage = new InMemoryReminderStorage();
			storage.Add(reminder);

			mockSender.Setup(x => x.Send(reminder.ContactId, reminder.Message)).Throws(new Exception());

			bool isCalled = false;

			// act
			using (var domain = new ReminderDomain(
				storage,
				mockReciever.Object,
				mockSender.Object,
				mockLogger.Object,
				TimeSpan.FromMilliseconds(10),
				TimeSpan.FromMilliseconds(10)))
			{
				domain.SendingFailed += (s, e) => isCalled = true;

				domain.Run();

				Thread.Sleep(50);
			}

			// assert
			Assert.IsTrue(isCalled);
		}
	}
}