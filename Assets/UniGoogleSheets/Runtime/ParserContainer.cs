using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

 
/// <summary>
/// 타입 정보를 저장하는 클래스
/// </summary>
public struct ParserData
{
    public System.Type Type;
    public IBaseParser Parser;
}


public class ParserContainer : Dictionary<string, ParserData>
{
    public ParserContainer ()
    {
        Initialize();
    }

    public void Initialize()
    {
        HashSet<string> duplicateDeclartionChecker = new HashSet<string>();
        Utility.GetAllSubclassOf(typeof(BaseParser)).ToList().ForEach(value =>
        {
            if (value.IsAbstract == false)
            {
                var typeInstance = Activator.CreateInstance(value) as BaseParser;
                var typeValueData = new ParserData()
                {
                    Type = typeInstance.Type,
                    Parser = Activator.CreateInstance(value) as IBaseParser
                };

                for (var i = 0; i < typeInstance.TypeKeywords.Length; i++)
                {
                    var key = typeInstance.TypeKeywords[i];
                    if (this.ContainsKey(key))
                        throw new Exception($"({typeInstance.GetType().Name})Duplicate Type Declaration : " + key +
                                            $" already used by {this[key].Parser.GetType().Name}");
                    this.Add(key, typeValueData);
                }
            }
        }); 
    }


    public void GetValue(string declaration, string value)
    {
        
    }
    
 
}