using Stardeck.Controllers;
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
        private readonly ILogger _logger;
        public CardLogic(StardeckContext context, ILogger logger)
        {
            _logger = logger;
            this.context = context;
            this.cardDB=new CardDb(context);
        }

        public List<Card> GetAll()
        {
            List<Card>? cards = cardDB.GetAllCards();
            _logger.LogInformation("Request GetAll de cards completada");
            return cards;
        }

        public Card? GetCard(string id)
        {
            var card = cardDB.GetCard(id);
            if (card == null)
            {
                _logger.LogWarning("no se encontró carta {id}",id);
                return null;
            }
            _logger.LogInformation("Request GetCards para {id} completada",id);
            return card;
        }


        public bool? NewCard(CardImage card)
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
            _logger.LogInformation("Request NewCard para {id} completada", card.Id);
            return save;

        }

        public Card? UpdateCard(string id, Card nCard)
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
                _logger.LogInformation("Request UpdateCard para {id} completada", id);
                return card;
            }
            _logger.LogWarning("No se pudo actualizar la carta {id}", id);
            return null;

        }


        public Card? DeleteCard(string id)
        {
            var card = cardDB.DeleteCard(id);
            if (card is null)
            {
                _logger.LogInformation("Request DeleteCard para {id} completada", id);
                return card;
            }
            _logger.LogWarning("No se pudo eliminar la carta {id}", id);
            return null;
        }

        public List<Card> GetNineCards()
        {
            List<Card> cards =(List<Card>) GetAll();
            if(cards.Count == 0 || cards==null)
            {
                _logger.LogWarning("No existen las suficientes cartas en GetNineCards");
                return null;
            }
            List<Card> filteredCards = cards.FindAll(x => x.Type == 1 || x.Type == 2);
            Random rand = new Random();
            var shuffled = filteredCards.OrderBy(_ => rand.Next()).ToList();
            _logger.LogInformation("Request GetNineCards completada");
            return shuffled.GetRange(0,9);
        }
     }

    }

