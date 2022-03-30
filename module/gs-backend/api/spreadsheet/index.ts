interface ReqGetSpreadSheet extends RequestBase{ 
     
}

interface ReqGetSpreadSheetParameter extends BaseParameter{
    spreadSheetId : string;
}

interface SheetData{
    spreadSheetName : string;
    sheetName : string;
    csv : string;
}

