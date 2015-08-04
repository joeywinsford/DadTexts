using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Runtime;
using Android.OS;
using Android.Provider;


namespace DadTexts
{
	[Activity(Label = "Dad Texts", MainLauncher = true, Icon = "@drawable/icon")]
	public class InboxScreen : ListActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			ListAdapter = new InboxScreenAdapter(this, GetSmsMessagesFromDevice().ToList());
			ListView.FastScrollEnabled = true;
		}

		private IEnumerable<SmsEntry> GetSmsMessagesFromDevice()
		{
			var inboxUri = Android.Net.Uri.Parse("content://sms");
			var columns = new[] { "_id", "thread_id", "address", "date", "body", "type" };

			var cursor = ContentResolver.Query(inboxUri, columns, null, null, null, null);
			while (cursor.MoveToNext())
			{
				var sms = new SmsEntry
				{
					Id = cursor.GetString(cursor.GetColumnIndex(columns[0])),
					ThreadId = cursor.GetString(cursor.GetColumnIndex(columns[1])),
					Address = cursor.GetString(cursor.GetColumnIndex(columns[2])),
					Date = cursor.GetString(cursor.GetColumnIndex(columns[3])),
					Message = cursor.GetString(cursor.GetColumnIndex(columns[4])),
					Type = (SmsDirection)int.Parse(cursor.GetString(cursor.GetColumnIndex(columns[5])))
				};
				yield return sms;
			}
		}
	}
}

