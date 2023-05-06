using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stardeck.Models;
[MetadataType(typeof(DeckMetaData))]


public partial class Deck
{

    static Constant maxcardcount = new StardeckContext().Constants.First(e => e.Key == "deckSize");
    public Deck(string[]? Deck1)
    {
        if (Deck1 != null)
        {
            this.Deck1 = Deck1;
            Decklist = new(Deck1);
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
        if (Decklist.Count <= maxcardcount.getAsInt())
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
        this.Deck1 = this.Decklist.ToArray();
    }

}

public class DeckMetaData
{
}