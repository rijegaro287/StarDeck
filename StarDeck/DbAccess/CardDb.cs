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

        public List<Card>? GetAllCards()
        {
            List<Card> cards = context.Cards.ToList();
            if (cards.Count == 0)
            {
                return null;
            }
            return cards;
        }

        public Card? GetCard(string id)
        {
            var card = context.Cards.Find(id);

            return card;
        }

        public List<Card>? GetCardByType(int type)
        {
            var card = context.Cards.Where(x => x.Type == 0).ToList();

            if (card.Count == 0)
            {
                return null;
            }
            return card;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns>True if card was saved, false if card catch a error in saving, null if card already exist</returns>
        public bool? NewCard(Card card)
        {
            if (GetCard(card.Id) != null)
            {
                return null;
            }
            context.Cards.Add(card);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
#warning add log for this exception
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public Card? DeleteCard(string id)
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
