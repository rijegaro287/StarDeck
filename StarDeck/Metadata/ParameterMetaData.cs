using System.ComponentModel.DataAnnotations;

namespace Stardeck.Models;

public partial class Parameter
{
    public string getAsString()
    {
        return Value.ToString();
    }

    public int getAsInt()
    {
        return int.Parse(Value);
    }

    public void set_Value(int value)
    {
        Value = value.ToString();
    }
}


