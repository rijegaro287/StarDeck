using System.Text.RegularExpressions;

namespace Stardeck.Models.Tests
{
    [TestClass()]
    public class DeckTests
    {
        private Deck deck;

        [TestInitialize]
        public void poblateData()
        {
            deck = new(new List<string>().ToArray());
            deck.addCard("1");
            deck.Id = "D-123456789012";
            deck.IdAccount = "U-123456789012";
        }

        [TestMethod()]
        public void generateIDTest()
        {
            var deckAux = new Deck(deck.Cardlist)
            {
                IdDeck = "asdasdasdasdasd",
                IdAccount = deck.IdAccount,
                Cardlist = deck.Cardlist,

            };

            deckAux.generateID();

            Assert.IsTrue(Regex.IsMatch(deckAux.Id, @"^D-[a-zA-Z0-9]{12}"));
        }


        [TestMethod()]
        public void CheckIDTest()
        {
            var deckAux = new Deck(deck.Cardlist)
            {
                IdDeck = deck.IdDeck,
                IdAccount = deck.IdAccount,
                Cardlist = deck.Cardlist,

            };

            deckAux.generateID();

            Assert.IsTrue(Regex.IsMatch(deckAux.Id, @"^D-[a-zA-Z0-9]{12}"));
            Assert.IsTrue((deckAux.Id == deck.Id));
        }


        [TestMethod()]
        public void addCardTest()
        {
            int initial = deck.Cardlist.Length;
            deck.addCard("2");
            Assert.IsTrue(deck.Cardlist.Length == initial + 1);

        }

        [TestMethod()]
        public void DekcLimitTest()
        {

            deck.addCard("2");
            deck.addCard("3");
            deck.addCard("4");
            deck.addCard("5");
            deck.addCard("6");
            deck.addCard("7");
            deck.addCard("8");
            deck.addCard("9");
            deck.addCard("10");
            deck.addCard("11");
            deck.addCard("12");
            deck.addCard("13");
            deck.addCard("14");
            deck.addCard("15");
            deck.addCard("16");
            deck.addCard("17");
            deck.addCard("18");
            Assert.IsTrue(deck.Cardlist.Length == 18);


            var card = deck.addCard("19");

            Assert.IsTrue(deck.Cardlist.Length == 18);
            Assert.IsTrue(card is null);

        }

    }
}