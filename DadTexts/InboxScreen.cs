using System.Linq;
using Android.App;
using Android.OS;

namespace DadTexts
{
	[Activity(Label = "Dad Texts", MainLauncher = true, Icon = "@drawable/icon")]
	public class InboxScreen : ListActivity
	{
		private readonly SmsRetriever _smsRetriever;

		public InboxScreen()
		{
			_smsRetriever = new SmsRetriever(this);
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			ListAdapter = new InboxScreenAdapter(this, _smsRetriever.GetSmsMessagesFromDevice().ToList());
			ListView.FastScrollEnabled = true;
		}
	}
}