
function TEST_READ_SPREAD_SHEET() : void {
    const sheetId : string = '1o9UdnBx0RRbftOy2stTSnGtnpYZIKnbwLP5pX2HsWz8';
    const sheetData = getSpreadSheetInfos(sheetId);


    console.log('spreadSheetName name : ' + sheetData[0].spreadSheetName);
    console.log('sheet name : ' + sheetData[0].sheetName);
    console.log('csv : ' + sheetData[0].csv);
    
}

function TEST_WRITE_SPREAD_SHEET(){
    
    const spreadSheetId : string = '1o9UdnBx0RRbftOy2stTSnGtnpYZIKnbwLP5pX2HsWz8';

    writeObjectToSpreadSheet('1Hn0u-_Wg9mEADDcmFpvMfMn-wLyOqlx_3b5wHBbJpBkyEH89hI32shfr', 'ClassName', ['100','200','300']);
}