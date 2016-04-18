using System.Collections;

namespace Cards
{
	public class CardComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			var xCard = x as Card;
			var yCard = y as Card;

			if (xCard != null && yCard != null)
			{
				return xCard.Departure == yCard.Departure && xCard.Arrival == yCard.Arrival ? 0 : 1;
			}

			return x.Equals(y) ? 0 : 1;
		}
	}
}