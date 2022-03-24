using System;
using System.Collections.Generic;
using System.IO;
using NReco.Csv;
using UnityEngine;


public class SheetClassData
{
    public string NameSpace;
    public List<SheetFieldInfo> FieldInfos = new List<SheetFieldInfo>();
    public List<string> Comments = new List<string>();
    public List<string> Datas = new List<string>();
}

public struct SheetFieldInfo
{
    public string FieldTypeKeyword;
    public string FieldName;
}
 
  
/// <summary>
/// The class that represents the data of a single row in the table.
/// TODO :: 모든 Table 데이터는 CSV형태로 받아서 C# 내에서 처리한다. 
/// </summary>


public class CSVReader
{
    public CSVReader (string path)
    {
        CSV_DIR = path;
    }

    readonly char SPLIT_CHAR = ',', QUOTE_CHAR = '"';
    private readonly string CSV_DIR = null;

    private String GetFullPathWithoutExtension(String path)
    {
        return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileNameWithoutExtension(path));
    }
    
    private string ReadFileFromResources(string path)
    {
        var filePath = GetFullPathWithoutExtension(path);
        var asset =  Resources.Load<TextAsset>(filePath);
        if (asset == null)
            throw new Exception("File not found : " + filePath); 
        if (asset != null) return asset.text; 
        return null;
    } 
    
     
    private string GetFileName(string @namespace, string @class) => $"{@namespace}.{@class}.csv";
    private string GetFileFullPath(string @namespace, string @class) => Path.Combine(CSV_DIR, GetFileName(@namespace, @class));

    public SheetClassData CSVFileToSheetClassData(string @namespace, string @class)
    {
        var csv = ReadFileFromResources(GetFileFullPath(@namespace, @class));
        SheetClassData classData = null; 
        using StringReader reader = new StringReader(csv); 
        NReco.Csv.CsvReader csvReader = new CsvReader(reader, ",");
        
        
        if (reader.Peek() == -1)
            throw new Exception("csv is empty");
        var currentRow = 0;
        while (csvReader.Read())
        { 
            // read field range
            if (currentRow == 0)
            {
                classData = new SheetClassData();
                classData.NameSpace = @namespace; 
                for (var i = 0; i < csvReader.FieldsCount; i++)
                {
                    var nameAndKeyword = csvReader[i].Split(':');
                    var name = nameAndKeyword[0];
                    var keyword = nameAndKeyword[1];
                    classData.FieldInfos.Add(new SheetFieldInfo()
                    {
                        FieldName = name,
                        FieldTypeKeyword = keyword
                    });
                }  
            }

            // read data range
            if (currentRow >= 2)
            { 
                for (var i = 0; i < csvReader.FieldsCount; i++)
                {
                    classData.Datas.Add(csvReader[i]);
                }
            }

            currentRow++;
        }

        return classData;
    }
}