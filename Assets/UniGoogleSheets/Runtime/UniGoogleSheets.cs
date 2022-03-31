using System;
using System.Threading.Tasks;
using Data;
using UniGS.Runtime.Protocol;
using UnityEngine;


namespace UniGS.Runtime
{
    public static class UniGoogleSheets
    {
        public static readonly ParserContainer ParserContainer = new ParserContainer();
        public static readonly SheetDataReader SheetDataReader = new SheetDataReader("TableData/");
        public static readonly IWebRequester WebRequester = new UniGSWebRequester();

        public static readonly string SCRIPT_URL = "";
        public static void LoadAll()
        {
            
        }


        static async Task<SpreadSheetInfo> GetSpreadSheet(string spreadSheetId)
        {
            SpreadSheetReqParams param = new SpreadSheetReqParams(spreadSheetId);
            var query        = param.ToQueryParameter();
            var responseBody = await WebRequester.Get(SCRIPT_URL, query);
            var spreadSheetInfo   = JsonUtility.FromJson<SpreadSheetInfo>(responseBody);
            return spreadSheetInfo;
        }

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