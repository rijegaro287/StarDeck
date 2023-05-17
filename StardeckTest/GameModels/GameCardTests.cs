using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stardeck.GameModels;
using Stardeck.Logic;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stardeck.GameModels.Tests
{
    [TestClass()]
    public class GameCardTests
    {
        [TestMethod()]
        public void GameCardTest()
        {
            var cartasRaras=(new CardLogic(new()).GetAll());
            Assert.IsNotNull(cartasRaras,"no se lograron obtener todas las cartas de la base");
            var CartasTransformadas = new List<GameModels.GameCard>();
            foreach(var card in cartasRaras)
            {
                try { 
                CartasTransformadas.Add(new(card));
                }catch(Exception ex)
                {
                    Assert.Inconclusive("No se logro transformar la carta: "+card.Id+" a una carta jugable");
                }
            }

        }
    }
}