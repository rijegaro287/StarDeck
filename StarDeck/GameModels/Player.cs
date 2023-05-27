using Stardeck.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stardeck.GameModels
{
    public class Player
    {

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        private Account? Account { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public long Avatar { get; set; }

        public string? Config { get; set; }

        public long Points { get; set; }

        public long Coins { get; set; }

        [JsonIgnore] public long Energy { get; set; }

        [JsonIgnore] public List<GameCard> Deck { get; set; } = new List<GameCard>();

        [JsonIgnore] public List<GameCard> Hand { get; set; } = new List<GameCard>();

        [JsonIgnore]
        public List<Territory> TmpTerritories { get; set; } = new List<Territory>();
        
        
        public Player(Account? dataPlayer)
        {
            if (dataPlayer is null)
            {
                throw new InvalidOperationException("La informacion del jugador es invalida");
            }

            //create object from player data
            Id = dataPlayer.Id;
            Name = dataPlayer.Name;
            Nickname = dataPlayer.Nickname;
            Avatar = dataPlayer.Avatar;
            Config = dataPlayer.Config;
            Points = dataPlayer.Points;
            Coins = dataPlayer.Coins;
            Energy = 1;
            Account = dataPlayer;
        }

        /// <summary>
        ///     Initialize player hand and deck and put player in game
        /// </summary>
        /// <returns>Isplaying from the player</returns>
        public bool Init()
        {
            var deck = GetFavoriteDeck();
            var hand = CreateInitialHand(deck);
            //remove hand cards from deck
            deck.RemoveAll(card => hand.Contains(card));
            //set deck and hand
            Deck = deck;
            Hand = hand;
            //put player in game
            Account.isplaying = true;
            Account.isInMatchMacking = false;

            return (bool)Account.isplaying;
        }

        /// <summary>
        ///   Create initial hand
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        private static List<GameCard> CreateInitialHand(IEnumerable<GameCard> deck)
        {
            //select random cards from deck
            var random = new Random();
            var hand = deck.OrderBy(card => random.Next()).Take(5).ToList();
            return hand;
        }

        /// <summary>
        ///  Get favorite deck from player data
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"> Throw when not a valid deck</exception> 
        private List<GameCard> GetFavoriteDeck()
        {
            //create deck
            var logicCard = new Logic.CardLogic(new StardeckContext());
            var logicDeck = new Logic.DeckLogic(new StardeckContext());
            var cards = logicDeck.GetDeck(Account.FavoriteDeck.Deckid).Decklist.ToList()
                .Select(card => logicCard.GetCard(card));
            var deck = (cards ?? throw new InvalidOperationException("El Deck seleccionado es invalido"))
                .Select(cardata => new GameCard(cardata)).ToList();
            //shuffle deck
            deck = Logic.DeckLogic.Shuffle(deck);
            return deck;
        }

        public List<GameCard> GetHand()
        {
            return Hand;
        }

        public List<GameCard> GetDeck()
        {
            return Deck;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardid"></param>
        /// <param name="territory"></param>
        /// <returns>false if not enough energy, true if played, null if card is no in hand</returns>
        public bool? PlayCard(string cardid, int territory)
        {
            var card = Hand.Find(x => x.Id == cardid);
            if (card is null)
            {
                return null;
            }

            if (card.Energy > Energy)
            {
                return false;
            }

            Energy -= card.Energy;
            Hand.Remove(card);
            TmpTerritories[territory].PlayCard(card);
            return true;
        }

        public GameCard? DrawCard()
        {
            if (Deck.Count == 0) return null;
            var rand = Random.Shared.Next(Deck.Count);
            Hand.Add(Deck[rand]);
            Deck.RemoveAt(rand);
            return Hand.Last();
        }
    }
}