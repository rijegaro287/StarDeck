﻿using System.ComponentModel.DataAnnotations;

namespace Stardeck.Models;
[MetadataType(typeof(ParameterMetaData))]

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


/**
public class ConstantElement<T> : Constant
{
     private T? _value;
     public new T? Value { get { return _value; } set { base.Value = value.ToString(); _value = value; } }


     public ConstantElement(string? Key, string? Value)
    {
        this.Key = Key;
        this.Value = internalParse(Value);
    }
    virtual protected  T? internalParse(string value) { return (T)Convert.ChangeType(value, typeof(T));  }
}
**/

public class ParameterMetaData
{
}

