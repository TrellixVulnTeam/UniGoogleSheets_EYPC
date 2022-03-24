using System;
using UnityEngine;


public class UniGoogleSheets
{
    public UniGoogleSheets()
    {
        this.context = new UniGoogleSheetsContext(new TypeMap());
    }
    private UniGoogleSheetsContext context;
    public TypeMap TypeMap => context.TypeMap;


    public IBaseType GetParser(string typeKeyword)
    {
        try
        {
            return TypeMap[typeKeyword].Parser;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    } 
}