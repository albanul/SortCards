using System.Collections.Generic;
using Xunit;

namespace Cards.Tests
{
	public class ProgramTests
	{
		[Fact]
		public void SortCards_NullList_EmptyListReturned()
		{
			var result = new List<Card>();

			var cards = Program.SortCards(null);

			Assert.Equal(result, cards);
		}

		[Fact]
		public void SortCards_EmptyList_EmptyListReturned()
		{
			var input = new List<Card>();
			var result = new List<Card>();

			var cards = Program.SortCards(input);

			Assert.Equal(result, cards);
		}

		[Fact]
		public void SortCards_ListWithOneCard_SameListReturned()
		{
			var input = new List<Card> {new Card("Melbourne", "Cologne")};

			var cards = Program.SortCards(input);

			Assert.Equal(input, cards);
		}

		[Fact]
		public void SortCards_ListWihTwoUnshuffeldCards_SameListReturned()
		{
			var input = new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Cologne", "Moscow")
			};

			var cards = Program.SortCards(input);

			Assert.Equal(input, cards);
		}

		[Fact]
		public void SortCards_ListWithCMAndMC_ListWithMCAndCMReturned()
		{
			var input = new List<Card>
			{
				new Card("Cologne", "Moscow"),
				new Card("Melbourne", "Cologne")
			};
			var result = new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Cologne", "Moscow")
			};

			var cards = Program.SortCards(input);

			Assert.Equal(result, cards);
		}

		[Fact]
		public void SortCards_ListWithThreeShuffeldCards_UnshuffeldListReturned()
		{
			var input = new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Moscow", "Paris"),
				new Card("Cologne", "Moscow")
			};
			var result = new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Cologne", "Moscow"),
				new Card("Moscow", "Paris")
			};

			var cards = Program.SortCards(input);

			Assert.Equal(result, cards);
		}

		[Fact]
		public void SortCards_WorstCaseScenario()
		{
			const int count = 100000;
			var input = new List<Card>();
			var subList = new List<Card>();
			var result = new List<Card>();

			for (var i = 0; i < count; i+=2)
			{
				var first = i.ToString();
				var second = (i+1).ToString();
				var third = (i+2).ToString();

				var card1 = new Card(first, second);
				var card2 = new Card(second, third);

				input.Add(card1);
				subList.Insert(0, card2);

				result.Add(card1);
				result.Add(card2);
			}

			input.AddRange(subList);

			var cards = Program.SortCards(input);

			Assert.Equal(result, cards);
		}

		[Fact]
		public void SortCards_ListWithLoopCards_ThrowsLoopException()
		{
			var input = new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Moscow", "Paris"),
				new Card("Cologne", "Moscow"),
				new Card("Paris", "Melbourne")
			};

			Assert.Throws<LoopException>(() => Program.SortCards(input));
		}

		
		[Fact]
		public void SortCards_ListWithLoopNumericCards_ThrowsLoopException()
		{
			var input = new List<Card>
			{
				new Card("1", "2"),
				new Card("2", "3"),
				new Card("4", "5"),
				new Card("5", "6"),
				new Card("6", "2"),
				new Card("6", "7"),
				new Card("7", "8")
			};

			Assert.Throws<LoopException>(() => Program.SortCards(input));
		}
	}
}