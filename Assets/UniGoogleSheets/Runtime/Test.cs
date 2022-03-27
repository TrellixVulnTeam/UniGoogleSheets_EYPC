using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System; 
using UniGS.Runtime;

namespace Game.Data {
    public class Test{
        public static List<Test> List = new List<Test>();
        public static Dictionary<Int32, Test> Map = new Dictionary<Int32,Test>();
        public int a;
        public int b;
        public int c;
        private static string FileName => "Game.Data.Test" + Extension;
        private static string Extension => ".csv" + Extension;
        public static void Load()
        {
 
            SheetData sheetData = UniGoogleSheets.SheetDataReader.GetSheetData(typeof(Test).Namespace, nameof(Test)); 
            FieldInfo[] fields = typeof(Test).GetFields(BindingFlags.Public | BindingFlags.Instance);
            int idx = 0;  
            for (int row = 0; row < sheetData.RowCount; row++)
            {
                var origin = new Test();
                var datas = sheetData.GetRowValues(row); 


                for(int col = 0; col < datas.Count; col++)
                {
                    var parserData = UniGoogleSheets.ParserContainer[sheetData.FieldInfos[col].FieldTypeKeyword]; 
                    var fieldValue = parserData.Parser.Read(datas[col]); // TODO :: 여기서 데이터를 처리한다.     
                    fields[col].SetValue(origin, fieldValue);
                } 

                List.Add(origin);
                Map.Add(origin.a, origin); 
            } 
        }
    }
} 