using Stardeck.DbAccess;
using Stardeck.Models;

namespace Stardeck.Logic
{
    public class DeckLogic
    {

        private readonly StardeckContext context;
        private readonly DeckDb deckDB;
        private readonly ILogger _logger;
        public DeckLogic(StardeckContext context, ILogger logger)
        {
            _logger = logger;
            this.context = context;
            this.deckDB = new DeckDb(context);
        }


        public List<Deck>? GetAll()
        {
            List<Deck> decks = deckDB.GetAllDecks();
            if (decks == null)
            {
                _logger.LogWarning("No existen deks en GetAll");
                return null;
            }
            _logger.LogInformation("Request GetAll de Decks completada");
            return decks;
        }

        public Object GetNames(string userId)
        {
            var decks = deckDB.GetNames(userId);
            if (decks.Equals(0))
            {
                _logger.LogWarning("No existen deks en para el usuario {userId} en GetNames", userId);
                return 0;
            }
            _logger.LogInformation("Request GetNames de Decks para ususario {userId} completada", userId);
            return decks;
        }



        public Deck? GetDeck(string id)
        {
            //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
            var deck = deckDB.GetDeck(id);
            _logger.LogInformation("Request GetDeck para ususario {userId} completada", id);
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
            if (deckDB.GetDeck(deckAux.Id) == null)
            {
                _logger.LogWarning("No se pudo guardar el nuevo deck {deck.Id}", deck.Id);
                return null;
            }
            _logger.LogInformation("Request NewDeck para deck {deck.Id} completada", deck.Id);
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
                _logger.LogInformation("Request UpdateDeck para deck {deck.Id} completada", deck.Id);
                return deck;
            }
            _logger.LogWarning("No existe el deck {id} en UpdateDeck", id);
            return null;

        }

        public Deck DeleteDeck(string id)
        {
            var deck = deckDB.DeleteDeck(id);
            if (deck != null)
            {
                _logger.LogWarning("No existe el deck {id} en DeleteDeck", id);
                return deck;
            }
            _logger.LogInformation("Request DeleteDeck para deck {id} completada", id);
            return null;
        }

        public List<Deck>? GetDecksByUser(string Userid)
        {
            List<Deck> decks = deckDB.GetDecksByUser(Userid);
            if (decks == null)
            {
                _logger.LogWarning("El usuario {Userid} no tiene decks", Userid);
                return null;
            }
            _logger.LogInformation("Request GetDecksByUser para usuario {id} completada", Userid);
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
