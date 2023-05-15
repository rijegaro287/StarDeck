using Stardeck.Models;
using System.Text.RegularExpressions;

namespace Stardeck.Logic
{
    public class DeckLogic
    {

        private readonly StardeckContext context;

        public DeckLogic(StardeckContext context)
        {
            this.context = context;
        }


        public List<Deck>? GetAll()
        {
            List<Deck> decks = context.Decks.ToList();
            if (decks.Count == 0)
            {
                return null;
            }
            return decks;
        }

        public Dictionary<string,string> GetNames(string userId)
        {
            var decks = GetDecksByUser(userId);
            Dictionary<string, string> deckNames = new Dictionary<string, string>();
            foreach (var deck in decks)
            {
                deckNames.Add(deck.IdDeck, deck.DeckName);
            }
            if (decks.Count == 0)
            {
                return null;
            }
            return deckNames;
        }



        public Deck GetDeck(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var deck = context.Decks.Find(id);

            if (deck == null)
            {
                return null;
            }
            return deck;

        }

        public Deck NewDeck(Deck deck)
        {
            var deckAux = new Deck(deck.Cardlist)
            {
                IdDeck = deck.IdDeck,
                IdAccount = deck.IdAccount,
                Cardlist = deck.Cardlist,
                DeckName=deck.DeckName

            };

            deckAux.generateID();


            context.Decks.Add(deckAux);
            context.SaveChanges();
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
                deck.DeckName=nDeck.DeckName;

                context.SaveChanges();
                return deck;
            }
            return null;

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

        public List<Deck>? GetDecksByUser(string Userid)
        {
            List<Deck> decks = context.Decks.Where(x=>x.IdAccount==Userid).ToList();
            if (decks.Count == 0)
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
