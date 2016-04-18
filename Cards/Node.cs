namespace Cards
{
	public class Node
	{
		public Card Previous { get; set; }

		public Card Next { get; set; }

		public Card Leftmost { get; set; }

		public Card Rightmost { get; set; }

		public Node(Card previous = null, Card next = null, Card leftmost = null, Card rightmost = null)
		{
			Previous = previous;
			Next = next;
			Leftmost = leftmost;
			Rightmost = rightmost;
		}
	}
}