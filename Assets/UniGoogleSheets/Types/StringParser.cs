using System;


public class StringParser : BaseParser
{
    /// <summary>
    /// in google sheet registered type.
    /// </summary>
    public override string[] TypeDeclarations => new string[] {"string", "String"}; 
     
    public override Type Type => typeof(int);

    public override object Read(string value)
    {
        return value;
    }

    public override string Write(object value)
    {
        return value.ToString();
    }
}