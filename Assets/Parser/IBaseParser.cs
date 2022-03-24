using System;

public interface IBaseParser
{
    string[] TypeKeywords { get; } 
    /// <summary>
    /// Parse target Type
    /// </summary>
    Type Type { get; } 
    object Read(string value);
    string Write(object value); 
}