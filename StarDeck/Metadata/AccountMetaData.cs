using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stardeck.Models;
public partial class Account:IAlphanumericID
{
    public Account(string? config)
    {
        this.Config = config;
        if (this.Config == "" | this.Config == null) { this.Serverconfig = new(this); return; }
        try
        {
            this.Serverconfig = JsonConvert.DeserializeObject<ServerconfigutationDict<string, string>>(this.Config);
            this.Serverconfig.main = this;

        }
        catch (JsonReaderException e)
        {
            this.Serverconfig ??= new(this);
        }
    }
    

    [NotMapped]
    public DateTime? lastacces { get; set; } = null;
    [NotMapped]
    public bool? logged { get; set; } = false;

    [NotMapped]
    public bool? isplaying { get; set; } = false;

    [NotMapped]
    public bool? isInMatchMacking { get; set; } = false;

    [NotMapped]
    public string? roomid { get; set; } = null;

    [NotMapped]
    public ServerconfigutationDict<string, string>? Serverconfig;
    [Serializable]
    public class ServerconfigutationDict<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
        [NotMapped]
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Account? main { get; set; }

        public ServerconfigutationDict(Account account) => main = account;

        protected ServerconfigutationDict(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
        public ServerconfigutationDict() : base()
        {
        }


        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set
            {
                base[key] = value;
                if (main != null)
                {
                    main.Config = JsonConvert.SerializeObject(this);
                }
            }

        }

        public new void Clear()
        {

            base.Clear();
            if (main != null)
            {
                main.Config = JsonConvert.SerializeObject(this);
            }

        }
        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            if (main != null)
            {

                main.Config = JsonConvert.SerializeObject(this);

            }
        }

    }

    public void generateID()
    {
        ((IAlphanumericID)this).generateIdWithPrefix("U");
    }
}



