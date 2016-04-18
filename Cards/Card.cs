using System.Collections;

namespace Cards
{
	public class Card
	{
		public static IComparer CardsComparer => new CardComparer() as IComparer;

		public string Departure { get; }

		public string Arrival { get; }

		public Card(string departure = "", string arrival = "")
		{
			Departure = departure;
			Arrival = arrival;
		}
	}
}