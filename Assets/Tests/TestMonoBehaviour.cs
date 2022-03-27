using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Data;
using UniGS;
using UniGS.Runtime;
using UnityEngine;

public class TestMonoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var data = UniGoogleSheets.SheetDataReader.GetSheetData("Game.Data", "Test");

        var code = UniGoogleSheets.SheetDataReader.GenerateCode("Game.Data", "Test");

        Game.Data.Test.Load();
        foreach (var keyValuePair in Game.Data.Test.Map)
        {
            Debug.Log(keyValuePair.Key);
        }
        Debug.Log(code);


        Test();
    }

    public async void Test()
    {
        UniGSWebRequester uniGsWebRequester = new UniGSWebRequester();
        var result = await uniGsWebRequester.Get("http://naver.com", null);
        Debug.Log(result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

 