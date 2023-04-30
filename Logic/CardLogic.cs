using Microsoft.AspNetCore.Mvc;
using Stardeck.Models;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Stardeck.Logic
{
    public class CardLogic
    {
        private readonly StardeckContext context;

        public CardLogic(StardeckContext context)
        {
            this.context = context;
        }

        public List<Card> GetAll()
        {
            List<Card> cards = context.Cards.ToList();
            if (cards.Count == 0)
            {
                return null;
            }
            return cards;
        }

        public Card GetCard(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var card = context.Cards.Find(id);

            if (card == null)
            {
                return null;
            }
            return card;
        }


        public Card NewCard(CardImage card)
        {
            var cardAux = new Card()
            {
                Id = card.Id,
                Name = card.Name,
                Energy = card.Energy,
                Battlecost = card.Battlecost,
                Image = Convert.FromBase64String(card.Image.ToString()),
                Active = card.Active,
                Type = card.Type,
                Ability = card.Ability,
                Description = card.Description,
                Race=card.Race

            };

            while (!Regex.IsMatch(cardAux.Id, @"^C-[a-zA-Z0-9]{12}"))
            {
                cardAux.Id = string.Concat("C-", System.Guid.NewGuid().ToString().Replace("-", "").AsSpan(0, 12));
            }
            context.Cards.Add(cardAux);

            context.SaveChanges();
            return card;

        }

        public Card UpdateCard(string id, Card nCard)
        {
            var card = context.Cards.Find(id);
            if (card != null)
            {
                card.Name = nCard.Name;
                card.Energy = nCard.Energy;
                card.Battlecost = nCard.Battlecost;
                card.Image = nCard.Image;
                card.Active = nCard.Active;
                card.Type = nCard.Type;
                card.Ability = nCard.Ability;
                card.Description = nCard.Description;
                card.Race = nCard.Race;

                context.SaveChanges();
                return card;
            }
            return null;

        }


        public Card deleteCard(string id)
        {
            var card = context.Cards.Find(id);
            if (card != null)
            {
                //REMOVER LOS DATOS ASOCIADOS EN OTRAS TABLAS
                context.Remove(card);
                context.SaveChanges();
                return card;
            }
            return null;
        }

        public List<List<Card>> getNineCards()
        {
            List<Card> filteredCards = GetAll().FindAll(x => x.Type == 1 ||x.Type==2);
            Random rand = new Random();
            var shuffled = filteredCards.OrderBy(_ => rand.Next()).ToList();

            List<List<Card>> cards = new List<List<Card>>
            {
                shuffled.GetRange(0, 3),
                shuffled.GetRange(3, 3),
                shuffled.GetRange(6, 3)
            };

            return cards;

        }

    }
}
