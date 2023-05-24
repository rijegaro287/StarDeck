using Stardeck.DbAccess;
using Stardeck.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Stardeck.Logic
{
    public class CardLogic
    {
        private readonly StardeckContext context;
        private readonly CardDb cardDB;
        public CardLogic(StardeckContext context)
        {
            this.context = context;
            this.cardDB=new CardDb(context);
        }

        public Object GetAll()
        {
            List<Card> cards = (List<Card>)cardDB.GetAllCards();
            
            if (cards.Equals(0) )
            {
                return 0;
            }
            return (List<Card>)cards;
        }

        public Card GetCard(string id)
        {
            var card = cardDB.GetCard(id);
            if (card == null)
            {
                return null;
            }
            return card;
        }


        public Object NewCard(CardImage card)
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
                Race = card.Race

            };
            var save = cardDB.NewCard(cardAux);
            if (save == null)
            {
                return null;
            }
            if(save.Equals(0))
            {
                return 0;
            }
            
            return cardAux;

        }

        public Card UpdateCard(string id, Card nCard)
        {
            var card = cardDB.GetCard(id);
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


        public Card DeleteCard(string id)
        {
            var card = cardDB.DeleteCard(id);
            if (card != null)
            {
                return card;
            }
            return null;
        }

        public List<Card> GetNineCards()
        {
            List<Card> cards =(List<Card>) GetAll();
            if(cards.Count == 0 || cards==null)
            {
                return null;
            }
            List<Card> filteredCards = cards.FindAll(x => x.Type == 1 || x.Type == 2);
            Random rand = new Random();
            var shuffled = filteredCards.OrderBy(_ => rand.Next()).ToList();
            return shuffled.GetRange(0,9);
        }
     }

    }

