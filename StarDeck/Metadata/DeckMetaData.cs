using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stardeck.Models;
[MetadataType(typeof(DeckMetaData))]


public partial class Deck: IAlphanumericID
{

    static Parameter maxcardcount = new StardeckContext().Parameters.First(e => e.Key == "deckSize");
    
    public Deck(string[]? Cardlist)
    {
        if (Cardlist != null)
        {
            this.Cardlist = Cardlist;
            Decklist = new(Cardlist);
        }
        else
        {
            this.Decklist ??= new();
        }
        this.Decklist.CollectionChanged += new(updateJson);
    }

    [NotMapped]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ObservableCollection<string>? Decklist;

    public string? addCard(string card)
    {
        if (Cardlist.Length < maxcardcount.getAsInt())
        {
            Decklist.Add(card);
            return card;
        }
        else
        {
            return null;
        }
    }

    private void updateJson(object sender, NotifyCollectionChangedEventArgs e)
    {
        this.Cardlist = this.Decklist.ToArray();
    }

    public string Id
    {
        get => IdDeck;
        set => IdDeck = value;
    }
    
    public void generateID()
    {
        ((IAlphanumericID)this).generateIdWithPrefix("D");
    }


}

public class DeckMetaData
{
}