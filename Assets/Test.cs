using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Game.Data
{
    /// <summary>
    /// The class that represents the data of a single row in the table.
    /// TODO :: 모든 Table 데이터는 CSV형태로 받아서 C# 내에서 처리한다.  
    public class Test
    {
        static Dictionary<int, Test> Map = new Dictionary<int, Test>();
        static List<Test> List = new List<Test>();
        private static string FileName => "Game.Data.SampleData" + Extension;
        private static string Extension => ".csv";
        
        
        public int a;
        public int b;
        public int c;

        public static void Load()
        { 
            SheetData sheetData = UniGoogleSheets.SheetDataReader.GetSheetData(typeof(Test).Namespace, nameof(Test)); 
            FieldInfo[] fields = typeof(Test).GetFields();
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
            }  
        }

        public static IEnumerator LoadFromGoogle()
        {
            yield return null;
        }
    }
}
 