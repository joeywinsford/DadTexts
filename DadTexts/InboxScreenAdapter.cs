using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;

namespace DadTexts
{
	public class InboxScreenAdapter : BaseAdapter<SmsEntry>
	{
		private readonly ContactRetriever _contactRetriever;
		private readonly Activity _context;
		private readonly List<SmsEntry> _items;

		public InboxScreenAdapter(Activity context, List<SmsEntry> items)
		{
			_context = context;
			_items = items;
			_contactRetriever = new ContactRetriever(context);
		}

		public override int Count => _items.Count();
		public override SmsEntry this[int position] => _items[position];

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			var smsEntry = _items[position];

			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = _contactRetriever.GetContactName(smsEntry) ?? smsEntry.Address;
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = smsEntry.Message;
			return view;
		}

		public override long GetItemId(int position) => position;
	}
}