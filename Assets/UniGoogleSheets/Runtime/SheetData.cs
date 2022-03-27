using System.Collections.Generic;

public class SheetData
{
    public string NameSpace;
    public string ClassName;
    public List<SheetFieldInfo> FieldInfos = new List<SheetFieldInfo>();
    public List<string> Comments = new List<string>();
    public List<string> Datas = new List<string>();

    public int RowCount;
    public int FieldCount => FieldInfos.Count;
    
    public List<string> GetRowValues(int rowNumber)
    {
        List<string> rows = new List<string>();
        var start = rowNumber * FieldCount;
        var end = start + FieldCount;

        for (int i = start; i < end; i++)
            rows.Add(Datas[i]); 
        return rows;
    } 
}

public struct SheetFieldInfo
{
    public string FieldTypeKeyword;
    public string FieldName;
}