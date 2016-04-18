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

			CollectionAssert.AreEqual(input, result, "two cards unshuffled test fails");
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

			CollectionAssert.AreEqual(correctResult, result, Card.CardsComparer);
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

			CollectionAssert.AreEqual(correctResult, result, Card.CardsComparer);
		}

		[TestMethod]
		[ExpectedException(typeof(LoopException))]
		public void LoopTest()
		{
			Program.SortCards(new List<Card>
			{
				new Card("Melbourne", "Cologne"),
				new Card("Moscow", "Paris"),
				new Card("Cologne", "Moscow"),
				new Card("Paris", "Melbourne")
			});
		}
	}
}