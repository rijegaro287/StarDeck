using Stardeck.Models;

namespace Stardeck.DbAccess
{
    public class CardDb
    {
        private readonly StardeckContext context;

        public CardDb(StardeckContext context)
        {
            this.context = context;
        }

        public List<Card> GetAllCards()
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
            var card = context.Cards.Find(id);

            if (card == null)
            {
                return null;
            }
            return card;
        }


        public Card NewCard(Card card)
        {
            context.Cards.Add(card);
            context.SaveChanges();
            return card;
        }

        public Card DeleteCard(string id)
        {
            var card = GetCard(id);
            if (card != null)
            {
                context.Remove(card);
                context.SaveChanges();
                return card;
            }
            return null;
        }

    }
}
