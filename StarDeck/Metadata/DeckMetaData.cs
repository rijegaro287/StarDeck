using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stardeck.Models;
[MetadataType(typeof(DeckMetaData))]


public partial class Deck
{

    static Parameter maxcardcount = new StardeckContext().Parameters.First(e => e.Key == "deckSize");
    public Deck(string[]? Deck1)
    {
        if (Deck1 != null)
        {
            this.Decklist = Deck1;
            DecklistObject = new(Deck1);
        }
        else
        {
            this.DecklistObject ??= new();
        }
        this.DecklistObject.CollectionChanged += new(updateJson);
    }

    [NotMapped]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public ObservableCollection<string>? DecklistObject;

    public string? addCard(string card)
    {
        if (Decklist.Count() <= maxcardcount.getAsInt())
        {
            DecklistObject.Add(card);
            return card;
        }
        else
        {
            return null;
        }
    }

    private void updateJson(object sender, NotifyCollectionChangedEventArgs e)
    {
        this.Decklist = this.DecklistObject.ToArray();
    }

}

public class DeckMetaData
{
}