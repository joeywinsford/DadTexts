using System.Collections.Generic;

namespace DadTexts
{
	public class SmsRetriever
	{
		private readonly InboxScreen _inboxScreen;

		public SmsRetriever(InboxScreen inboxScreen)
		{
			_inboxScreen = inboxScreen;
		}

		public IEnumerable<SmsEntry> GetSmsMessagesFromDevice()
		{
			var inboxUri = Android.Net.Uri.Parse("content://sms");
			var columns = new[] { "_id", "thread_id", "address", "date", "body", "type" };

			using (var cursor = _inboxScreen.ContentResolver.Query(inboxUri, columns, null, null, null, null))
			{
				while (cursor.MoveToNext())
				{
					var sms = new SmsEntry
					{
						Id = cursor.GetString(cursor.GetColumnIndex(columns[0])),
						ThreadId = cursor.GetString(cursor.GetColumnIndex(columns[1])),
						Address = cursor.GetString(cursor.GetColumnIndex(columns[2])),
						Date = cursor.GetString(cursor.GetColumnIndex(columns[3])),
						Message = cursor.GetString(cursor.GetColumnIndex(columns[4])),
						Type = (SmsDirection) int.Parse(cursor.GetString(cursor.GetColumnIndex(columns[5])))
					};
					yield return sms;
				}
			}
		}
	}
}