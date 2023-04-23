using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Runtime.Serialization;

namespace Stardeck.Models;
[MetadataType(typeof(CollectionMetaData))]


public partial class Collection
{
    public Collection(string? collection1)
    {
        this.Collection1 = collection1;
        if (this.Collection1 == "" | this.Collectiondict == null) { this.Collectiondict = new(this); return; }
        this.Collectiondict = JsonConvert.DeserializeObject<CollectionDict<string, int>>(this.Collection1);
        this.Collectiondict ??= new CollectionDict<string, int>();
    }
    [NotMapped]
    public CollectionDict<string, int>? Collectiondict;
    [Serializable]
    public class CollectionDict<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
        [NotMapped]
        [JsonIgnore]
        public Collection? main { get; set; }
        public CollectionDict(Collection collection)
        {
            main = collection;
        }
        public CollectionDict():base()
        {
        }

        protected CollectionDict(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public new TValue this[TKey key]
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

                    main.Collection1 = JsonConvert.SerializeObject(this);

                }

            }
        }
    }
}
public class CollectionMetaData
    {
    }

