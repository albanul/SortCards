using System;
using System.Collections.Generic;

namespace Cards
{
	public class Program
	{
		public static List<Card> SortCards(List<Card> shuffledCards)
		{
			var unshuffledCards = new List<Card>();

			if (shuffledCards != null && shuffledCards.Count > 0)
			{
				var firstCard = shuffledCards[0];

				var departureDict = new Dictionary<string, Node>();
				var arrivalDict = new Dictionary<string, Node>();

				foreach (var card in shuffledCards)
				{
					var arrivalNode = new Node {Previous = card, Leftmost = card, Rightmost = card};
					var departureNode = new Node {Next = card, Leftmost = card, Rightmost = card};

					Node arrivalNodeLeft = null;
					Node departureNodeRight = null;

					departureDict.Add(card.Departure, departureNode);
					arrivalDict.Add(card.Arrival, arrivalNode);

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

					if (arrivalNodeLeft != null)
					{
						var leftmostCard = arrivalNodeLeft.Leftmost;

						var leftmostArrivalNode = arrivalDict[leftmostCard.Arrival];
						var leftmostDepartureNode = departureDict[leftmostCard.Departure];

						leftmostArrivalNode.Rightmost = card;
						leftmostDepartureNode.Rightmost = card;

						arrivalNode.Leftmost = leftmostArrivalNode.Previous;
						departureNode.Leftmost = leftmostDepartureNode.Next;
					}

					if (departureNodeRight != null)
					{
						var rightmostCard = departureNodeRight.Rightmost;
						var leftmostCard = departureNode.Leftmost;

						var rightmostArrivalNode = arrivalDict[rightmostCard.Arrival];
						var rightmostDepartureNode = departureDict[rightmostCard.Departure];

						var leftmostArrivalNode = arrivalDict[leftmostCard.Arrival];
						var leftmostDepartureNode = departureDict[leftmostCard.Departure];

						if (rightmostCard == firstCard)
						{
							firstCard = leftmostCard;
						}

						rightmostArrivalNode.Leftmost = leftmostCard;
						rightmostDepartureNode.Leftmost = leftmostCard;

						leftmostArrivalNode.Rightmost = rightmostCard;
						leftmostDepartureNode.Rightmost = leftmostCard;
					}
				}

				unshuffledCards.Add(firstCard);

				var node = arrivalDict[firstCard.Arrival];

				while (node.Next != null)
				{
					unshuffledCards.Add(node.Next);
					node = arrivalDict[node.Next.Arrival];
					if (node.Next == firstCard)
					{
						throw new LoopException("We are in loop. The departure point and the arrival point are the same!");
					}
						
				}
			}

			return unshuffledCards;
		}

		private static void Main()
		{
			
		}
	}
}
