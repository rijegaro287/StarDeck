using Stardeck.Models;

namespace Stardeck.Logic
{
    public class DeckLogic
    {

        private readonly StardeckContext context;

        public DeckLogic(StardeckContext context)
        {
            this.context = context;
        }


        public List<Deck> GetAll()
        {
            List<Deck> decks = context.Decks.ToList();
            if (decks.Count == 0)
            {
                return null;
            }
            return decks;
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
            var deckAux = new Deck(deck.Deck1)
            {
                IdDeck = "00012", //MAKE DECK ID
                IdAccount = deck.IdAccount,
                Deck1 = deck.Deck1,

            };
            context.Decks.Add(deckAux);
            context.SaveChanges();
            return deckAux;

        }

        public Deck UpdateDeck(string id, Deck nDeck)
        {
            var deck= context.Decks.Find(id);
            if (deck != null)
            {
                deck.IdDeck =nDeck.IdDeck; //MAKE DECK ID
                deck.IdAccount = nDeck.IdAccount;
                deck.Deck1 = nDeck.Deck1;

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
    }
}
