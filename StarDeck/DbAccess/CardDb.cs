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

        public Object GetAllCards()
        {
            List<Card> cards = context.Cards.ToList();
            if (cards.Count == 0)
            {
                return 0;
            }
            if (cards== null) 
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

        public Object GetCardByType(int type)
        {
            var card = context.Cards.Where(x => x.Type == 0).ToList();

            if (card == null)
            {
                return null;
            }
            if(card.Count == 0) 
            {
                return 0;
            }
            return card;
        }


        public Object NewCard(Card card)
        {
            if(GetCard(card.Id)!=null) 
            { 
                return 0; 
            }
            context.Cards.Add(card);
            context.SaveChanges();
            if (GetCard(card.Id) == null)
            {
                return null;
            }
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
