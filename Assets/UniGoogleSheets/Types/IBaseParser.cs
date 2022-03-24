using System;

public interface IBaseType
{
    string[] TypeDeclarations { get; } 
    Type Type { get; } 
    object Read(string value);
    string Write(object value); 
}