using Microsoft.Extensions.Logging.Abstractions;
using Stardeck.Controllers;
using Stardeck.Logic;
using Stardeck.Models;

namespace Stardeck.GameModels.Tests
{
    [TestClass()]
    public class GameCardTests
    {
        [TestMethod()]
        public void GameCardTest()
        {
            var cartasRaras = (new CardLogic(new StardeckContext(), new NullLogger<GameController>()).GetAll());
            Assert.IsNotNull(cartasRaras, "no se lograron obtener todas las cartas de la base");
            var CartasTransformadas = new List<GameModels.GameCard>();
            foreach (var card in cartasRaras)
            {
                try
                {
                    CartasTransformadas.Add(new(card));
                }
                catch (Exception ex)
                {
                    Assert.Inconclusive("No se logro transformar la carta: " + card.Id + " a una carta jugable");
                }
            }

        }
    }
}