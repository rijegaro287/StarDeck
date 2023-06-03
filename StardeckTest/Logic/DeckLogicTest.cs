using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StardeckTest.Logic
{
    [TestClass]
    public class DeckLogicTest
    {
        Deck deck;


        [TestInitialize]
        public void poblateData()
        {
            deck = new Deck(new string[]{ "C-9ae900e6feec" } )
            {
                Id = "",
                IdAccount="U-RXF7RJNBWEKD",
                Cardlist = new string[] { "C-9ae900e6feec" }

            };
        }
        [TestMethod]
        public void DeckCreationTest()
        {
            var logic = new Stardeck.Logic.DeckLogic(new FakeStardeckContext());
            var response = logic.NewDeck(deck);
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(),typeof(Deck) );
        }


        [TestMethod]
        public void DeckCheckIdTest()
        {
            deck.generateID();

            //Assert
            Assert.IsTrue(Regex.IsMatch(deck.Id, @"^D-[a-zA-Z0-9]{12}"));
        }

        [TestMethod]
        public void DeckFoundTest()
        {
            var logic = new Stardeck.Logic.DeckLogic(new FakeStardeckContext());

            var response = logic.GetDeck("D-100bfff317c4");

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(Deck));

        }

        [TestMethod]
        public void AllDecksFoundTest()
        {
            var logic = new Stardeck.Logic.DeckLogic(new FakeStardeckContext());
            var response = logic.GetAll();
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(List<Deck>));
        }

        [TestMethod]
        public void GetNamesTest()
        {
            var logic = new Stardeck.Logic.DeckLogic(new FakeStardeckContext());
            var response = logic.GetAll();
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(List<Deck>));
        }

        [TestMethod]
        public void GetDecksByUserTest()
        {
            var logic = new Stardeck.Logic.DeckLogic(new FakeStardeckContext());
            var response = logic.GetDecksByUser("U-XA8G5I3ZEZJV");
            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.GetType(), typeof(List<Deck>));
        }
    }
}
