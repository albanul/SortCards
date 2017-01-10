using System.Collections.Generic;

namespace Cards
{
	public class Program
	{
		public static List<Card> SortCards(List<Card> shuffledCards)
		{
			var unshuffledCards = new List<Card>();

			// if list of shuffled cards is null or empty then return empty array
			if (shuffledCards == null || shuffledCards.Count == 0)
			{
				return unshuffledCards;
			}

			var firstCard = shuffledCards[0];

			var departureDict = new Dictionary<string, Node>();
			var arrivalDict = new Dictionary<string, Node>();

			foreach (var card in shuffledCards)
			{
				// Leftmost and Rightmost properties are links to leftmost and rightmost cards
				// in its own connected component
				var arrivalNode = new Node {Previous = card, Leftmost = card, Rightmost = card};
				var departureNode = new Node {Next = card, Leftmost = card, Rightmost = card};

				Node arrivalNodeLeft = null;
				Node departureNodeRight = null;

				// trying to find left card to join
				if (arrivalDict.ContainsKey(card.Departure))
				{
					arrivalNodeLeft = arrivalDict[card.Departure];
					departureNode.Previous = arrivalNodeLeft.Previous;
					if (arrivalNodeLeft.Next == null)
					{
						arrivalNodeLeft.Next = card;
					}
					else
					{
						throw new LoopException("There is a loop in the card list!");
					}
				}

				// trying to find right card to join
				if (departureDict.ContainsKey(card.Arrival))
				{
					departureNodeRight = departureDict[card.Arrival];
					arrivalNode.Next = departureNodeRight.Next;
					if (departureNodeRight.Previous == null)
					{
						arrivalNode.Previous = card;
					}
					else
					{
						throw new LoopException("There is a loop in the card list!");
					}
				}

				// add nodes to the hash tables
				departureDict.Add(card.Departure, departureNode);
				arrivalDict.Add(card.Arrival, arrivalNode);


				// section where we handle the first card in result list

				// if we have successfully joined to the left card
				if (arrivalNodeLeft != null)
				{
					var leftmostCard = arrivalNodeLeft.Leftmost;

					var leftmostArrivalNode = arrivalDict[leftmostCard.Arrival];
					var leftmostDepartureNode = departureDict[leftmostCard.Departure];

					// set correct pointers to the leftmost and the rightmost cards
					// in the current connected component
					leftmostArrivalNode.Rightmost = card;
					leftmostDepartureNode.Rightmost = card;

					arrivalNode.Leftmost = leftmostArrivalNode.Previous;
					departureNode.Leftmost = leftmostDepartureNode.Next;
				}

				// if we have successfully joined to right card
				if (departureNodeRight != null)
				{
					var rightmostCard = departureNodeRight.Rightmost;
					var leftmostCard = departureNode.Leftmost;

					var rightmostArrivalNode = arrivalDict[rightmostCard.Arrival];
					var rightmostDepartureNode = departureDict[rightmostCard.Departure];

					var leftmostArrivalNode = arrivalDict[leftmostCard.Arrival];
					var leftmostDepartureNode = departureDict[leftmostCard.Departure];

					// change first card pointer if necessary
					if (rightmostCard == firstCard)
					{
						firstCard = leftmostCard;
					}

					// set correct pointers to the leftmost and the rightmost cards
					// in the current connected component
					rightmostArrivalNode.Leftmost = leftmostCard;
					rightmostDepartureNode.Leftmost = leftmostCard;

					leftmostArrivalNode.Rightmost = rightmostCard;
					leftmostDepartureNode.Rightmost = leftmostCard;
				}
			}

			// generate unshuffled list
			unshuffledCards.Add(firstCard);
			var node = arrivalDict[firstCard.Arrival];

			while (node.Next != null)
			{
				unshuffledCards.Add(node.Next);
				node = arrivalDict[node.Next.Arrival];

				// throw an exception if we have stucked in a loop
				if (node.Next == firstCard)
				{
					throw new LoopException("We are in a loop. The departure point and the arrival point are the same!");
				}

			}

			return unshuffledCards;
		}

		private static void Main()
		{
			
		}
	}
}
