


function createSpreadSheet(spreadSheetName : string, sheetName : string, folderId : string){
    const folder = DriveApp.getFolderById(folderId);
    const spreadSheet = SpreadsheetApp.create(spreadSheetName);  
          spreadSheet.getSheets[0].setName(sheetName);

    const file = DriveApp.getFileById(spreadSheet.getId());    
          file.moveTo(folder);
    return spreadSheet;
}
 
function writeObjectToSpreadSheet(spreadSheetId: string, sheetName: string, data: string[]) : boolean{
    const spreadSheet = SpreadsheetApp.openById(spreadSheetId);
    const sheet = spreadSheet.getSheetByName(sheetName);
    const lastRow = sheet.getLastRow(); 
    const range = sheet.getDataRange(); 
    const table = range.getValues(); 
    let isWrite = false;
    //값이 중복되면 덮어쓰기
    for (var row = DATA_ROW; row < table.length; row++) { 
        if(table[row][0] == data[0]){
            sheet.getRange(row + 1, 1, 1, table[0].length).setValues([data]);
            isWrite = true;
            return true;
            break;
        }
    }

    if(isWrite === false){
        sheet.getRange(lastRow + 1, 1, 1, data.length).setValues([data]); 
        return true;
    } 


    return false;
} 

function getSpreadSheetInfos(spreadsheetId: string) : SpreadSheetInfo[]{
    const spreadSheet = SpreadsheetApp.openById(spreadsheetId);
    const sheetData: SpreadSheetInfo[] = []; 
    spreadSheet.getSheets().forEach(sheet => {
        const sheetName = sheet.getName();
        const data = sheet.getDataRange().getValues();
        
        let csv = "";
        if (data.length > 1) { 
            for (var row = 0; row < data.length; row++) {
                for (var col = 0; col < data[row].length; col++) {
                    if (data[row][col].toString().indexOf(",") != -1) {
                        data[row][col] = "\"" + data[row][col] + "\"";
                    }
                } 
                if (row < data.length - 1) {
                    csv += data[row].join(",") + "\r\n";
                }
                else {
                    csv += data[row];
                }
            }
        } 
        else{
            csv = null;
        } 
        sheetData.push({ spreadSheetName: spreadSheet.getName(), sheetName: sheetName, csv: csv });
    });

    return sheetData;
}
