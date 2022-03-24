using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

 
/// <summary>
/// 타입 정보를 저장하는 클래스
/// </summary>
public struct TypeValueData
{
    public System.Type Type;
    public IBaseType Parser;
}


public class TypeMap : Dictionary<string, TypeValueData>
{ 
    public void Initialize()
    {
        HashSet<string> duplicateDeclartionChecker = new HashSet<string>();
        Utility.GetAllSubclassOf(typeof(BaseParser)).ToList().ForEach(value =>
        {
            if (value.IsAbstract == false)
            {
                var typeInstance = Activator.CreateInstance(value) as BaseParser;
                var typeValueData = new TypeValueData()
                {
                    Type = typeInstance.Type,
                    Parser = Activator.CreateInstance(value) as IBaseType
                };

                for (var i = 0; i < typeInstance.TypeDeclarations.Length; i++)
                {
                    var key = typeInstance.TypeDeclarations[i];
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