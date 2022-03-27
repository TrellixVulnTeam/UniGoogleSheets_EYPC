using System;
using UnityEngine;


public static class UniGoogleSheets
{
    public static readonly ParserContainer ParserContainer = new ParserContainer();
    public static readonly SheetDataReader SheetDataReader = new SheetDataReader("TableData/"); 
    public static IBaseParser GetParser(string typeKeyword)
    {
        try
        {
            return ParserContainer[typeKeyword].Parser;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    } 
}