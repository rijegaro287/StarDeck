using System.Text.RegularExpressions;

namespace Stardeck.Models;

public interface IAlphanumericID
{
    public string Id { get; set; }

    public abstract void generateID();


    void generateIdWithPrefix(string prefix)
    {
        while (!Regex.IsMatch(Id, $@"^{prefix}-[a-zA-Z0-9]{{12}}"))
        {
            Id = string.Concat(prefix, "-", System.Guid.NewGuid().ToString().Replace("-", "").AsSpan(0, 12));
        }

    }

}