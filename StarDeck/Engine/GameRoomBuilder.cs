﻿using System.Diagnostics;
using Stardeck.GameModels;
using Stardeck.Logic;
using Stardeck.Models;

namespace Stardeck.Engine;

public class GameRoomBuilder
{
    /// <summary>
    ///  Create a Runtime GameRoom from a database GameRoom and initialize it Gamelog if needed
    /// </summary>
    /// <param name="data"></param>
    public static GameRoom CreateInstance(Gameroom? data)
    {
        if (data is null) throw new Exception("Error al crear la partida");
        data.Gamelog ??= new Gamelog { Gameid = data.Roomid, Game = data };
        GameRoom game = new GameRoom
        {
            TokenSource = new CancellationTokenSource(),

            //assign space for terrutories
            Territories = new List<PlayableTerritory>(new PlayableTerritory[3]),
            //create object from room data
            Roomid = data.Roomid,
            Player1 = new Player(data.Player1Navigation),
            Player2 = new Player(data.Player2Navigation),
            Gamelog = data.Gamelog,
            Room = data
        };
        return game;
    }


    /// <summary>
    /// Initialize the game. THis method don create the instance only initialize the players and territories.
    /// constructor is required to be called before this method
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static GameRoom Init(StardeckContext context, GameRoom game)
    {
        var ready1 = InitPlayer(context, game.Player1);
        var ready2 = InitPlayer(context, game.Player2);

        var territories = AssignTerritories(context, game);

        if (!ready1 || !ready2 || !territories) throw new Exception("Error al inicializar la partida");
        SaveToDb(context, game);
        game._loop = Task.Run(game.TurnLoop);
        return game;
    }


    /// <summary>
    ///  Assign the territories to the game
    /// </summary>
    /// <param name="gameRoom"></param>
    /// <returns> true id assigned false if failed</returns>
    private static bool AssignTerritories(StardeckContext context, GameRoom gameRoom)
    {
        gameRoom.Territories[0] = GetRandomPlanetByDb(context);
        gameRoom.Territories[1] = GetRandomPlanetByDb(context);
        gameRoom.Territories[2] = new PlayableTerritory();
        gameRoom.Territory3 = GetRandomPlanetByDb(context);

        if (gameRoom.Territories.Any(t => t.Id is null))
        {
            return false;
        }

        return gameRoom.Territory3.Id is not null;
    }


    /// <summary>
    ///  Get a random planet using the planet list of the game and their probability
    /// </summary>
    /// <returns></returns>
    private static PlayableTerritory GetRandomPlanetByDb(StardeckContext context)
    {
        var random = new Random();
        var probability = random.Next(0, 100);
        var logic = new PlanetLogic(context);
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

        return new PlayableTerritory(planet);
    }


    private static void SaveToDb(StardeckContext context, GameRoom Room)
    {
        if (Room.Room != null)
        {
            Room.Room.Player1Navigation = null;
            Room.Room.Player2Navigation = null;
        }

        var room = context.Gamerooms.Find(Room.Roomid) ?? context.Gamerooms.Add(Room.Room).Entity;
        Debug.Assert(room != null, nameof(room) + " != null");
        room.Gamelog = Room.Gamelog;
        room.Winner = Room.Winner;
        room.Bet = Room.Bet;
        context.SaveChanges();
    }


    /// <summary>
    ///     Initialize player hand and deck and put player in game
    /// </summary>
    /// <param name="context"></param>
    /// <param name="gamePlayer"></param>
    /// <returns>Isplaying from the player</returns>
    public static bool InitPlayer(StardeckContext context, PlayerModel gamePlayer)
    {
        var deck = GetFavoriteDeck(context, gamePlayer);
        var hand = CreateInitialHand(deck);
        //remove hand cards from deck
        deck.RemoveAll(card => hand.Contains(card));
        //set deck and hand
        gamePlayer.Deck = deck;
        gamePlayer.Hand = hand;
        //put player in game
        gamePlayer.Account.isplaying = true;
        gamePlayer.Account.isInMatchMacking = false;

        return (bool)gamePlayer.Account.isplaying;
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
    /// <param name="stardeckContext"></param>
    /// <param name="player"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"> Throw when not a valid deck</exception> 
    private static List<GameCard> GetFavoriteDeck(StardeckContext stardeckContext, PlayerModel player)
    {
        //create deck
        var logicCard = new Logic.CardLogic(stardeckContext);
        var logicDeck = new Logic.DeckLogic(stardeckContext);
        var cards = logicDeck.GetDeck(player.Account.FavoriteDeck.Deckid).Decklist.ToList()
            .Select(card => logicCard.GetCard(card));
        var deck = (cards ?? throw new InvalidOperationException("El Deck seleccionado es invalido"))
            .Select(cardata => new GameCard(cardata)).ToList();
        //shuffle deck
        deck = Logic.DeckLogic.Shuffle(deck);
        return deck;
    }
}