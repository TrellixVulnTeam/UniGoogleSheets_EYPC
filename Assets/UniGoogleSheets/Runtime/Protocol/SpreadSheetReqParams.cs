namespace UniGS.Runtime.Protocol
{
    public class SpreadSheetReqParams : ParameterBase
    { 
        public override string action => "api/spreadsheet/get"; 
        public string spreadSheetId;
        public SpreadSheetReqParams(string spreadSheetId)
        {
            this.spreadSheetId = spreadSheetId;
        }

    }
}