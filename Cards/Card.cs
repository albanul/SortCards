using System;

namespace Cards
{
	public class Card : IEquatable<Card>
	{
		public string Departure { get; }

		public string Arrival { get; }

		public Card(string departure, string arrival)
		{
			Departure = departure;
			Arrival = arrival;
		}

		public bool Equals(Card other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Departure, other.Departure) && string.Equals(Arrival, other.Arrival);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return obj.GetType() == GetType() && Equals((Card) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Departure?.GetHashCode() ?? 0) * 397) ^ (Arrival?.GetHashCode() ?? 0);
			}
		}
	}
}