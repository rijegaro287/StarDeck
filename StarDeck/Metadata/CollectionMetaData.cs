using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        if (collection1 != null)
        {
            this.Collection1 = collection1;
            Collectionlist = new(Collection1);
        }
        else
        {
            this.Collectionlist ??= new();
        }
        this.Collectionlist.CollectionChanged += new(updateJson);
    }

  [NotMapped]
  public ObservableCollection<string>? Collectionlist;

    private void updateJson(object sender, NotifyCollectionChangedEventArgs e)
    {
        this.Collection1 = this.Collectionlist.ToArray();
    }

}
public class CollectionMetaData
{
}
