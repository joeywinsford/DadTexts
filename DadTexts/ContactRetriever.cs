using System.Collections.Generic;
using Android.App;
using Android.Net;
using Android.Provider;

namespace DadTexts
{
	public class ContactRetriever
	{
		private readonly Activity _context;

		private readonly Dictionary<string, string> _knownContacts = new Dictionary<string, string>();

		public ContactRetriever(Activity context)
		{
			_context = context;
		}

		public string GetContactName(SmsEntry smsEntry)
		{
			var phoneNumber = smsEntry.Address;
			if (_knownContacts.ContainsKey(phoneNumber))
			{
				return _knownContacts[phoneNumber];
			}

			var contactUri = Uri.WithAppendedPath(ContactsContract.PhoneLookup.ContentFilterUri, Uri.Encode(phoneNumber));
			using (var cursor = _context.ContentResolver.Query(contactUri, new[] {ContactsContract.PhoneLookup.InterfaceConsts.DisplayName}, null, null, null))
			{
				if (!cursor.MoveToFirst())
				{
					return null;
				}

				var contactName = cursor.GetString(cursor.GetColumnIndex(ContactsContract.PhoneLookup.InterfaceConsts.DisplayName));
				_knownContacts.Add(phoneNumber, contactName);
				return contactName;
			}
		}
	}
}