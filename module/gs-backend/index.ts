 


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