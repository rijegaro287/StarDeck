using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Runtime.Serialization;
using static Stardeck.Models.Collection;

namespace Stardeck.Models;
[MetadataType(typeof(DeckMetaData))]


public partial class Deck
{

    static int maxcardcount = 18;
    public Deck(string[]? Deck1)
    {
        this.Deck1 = Deck1;
        if (this.Deck1.Count() < 1 | this.Collectionlist == null)
        {
            this.Collectionlist = new(this);
            return;
        }
        this.Collectionlist = new(Deck1);
        this.Collectionlist ??= new DeckList();
        this.Collectionlist.main = this;
    }

    [NotMapped]
    public DeckList? Collectionlist;
    [Serializable]
    public class DeckList : List<string>
    {
        [NotMapped]
        [JsonIgnore]
        public Deck? main { get; set; }
        public DeckList(Deck Deck) : base(Deck.Deck1)
        {
            main = Deck;
        }
        public DeckList(string[] Deck) : base(Deck)
        {

        }
        public DeckList() : base()
        {
        }

        public new string this[int key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;

                if (main != null)
                {

                    main.Deck1 = base.ToArray();


                }

            }
        }
        public new void Add(string value)
        {
            if (base.Contains(value)) return;
            if (base.Count == maxcardcount) throw(new MaxCardCountSuppased("on "+main.IdAccount+" when added "+value));
                base.Insert(base.Count, value);

            main.Deck1 = base.ToArray();

        }

        public new string Remove(string item)
        {
            base.Remove(item);

            main.Deck1 = base.ToArray();

            return item;
        }
    }
    public class DeckMetaData
    {
    }
    [Serializable]
    public class MaxCardCountSuppased : Exception
    {
        public MaxCardCountSuppased():base() { }
        public MaxCardCountSuppased(string message) : base(message) { }
        public MaxCardCountSuppased(string message, Exception inner) : base(message, inner) { }
    }
}