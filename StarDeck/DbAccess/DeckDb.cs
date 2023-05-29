using Stardeck.Models;

namespace Stardeck.DbAccess
{
    public class DeckDb
    {

        private readonly StardeckContext context;

        public DeckDb(StardeckContext context)
        {
            this.context = context;
        }

        public List<Deck>? GetAllDecks()
        {
            List<Deck> decks = context.Decks.ToList();
            if (decks.Count == 0)
            {
                return null;
            }
            return decks;
        }


        public Object GetNames(string userId)
        {
            var userDecks = GetDecksByUser(userId);
            if(userDecks==null) 
            {
                return 0;
            }


            var decks = userDecks.Select(x => new { Id = x.IdDeck, Name = x.DeckName }).ToList();
            
            if (decks==null)
            {
                return null;
            }
            return decks;
        }
        public List<Deck>? GetDecksByUser(string Userid)
        {
            List<Deck> decks = context.Decks.Where(x => x.IdAccount == Userid).ToList();
            if (decks.Count == 0)
            {
                return null;
            }
            return decks;
        }

        public Deck? GetDeck(string id)
        {
            var deck = context.Decks.Find(id);

            return deck;

        }

        public Deck NewDeck(Deck deck)
        {
            context.Decks.Add(deck);
            context.SaveChanges();
            return deck;
        }

        public Deck DeleteDeck(string id)
        {
            var deck = context.Decks.Find(id);
            if (deck != null)
            {
                context.Remove(deck);
                context.SaveChanges();
                return deck;
            }
            return null;
        }




    }
}
