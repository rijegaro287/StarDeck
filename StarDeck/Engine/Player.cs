using Stardeck.GameModels;
using Stardeck.Models;

namespace Stardeck.Engine;

public class Player : PlayerModel
{
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

    if (card.Energy >= Energy)
    {
      return false;
    }

    Energy -= card.Energy;
    Hand.Remove(card);
    TmpTerritories[territory].Add(card);
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

  public void CleanTmpTerritories()
  {
    foreach (var tmpTerritory in TmpTerritories)
    {
      tmpTerritory.Clear();
    }
  }

  public void SetEnergy(int turn)
  {
    Energy = 1 + turn;

  }

}