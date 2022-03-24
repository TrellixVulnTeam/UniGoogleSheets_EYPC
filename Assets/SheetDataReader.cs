using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NReco.Csv;
using UnityEngine;


public class SheetData
{
    public string NameSpace;
    public string ClassName;
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
public class SampleData
{
    static Dictionary<int, SampleData> Map = new Dictionary<int, SampleData>();
    static List<SampleData> List = new List<SampleData>();
    public int index;

    public static void Load()
    {
        SheetData origin = null;
        FieldInfo[] fields = origin.GetType().GetFields();
        int idx = 0;
        origin.Datas.ForEach(x =>
        {
            var sampleData = new SampleData();
            var parserData = UniGoogleSheets.ParserContainer[origin.FieldInfos[idx].FieldName];
            var value = parserData.Parser.Read(x); // TODO :: 여기서 데이터를 처리한다.  
            fields[idx].SetValue(value, sampleData);
            if (idx == origin.FieldInfos.Count)
                idx = 0;

            SampleData.Map.Add(sampleData.index, sampleData);
        });
    }
}


public interface ICSVFileReader
{
    string ReadFile(string path);
}


public abstract class AbstractCSVFileReader : ICSVFileReader
{
    protected String GetFullPathWithoutExtension(String path)
    {
        return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path),
            System.IO.Path.GetFileNameWithoutExtension(path));
    }

    public abstract string ReadFile(string path);
}

public class CSVFileReaderFromResources : AbstractCSVFileReader
{
    public override string ReadFile(string path)
    {
        var filePath = GetFullPathWithoutExtension(path);
        var asset = Resources.Load<TextAsset>(filePath);
        if (asset == null)
            throw new Exception("File not found : " + filePath);
        if (asset != null) return asset.text;
        return null;
    }
}
 
 
public class SheetDataReader
{
    public enum Mode
    {
        FROM_RESOURCE,
        FROM_STREAMING_ASSET, 
        FROM_FILE_PATH
    }

    private Mode mode;

    private Mode ResourceLoadMode
    {
        get => mode;
        set
        {
            if (value == Mode.FROM_RESOURCE)
            {
                this.mode = value;
                this._csvReader = new CSVFileReaderFromResources();
            }
        }
    }
    public SheetDataReader(string path)
    {
        CSV_DIR = path;
#if UNITY_STANDALONE
        ResourceLoadMode = Mode.FROM_RESOURCE;
#endif
    }

    readonly char SPLIT_CHAR = ',', QUOTE_CHAR = '"';
    private readonly string CSV_DIR = null;
    private ICSVFileReader _csvReader; 
    private string GetFileName(string @namespace, string @class) => $"{@namespace}.{@class}.csv"; 
    private string GetFileFullPath(string @namespace, string @class) =>
        Path.Combine(CSV_DIR, GetFileName(@namespace, @class));

    
    public string GenerateCode(string @namespace, string @class)
    {
        var data = ReadFromCSVData(@namespace, @class);
        var generator = new CodeGenerator(); 
        generator.UsingNamespace("System.Collections.Generic"); 
        generator.CreateClass(@namespace, @class); 
        
        // Add List
        { 
            var className = data.ClassName;
            generator.AddStaticField("public", $"List<{className}>", $"List", $"new List<{className}>()");
        }
        
        // Add Dictionary
        { 
            var key = UniGoogleSheets.ParserContainer[data.FieldInfos[0].FieldTypeKeyword].Parser.Type.Name;
            var className = data.ClassName;
            generator.AddStaticField("public", $"Dictionary<{key}, {className}>", $"Map", $"new Dictionary<{key},{className}>()");
        }
        
        
        // Add Member
        foreach (var fieldInfo in data.FieldInfos)
        {
            var name = fieldInfo.FieldName;
            var keyword = fieldInfo.FieldTypeKeyword; 
            generator.AddField("public",keyword, name, null);
        }

        return generator.GenerateCode();
    }
    public SheetData ReadFromCSVData(string @namespace, string @class)
    { 
        var csv = _csvReader.ReadFile(GetFileFullPath(@namespace, @class));
        SheetData data = null;
        using StringReader reader = new StringReader(csv);
        NReco.Csv.CsvReader csvReader = new CsvReader(reader, ",");


        if (reader.Peek() == -1)
            throw new Exception("csv is empty");
        var currentRow = 0;
        int kvCount = -1;
        while (csvReader.Read())
        {
            // read field range
            if (currentRow == 0)
            {
                data = new SheetData();
                data.NameSpace = @namespace;
                data.ClassName = @class;
                kvCount = csvReader.FieldsCount;
                for (var i = 0; i < csvReader.FieldsCount; i++)
                {
                    var nameAndKeyword = csvReader[i].Trim().Replace(" ", null).Split(':');
                    var name = nameAndKeyword[0];
                    var keyword = nameAndKeyword[1];
                    data.FieldInfos.Add(new SheetFieldInfo()
                    {
                        FieldName = name,
                        FieldTypeKeyword = keyword
                    });
                }
            }

            // read data range
            if (currentRow >= 2)
            {
                if (kvCount != csvReader.FieldsCount)
                    throw new Exception("csv file is invalid (row field count is not same) : " + @namespace + "." +
                                        @class);
                for (var i = 0; i < csvReader.FieldsCount; i++)
                    data.Datas.Add(csvReader[i]);
            }

            currentRow++;
        }

        return data;
    }
}