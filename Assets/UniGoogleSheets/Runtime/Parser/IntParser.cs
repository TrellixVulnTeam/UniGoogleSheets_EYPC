using System;


public class IntParser : BaseParser
{
    /// <summary>
    /// in google sheet registered type.
    /// </summary>
    public override string[] TypeKeywords => new string[] {"int", "Int"}; 
     
    public override Type Type => typeof(int);

    public override object Read(string value)
    {
        return int.Parse(value);
    }

    public override string Write(object value)
    {
        return value.ToString();
    }
}