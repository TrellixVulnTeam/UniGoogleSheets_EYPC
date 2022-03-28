


function createSpreadSheet(spreadSheetName : string, folderId : string){
    const folder = DriveApp.getFolderById(folderId);
    const spreadSheet = SpreadsheetApp.create(spreadSheetName);  
    const file = DriveApp.getFileById(spreadSheet.getId());    
          file.moveTo(folder);
    return spreadSheet;
}

function getSpreadSheetInfos(spreadsheetId: string) {
    const spreadSheet = SpreadsheetApp.openById(spreadsheetId);
    const sheetData: SheetData[] = []; 
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
