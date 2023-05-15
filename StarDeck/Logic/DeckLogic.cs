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

        public List<KvPairDeckName>? GetNames(string userId)
        {
            var decks = GetDecksByUser(userId).Select(x => new KvPairDeckName(){Id=x.IdDeck,Name=x.DeckName}).ToList();
            return decks.Count == 0 ? null : decks.ToList();
        }
        public struct KvPairDeckName
        {
            public string Id { get; set; }
            public string Name{ get; set; }
        }



        public Deck? GetDeck(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var deck = context.Decks.Find(id);

            return deck ?? null;
        }

        public Deck NewDeck(Deck deck)
        {
            var deckAux = new Deck(deck.Cardlist)
            {
                IdDeck = deck.IdDeck,
                IdAccount = deck.IdAccount,
                Cardlist = deck.Cardlist,

            };

            deckAux.generateID();


            context.Decks.Add(deckAux);
            context.SaveChanges();
            return deckAux;

        }

        public Deck? UpdateDeck(string id, Deck nDeck)
        {
            var deck = context.Decks.Find(id);
            if (deck == null) return null;
            deck.IdDeck = nDeck.IdDeck; //MAKE DECK ID
            deck.IdAccount = nDeck.IdAccount;
            deck.Cardlist = nDeck.Cardlist;

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

        public List<Deck>? GetDecksByUser(string Userid)
        {
            List<Deck> decks = context.Decks.Where(x=>x.IdAccount==Userid).ToList();
            return decks.Count == 0 ? null : decks;
        }
        
        public static List<T> Shuffle<T>(IEnumerable<T> list)
        {
            var random = new Random();
            var newList = list.OrderBy(item => random.Next()).ToList();
            return newList;
        }
    }
}
