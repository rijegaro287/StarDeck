using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using static Stardeck.Models.Collection;

namespace Stardeck.Models;
[MetadataType(typeof(DeckMetaData))]


public partial class Deck
{

    static int maxcardcount = 18;
    public Deck(string[]? Deck1)
    {
        if (Deck1!=null)
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
    
    
    public class DeckMetaData
    {
    }

    private void updateJson( object sender, NotifyCollectionChangedEventArgs e)
    {
        this.Deck1=this.Decklist.ToArray();
    }

}