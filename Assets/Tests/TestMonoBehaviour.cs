using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Data;
using UnityEngine;

public class TestMonoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UniGoogleSheets test = new UniGoogleSheets();
        var parser = test.GetParser("int");
        Debug.Log(parser.Read("1000")); 
        CodeGenerator generator = new CodeGenerator();
        generator.UsingNamespace("System.Collections.Generic"); 
        generator.CreateClass("Game.Test", "ClassNameTest"); 
        generator.AddField("int", "userId");
        generator.AddField("int", "userId2");
        generator.AddField("int", "userId3"); 
        generator.AddMethod("void", "GetUsers", "//code");

        var code = generator.GenerateCode();
        Debug.Log(code);  


        var data = test.CSVReader.CSVFileToSheetClassData("Game.Data", "Test");
        foreach (var t in data.Datas)
        {
            Debug.Log(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

 