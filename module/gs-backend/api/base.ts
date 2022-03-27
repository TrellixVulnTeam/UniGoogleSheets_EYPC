//  Type Script Module Path : UniGoogleSheets\module\gs-backend

interface RequestBase{
    parameter : BaseParameter; // 쿼리 파라미터  
    action() : string;  // 액션 이름 
}
 
interface BaseParameter{
    method : string;
    action : string;  
    doResponse : () => any;
}

abstract class AbstractRequest implements RequestBase{
    parameter : any;
    action = () => null; 

    doResponse (){
        return this.parameter.doResponse();
    }
    
    constructor(parameter : (e : any) => void){
        this.parameter = parameter;
    }
    

}