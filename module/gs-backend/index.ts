//  Type Script Module Path : UniGoogleSheets\module\gs-backend

interface RequestBase{
    parameter : BaseParameter; // 쿼리 파라미터  
    action() : string;  // 액션 이름 
}
 
interface BaseParameter{
    method : string,
    action : string,
}

abstract class AbstractRequest implements RequestBase{
    parameter : any;
    action = () => null; 
    constructor(parameter : (e : any) => void){
        this.parameter = parameter;
    }
}


class ReqSpreadSheet extends AbstractRequest{ 
    parameter : any;
    action = () => 'api/spreadsheet'; 
}


function doGet(e: any): any{ 
    // 쿼리 파라미터
    const queryParameters =  e.parameter;
    const action : string = e.parameter.action;

}

function doPost(e: any): any{

}

