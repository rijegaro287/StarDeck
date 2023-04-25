using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Runtime.Serialization;
using static Stardeck.Models.Collection;

namespace Stardeck.Models;
[MetadataType(typeof(CollectionMetaData))]


public partial class Collection
{
  public Collection(string[]? collection1)
  {
    this.Collection1 = collection1;
    if (this.Collection1.Count() < 1 | this.Collectionlist == null) { this.Collectionlist = new(this); return; }
    this.Collectionlist = new(collection1);
    this.Collectionlist ??= new CollectionList();
    this.Collectionlist.main = this;
  }

  [NotMapped]
  public CollectionList? Collectionlist;
  [Serializable]
  public class CollectionList : List<string>
  {
    [NotMapped]
    [JsonIgnore]
    public Collection? main { get; set; }
    public CollectionList(Collection collection)
    {
      main = collection;
    }
    public CollectionList(string[] collection1) : base(collection1)
    {

    }
    public CollectionList() : base()
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

          main.Collection1 = base.ToArray();


        }

      }
    }
    public new void Add(string value)
    {
      if (base.Contains(value)) return;
      base.Add(value);
      main.Collection1 = base.ToArray();

    }

    public new string Remove(string item)
    {
      base.Remove(item);

      main.Collection1 = base.ToArray();

      return item;
    }
  }
  public class CollectionMetaData
  {
  }
}

