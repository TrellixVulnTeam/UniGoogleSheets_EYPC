using System;
using UnityEngine;


public class UniGoogleSheets
{
    public UniGoogleSheets()
    {
        this.context = new UniGoogleSheetsContext(new ParserContainer());
    }
    private UniGoogleSheetsContext context;
    public ParserContainer ParserContainer => context.ParserContainer;


    public IBaseParser GetParser(string typeKeyword)
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