using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Cards.Tests
{
	[TestClass]
	public class ProgramTests
	{
		[TestMethod]
		public void NullInputCardsTest()
		{
			var result = Program.SortCards(null);

			CollectionAssert.AreEqual(new List<Card>(), result, "null input test fails");
		}

		[TestMethod]
		public void EmptyInputCardsTests()
		{
			var result = Program.SortCards(new List<Card>());

			CollectionAssert.AreEqual(new List<Card>(), result, "empty input test fails");
		}

		[TestMethod]
		public void SingleCardTest()
		{
			var input = new List<Card> {new Card("Melbourne", "Cologne")};

			var result = Program.SortCards(input);

			CollectionAssert.AreEqual(input, result, "single card input test fails");
		}

		[TestMethod]
		public void TwoUnshuffledCardsTest()
		{
			var input = new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Cologne", "Moscow")
			};

			var result = Program.SortCards(input);

			CollectionAssert.AreEqual(input, result, "two unshuffled cards test fails");
		}

		[TestMethod]
		public void TwoShuffledCardsTest()
		{
			var result = Program.SortCards(new List<Card>
			{
				new Card("Cologne", "Moscow"),
				new Card("Melbourne", "Cologne")
			});

			var correctResult = new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Cologne", "Moscow")
			};

			CollectionAssert.AreEqual(correctResult, result, Card.CardsComparer, "two shuffled cards test fails");
		}

		[TestMethod]
		public void SimpleTest()
		{
			var result = Program.SortCards(new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Moscow", "Paris"),
				new Card("Cologne", "Moscow")
			});

			var correctResult = new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Cologne", "Moscow"),
				new Card("Moscow", "Paris")
			};

			CollectionAssert.AreEqual(correctResult, result, Card.CardsComparer, "simple test fails");
		}

		[TestMethod]
		public void WorstCaseScenarioTest()
		{
			var input = new List<Card>();
			var subList = new List<Card>();
			var correctResult = new List<Card>();

			var i = 0;
			while (i < 1000000)
			{
				var card1 = new Card(i.ToString(), (i+1).ToString());
				var card2 = new Card((i+1).ToString(), (i+2).ToString());

				input.Add(card1);
				subList.Add(card2);

				correctResult.Add(card1);
				correctResult.Add(card2);

				i += 2;
			}

			subList.Reverse();

			input.AddRange(subList);

			var result = Program.SortCards(input);

			CollectionAssert.AreEqual(result, correctResult, "Worst case scenario test fails");
		}

		[TestMethod]
		[ExpectedException(typeof(LoopException))]
		public void FullLoopTest()
		{
			Program.SortCards(new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Moscow", "Paris"),
				new Card("Cologne", "Moscow"),
				new Card("Paris", "Melbourne")
			});
		}

		[TestMethod]
		[ExpectedException(typeof(LoopException))]
		public void LocalLoopTest()
		{
			Program.SortCards(new List<Card>
			{
				new Card("1", "2"),
				new Card("2", "3"),
				new Card("4", "5"),
				new Card("5", "6"),
				new Card("6", "2"),
				new Card("6", "7"),
				new Card("7", "8")
			});
		}
	}
}