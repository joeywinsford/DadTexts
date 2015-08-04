namespace DadTexts
{
	public class SmsEntry
	{
		public string Id { get; set; }

		public string ThreadId { get; set; }

		public string Address { get; set; }

		public string Date { get; set; }

		public string Message { get; set; }

		public SmsDirection Type { get; set; }
	}

	public enum SmsDirection
	{
		Incoming = 1,
		Outgoing = 2
	}
}