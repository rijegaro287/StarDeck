using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stardeck.Models;

public partial class Collection
{
    public Collection(string[]? collection1)
    {
        if (collection1 != null)
        {
            this.Collection1 = collection1;
            Collectionlist = new ObservableCollection<string>(Collection1);
        }
        else
        {
            this.Collectionlist ??= new ObservableCollection<string>();
        }

        this.Collectionlist.CollectionChanged += new NotifyCollectionChangedEventHandler(updateJson);
    }

    [NotMapped] public ObservableCollection<string>? Collectionlist;

    private void updateJson(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e is { Action: NotifyCollectionChangedAction.Add, NewItems: not null })
            foreach (string eNewItem in e.NewItems)
            {
                if (Collectionlist != null && Collectionlist.Count(x => x == eNewItem) > 1)
                {
                    Collectionlist.Remove(eNewItem);
                }
            }

        this.Collection1 = this.Collectionlist.ToArray();
    }
}