using Stardeck.Models;

namespace Stardeck.GameModels
{
    public class Territory
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public long Type { get; set; }

        public bool? Active { get; set; }

        public TerritoryAbility? Ability { get; set; }

        public string? Winner { get; set; }
        public List<GameCard> player1Cards { get; set; } = new();

        public List<GameCard> player2Cards { get; set; } = new();

        public Territory(Planet data)
        {
            Id = data.Id;
            Name = data.Name;
            Type = data.Type;
            Active = data.Active;
            Ability = new TerritoryAbility(data.Ability);
        }

        public Territory()
        {
            Name = "Oculto";
            Type = 0;
            Active = false;
            Ability = new TerritoryAbility(null);
            Id = "0";
        }


        /// <summary>
        /// Play the list of cards played in this turn in the territory as a player1
        /// </summary>
        /// <param name="list"></param>
        public void PlayCardPlayer1(List<GameCard> list)
        {
            foreach (var card in list)
            {
                PlayCardPlayer1(card);
            }
        }

        /// <summary>
        ///  Play the list of cards played in this turn in the territory as a player2
        /// </summary>
        /// <param name="list"></param>
        public void PlayCardPlayer2(List<GameCard> list)
        {
            foreach (var card in list)
            {
                PlayCardPlayer2(card);
            }
        }

        /// <summary>
        /// Play a card in the territory as a player1
        /// </summary>
        /// <param name="card"></param>
        public void PlayCardPlayer1(GameCard card)
        {
            player1Cards.Add(card);
        }

        /// <summary>
        /// Play a card in the territory as a player2
        /// </summary>
        /// <param name="card"></param>
        public void PlayCardPlayer2(GameCard card)
        {
            player2Cards.Add(card);
        }

        public void checkWinner()
        {
            var points = GetPlayersPoints();
            if (points.player1 > points.player2)
            {
                Winner = "player1";
            }
            else if (points.player1 < points.player2)
            {
                Winner = "player2";
            }
            else
            {
                Winner = "Draw";
            }
        }

        public Points GetPlayersPoints()
        {
            Points points = new Points();
            player1Cards.ForEach(c => { points.player1 += c.Battlecost; });
            player1Cards.ForEach(c => { points.player1 += c.Battlecost; });
            return points;
        }

        public struct Points
        {
            public int player1;
            public int player2;

            public static Points operator +(Points a, Points b)
            {
                return new Points { player1 = a.player1 + b.player1, player2 = a.player2 + b.player2 };
            }
        }
    }
}