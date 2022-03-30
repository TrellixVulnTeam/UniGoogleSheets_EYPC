const ActionMap = {
    'api/spreadsheet/get' : (parameter : BaseParameter, body : any)=>{
        const reqParameter : ReqGetSpreadSheetParameter = parameter as ReqGetSpreadSheetParameter;  
        const spreadSheetId = reqParameter.spreadSheetId; 
        const sheetDatas = getSpreadSheetInfos(spreadSheetId);
        const response : JsonResponse<SpreadSheetInfo[]> = {
            code : 0,
            data : sheetDatas,
            message : "success"
        }; 
        return JSON.stringify(response);
    },
    'api/spreadsheet/write' : (parameter : BaseParameter, body : any)=>{
        return "Not Implements";
    },
    'api/spreadsheet/create' : (parameter : BaseParameter, body : any)=>{ 
        return "Not Implements";
    }
};


function doGet(e: any): any{ 
    // 쿼리 파라미터
    const queryParameters =  e.parameter;
    const action : string = e.parameter.action;

}

function doPost(e: any): any{

}  