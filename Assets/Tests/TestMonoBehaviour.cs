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
        var data = UniGoogleSheets.SheetDataReader.ReadFromCSVData("Game.Data", "Test");

        var code = UniGoogleSheets.SheetDataReader.GenerateCode("Game.Data", "Test");

        Debug.Log(code);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

 