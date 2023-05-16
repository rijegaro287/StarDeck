using Stardeck.Models;
using System.Text.RegularExpressions;

namespace Stardeck.Logic
{
  public class CardLogic
  {
    private readonly StardeckContext context;

    public CardLogic(StardeckContext context)
    {
      this.context = context;
    }

    public List<Card> GetAll()
    {
      List<Card> cards = context.Cards.ToList();
      if (cards.Count == 0)
      {
        return null;
      }
      return cards;
    }

    public Card GetCard(string id)
    {
      //var card = context.Cards.Where(x=> x.Id==id).Include(x=>x.Navigator);
      var card = context.Cards.Find(id);

      if (card == null)
      {
        return null;
      }
      return card;
    }


    public Card NewCard(CardImage card)
    {
      var cardAux = new Card()
      {
        Id = card.Id,
        Name = card.Name,
        Energy = card.Energy,
        Battlecost = card.Battlecost,
        Image = Convert.FromBase64String(card.Image.ToString()),
        Active = card.Active,
        Type = card.Type,
        Ability = card.Ability,
        Description = card.Description,
        Race = card.Race

      };


      context.Cards.Add(cardAux);

      context.SaveChanges();
      return card;

    }

    public Card UpdateCard(string id, Card nCard)
    {
      var card = context.Cards.Find(id);
      if (card != null)
      {
        card.Name = nCard.Name;
        card.Energy = nCard.Energy;
        card.Battlecost = nCard.Battlecost;
        card.Image = nCard.Image;
        card.Active = nCard.Active;
        card.Type = nCard.Type;
        card.Ability = nCard.Ability;
        card.Description = nCard.Description;
        card.Race = nCard.Race;

        context.SaveChanges();
        return card;
      }
      return null;

    }

    public Card DeleteCard(string id)
    {
      var card = context.Cards.Find(id);
      if (card != null)
      {
        //REMOVER LOS DATOS ASOCIADOS EN OTRAS TABLAS
        context.Remove(card);
        context.SaveChanges();
        return card;
      }
      return null;
    }

    public List<Card> GetNineCards()
    {
      List<Card> filteredCards = GetAll().FindAll(x => x.Type == 1 || x.Type == 2);
      Random rand = new Random();
      var shuffled = filteredCards.OrderBy(_ => rand.Next()).ToList();
      return shuffled.GetRange(0, 9);
    }

  }
}