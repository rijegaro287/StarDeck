using Stardeck.DbAccess;
using Stardeck.Models;
using System.Text.RegularExpressions;

namespace Stardeck.Logic
{
    public class DeckLogic
    {

        private readonly StardeckContext context;
        private readonly DeckDb deckDB;
        public DeckLogic(StardeckContext context)
        {
            this.context = context;
            this.deckDB = new DeckDb(context);
        }


        public List<Deck>? GetAll()
        {
            List<Deck> decks = deckDB.GetAllDecks();
            if (decks == null)
            {
                return null;
            }
            return decks;
        }

        public Object GetNames(string userId)
        {
            var decks = deckDB.GetNames(userId);
            if (decks .Equals(0))
            {
                return 0;
            }
            return decks;
        }



        public Deck? GetDeck(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var deck = deckDB.GetDeck(id);

            return deck;

        }

        public Deck? NewDeck(Deck deck)
        {
            var deckAux = new Deck(deck.Cardlist)
            {
                DeckName = deck.DeckName,
                IdDeck = deck.IdDeck,
                IdAccount = deck.IdAccount,
                Cardlist = deck.Cardlist,

            };

            deckAux.generateID();
            deckDB.NewDeck(deckAux);
            if(deckDB.GetDeck(deckAux.Id) == null)
            {
                return null;
            }
            return deckAux;

        }

        public Deck UpdateDeck(string id, Deck nDeck)
        {
            var deck = context.Decks.Find(id);
            if (deck != null)
            {
                deck.IdDeck = nDeck.IdDeck; //MAKE DECK ID
                deck.IdAccount = nDeck.IdAccount;
                deck.Cardlist = nDeck.Cardlist;

                context.SaveChanges();
                return deck;
            }
            return null;

        }

        public Deck DeleteDeck(string id)
        {
            var deck = deckDB.DeleteDeck(id);
            if (deck != null)
            {
                return deck;
            }
            return null;
        }

        public List<Deck>? GetDecksByUser(string Userid)
        {
            List<Deck> decks = deckDB.GetDecksByUser(Userid);
            if (decks == null)
            {
                return null;
            }
            return decks;
        }
        
        public static List<T> Shuffle<T>(IEnumerable<T> list)
        {
            var random = new Random();
            var newList = list.OrderBy(item => random.Next()).ToList();
            return newList;
        }
    }
}
