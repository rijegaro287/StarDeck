﻿using System.Diagnostics;
using System.Text.Json;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Stardeck.Models;

namespace Stardeck.GameModels
{
    public class GameRoom : GameModels.GameModel
    {
        /// <summary>
        ///  Create a Runtime GameRoom from a database GameRoom and intialize it Gamelog if needed
        /// </summary>
        /// <param name="data"></param>
        public GameRoom(Gameroom? data) : base()
        {
            //assign space for terrutories
            Territories = new List<Territory>(new Territory[3]);
            //create object from room data
            Roomid = data.Roomid;
            Player1 = new Player(data.Player1Navigation);
            Player2 = new Player(data.Player2Navigation);
            data.Gamelog ??= new Gamelog() { Gameid = data.Roomid, Game = data };
            Gamelog = data.Gamelog;
            Room = data;
        }


        /// <summary>
        /// Initialize the game. THis method don create the instance only initialize the players and territories.
        /// constructor is required to be called before this method
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GameRoom Init()
        {
            var ready1 = Player1.Init();
            var ready2 = Player2.Init();
            var territories = AssignTerritories();

            if (!ready1 || !ready2 || !territories) throw new Exception("Error al inicializar la partida");
            SaveToDb();
            return this;
        }

        public string? CheckWinner()
        {
            foreach (var territory in Territories)
            {
                territory.checkWinner();
            }

            var player1Win = Territories.Count(x => x.Winner == Player1.Id);
            var player2Win = Territories.Count(x => x.Winner == Player2.Id);

            if (player1Win > player2Win)
            {
                return Player1.Id;
            }

            if (player2Win > player1Win)
            {
                return Player2.Id;
            }

            var playerWithMorePoints = GetPlayerWithMaxPoints();
            if (playerWithMorePoints is null)
            {
                return "Draw";
            }

            return playerWithMorePoints;
        }

        /// <summary>
        /// Draw a card from the player deck
        /// </summary>
        /// <param name="playerid"></param>
        public void DrawCard(string playerid)
        {
            var player = FindPlayerOnGame(playerid);
            if (player is null) return;
            DrawCard(player);
        }

        /// <summary>
        ///  Draw a card from the player deck
        /// </summary>
        /// <param name="player"></param>
        private void DrawCard(Player player)
        {
            var drawed = player.DrawCard();
            if (drawed is null)
            {
                Gamelog?.LogDrawError(player.Id);
            }
            else
            {
                Gamelog?.LogDraw(player.Id, drawed.Id);
            }
        }


        public string RevealCardsOrder()
        {
            if (Turn.Equals(1))
            {
                return GetRandomPlayerId();
            }

            var player = GetPlayerWithMaxPoints();

            if (player is null)
            {
                return GetRandomPlayerId();
            }

            return player;
        }


        /// <summary>
        /// Play a card on the temporary player territory
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="cardid"></param>
        /// <param name="territoryid"></param>
        /// <returns>True if card played and energy reduced, false if not enough energy and null if invalid id</returns>
        public bool? PlayCard(string playerid, string cardid, string territoryid)
        {
            var territory = Territories.FindIndex(x => x.Id == territoryid);
            var player = FindPlayerOnGame(playerid);
            if (player is null)
            {
                return null;
            }

            var played = player.PlayCard(cardid, territory);
            if (played is null) Gamelog?.LogCard(playerid, cardid, territoryid);
            return played;
        }

        /// <summary>
        /// Get the player reference from the player id
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>player reference or null if not found</returns>
        public Player? FindPlayerOnGame(string playerId)
        {
            if (playerId == Player1.Id)
            {
                return Player1;
            }
            else if (playerId == Player2.Id)
            {
                return Player2;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the data of the player that is not included in the game data getter
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>Json string</returns>
        public string GetPlayerData(string playerId)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(playerId == Player1.Id ? Player1 : Player2,
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
        
        /// <summary>
        /// Get the player with the most points including all the territories
        /// </summary>
        /// <returns> player with the most points or null if draw</returns>
        public string? GetPlayerWithMaxPoints()
        {
            Territory.Points points = new();
            Territories.ForEach(t => { points += t.GetPlayersPoints(); });
            if (points.player1 == points.player2)
            {
                return null;
            }

            if (points.player1 > points.player2)
            {
                return Player1.Id;
            }

            return Player2.Id;
        }

        /// <summary>
        ///  Return the Deck of the player as a list of string
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>list of string or null when not founded the player</returns>
        public List<string>? GetDeck(string playerId)
        {
            if (playerId == Player1.Id)
            {
                return Player1.Deck.Select(x => x.Id).ToList();
            }

            return playerId == Player2.Id ? Player2.Deck.Select(x => x.Id).ToList() : null;
        }

        /// <summary>
        ///  Return the Hand of the player as a list of string
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns> list of string or null when not founded the player </returns>
        public List<string>? GetHand(string playerId)
        {
            if (playerId == Player1.Id)
            {
                return Player1.Hand.Select(x => x.Id).ToList();
            }

            return playerId == Player2.Id ? Player2.Hand.Select(x => x.Id).ToList() : null;
        }

        /// <summary>
        ///  Assign the territories to the game
        /// </summary>
        /// <returns> true id assigned false if failed</returns>
        private bool AssignTerritories()
        {
            Territories[0] = GetRandomPlanet();
            Territories[1] = GetRandomPlanet();
            Territories[2] = new Territory
            {
                Id = "0"
            };
            _territory3 = GetRandomPlanet();

            if (Territories.Any(t => t.Id is null))
            {
                return false;
            }

            return _territory3.Id is not null;
        }

        /// <summary>
        ///  Get a random planet using the planet list of the game and their probability
        /// </summary>
        /// <returns></returns>
        private static Territory GetRandomPlanet()
        {
            var random = new Random();
            var probability = random.Next(0, 100);
            var logic = new Logic.PlanetLogic(new StardeckContext());
            var planets = logic.GetAll().GroupBy(x => x.Type).OrderByDescending(x => x.Key);
            var planetsList = probability switch
            {
                < 15 => planets.First(x => x.Key == 0).ToList(),
                < 50 => planets.First(x => x.Key == 1).ToList(),
                < 101 => planets.First(x => x.Key == 2).ToList(),
                _ => planets.First(x => x.Key == 2).ToList()
            };

            //select random planet from list
            var planet = planetsList[random.Next(0, planetsList.Count)];

            return new Territory(planet);
        }

        private string GetRandomPlayerId()
        {
            return Random.Shared.Next(0, 2) == 0 ? Player1.Id : Player2.Id;
        }


        private void SaveToDb()
        {
            var context = new StardeckContext();
            Room.Player1Navigation = null;
            Room.Player2Navigation = null;
            var room = context.Gamerooms.Find(Roomid) ?? context.Gamerooms.Add(Room).Entity;
            room.Gamelog = Gamelog;
            room.Winner = Winner;
            room.Bet = Bet;
            context.SaveChanges();
        }
    }
}