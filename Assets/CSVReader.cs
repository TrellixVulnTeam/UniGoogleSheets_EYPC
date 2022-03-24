using System;
using System.Collections.Generic;
using System.IO;


public class SheetNamespaceDataMap : Dictionary<string, List<SheetClassData>>
{
}

public class SheetClassData
{
    public List<SheetFieldInfo> fieldInfos;
    public List<string> comments;
    public List<string> datas;
}

public struct SheetFieldInfo
{
    public string fieldTypeKeyword;
    public string fieldName;
}

  
/// <summary>
/// The class that represents the data of a single row in the table.
/// TODO :: 모든 Table 데이터는 CSV형태로 받아서 C# 내에서 처리한다. 
/// </summary>
public class CSVReader
{
    readonly char SPLIT_CHAR = '\t';

    public void Read(string @namespace, string @class, string csv)
    {
        SheetNamespaceDataMap sheetNamespaceData = new SheetNamespaceDataMap();
        SheetClassData classData = null;
        if (sheetNamespaceData.ContainsKey(@namespace) == false)
            sheetNamespaceData.Add(@namespace, new List<SheetClassData>());


        StringReader reader = new StringReader(csv);
        if (reader.Peek() == -1)
            throw new Exception("csv is empty");
        int currentRow = 0;
        while (true)
        {
            var line = reader.ReadLine();
            if (line == null)
                break;

            //delete all white space
            line = line.Replace(" ", null);
            if (currentRow == 0)
            {
                classData = new SheetClassData();
                var rows = line.Split(SPLIT_CHAR);
                for (var i = 0; i < rows.Length; i++)
                {
                    var nameAndKeyword = rows[i].Split(':');
                    var name = nameAndKeyword[0];
                    var keyword = nameAndKeyword[1];
                    classData.fieldInfos.Add(new SheetFieldInfo()
                    {
                        fieldName = name,
                        fieldTypeKeyword = keyword
                    });
                }

                sheetNamespaceData[@namespace].Add(classData);
            }

            if (currentRow >= 2)
            {
                var rows = line.Split(SPLIT_CHAR);
                for (var i = 0; i < rows.Length; i++)
                {
                    classData.datas.Add(rows[i]);
                }
            }

            currentRow++;
        }
    }
}