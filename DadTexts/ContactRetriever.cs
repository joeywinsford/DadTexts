using Android.App;
using Android.Net;
using Android.Provider;

namespace DadTexts
{
	public class ContactRetriever
	{
		private readonly Activity _context;

		public ContactRetriever(Activity context)
		{
			_context = context;
		}

		public string GetContactName(SmsEntry smsEntry)
		{
			var contactUri = Uri.WithAppendedPath(ContactsContract.PhoneLookup.ContentFilterUri, Uri.Encode(smsEntry.Address));
			using (
				var cursor = _context.ContentResolver.Query(contactUri,
					new[] {ContactsContract.PhoneLookup.InterfaceConsts.DisplayName}, null, null, null))
			{
				if (cursor.MoveToFirst())
				{
					return cursor.GetString(cursor.GetColumnIndex(ContactsContract.PhoneLookup.InterfaceConsts.DisplayName));
				}
			}

			return null;
		}
	}
}