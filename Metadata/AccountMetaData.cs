﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Stardeck.Models;
[MetadataType(typeof(AccountMetadata))]
public partial class Account
{
    public Account(string? config)
    {
        this.Config=config;
        if (this.Config == "" | this.Config == null) { this.Serverconfig = new(this);  return; }
        this.Serverconfig = JsonConvert.DeserializeObject<ServerconfigutationDict<string, string>>(this.Config);
        this.Serverconfig ??= new(this);
        this.Serverconfig.main = this;
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

    public class ServerconfigutationDict<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
        [NotMapped]
        [JsonIgnore]
        public Account main { get; set; }

        public ServerconfigutationDict(Account account)
        {
            main = account;
        }

        public ServerconfigutationDict()
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
                main.Config = JsonConvert.SerializeObject(this);
            }

        }

    }
}



public class AccountMetadata
{
    [RegularExpression(@"^C-[a-zA-Z0-9]{12}")]
    [StringLength(maximumLength: 14, MinimumLength = 14)]
    public string Id { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
}
