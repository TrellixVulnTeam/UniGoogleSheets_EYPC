using System;
using UnityEngine;


namespace UniGS.Runtime
{
    public static class UniGoogleSheets
    {
        public static readonly ParserContainer ParserContainer = new ParserContainer();
        public static readonly SheetDataReader SheetDataReader = new SheetDataReader("TableData/");
        public static readonly IWebRequester WebRequester = new UniGSWebRequester();


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
}