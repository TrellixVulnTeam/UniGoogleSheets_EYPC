using System;
using System.IO;
using System.Threading.Tasks;
using UniGS;
using UniGS.NReco.Csv;

namespace UniGS.Runtime
{
    public class SheetDataReader
    {
        public enum Mode
        {
            FROM_RESOURCE,
            FROM_STREAMING_ASSET,
            FROM_FILE_PATH
        }

        private Mode mode;

        private Mode ResourceLoadMode
        {
            get => mode;
            set
            {
                if (value == Mode.FROM_RESOURCE)
                {
                    this.mode = value;
                    this._csvReader = new ResourceLoaderFromResources();
                }
            }
        }

        public SheetDataReader(string path)
        {
            CSV_DIR = path;
#if UNITY_STANDALONE
            ResourceLoadMode = Mode.FROM_RESOURCE;
#endif
        }

        readonly char SPLIT_CHAR = ',', QUOTE_CHAR = '"';

        /// <summary>
        /// CSV Load Path
        /// </summary>
        private readonly string CSV_DIR = null;

        private IResourceLoader _csvReader;


        private string GetFileName(string @namespace, string @class) => $"{@namespace}.{@class}.csv";

        private string GetFileFullPath(string @namespace, string @class) =>
            Path.Combine(CSV_DIR, GetFileName(@namespace, @class));


        public string GenerateCode(string @namespace, string @class)
        {
            var data = GetSheetData(@namespace, @class);
            var generator = new CodeGenerator();
            generator.UsingNamespace("System.Collections.Generic");
            generator.UsingNamespace("System.Reflection");
            generator.UsingNamespace("System.Collections");
            generator.UsingNamespace("System");
            generator.CreateClass(@namespace, @class + " : IGeneratedTable");

            // Add List
            {
                var className = data.ClassName;
                generator.AddStaticField("public", $"List<{className}>", $"List", $"new List<{className}>()");
            }

            // Add Dictionary
            {
                var key = UniGoogleSheets.ParserContainer[data.FieldInfos[0].FieldTypeKeyword].Parser.Type.Name;
                var className = data.ClassName;
                generator.AddStaticField("public", $"Dictionary<{key}, {className}>", $"Map",
                    $"new Dictionary<{key},{className}>()");
            }


            // Add Member
            foreach (var fieldInfo in data.FieldInfos)
            {
                var name = fieldInfo.FieldName;
                var keyword = fieldInfo.FieldTypeKeyword;
                generator.AddField("public", keyword, name, null);
            }


            generator.AppendLine($@"private static string FileName => ""{@namespace}.{@class}"" + Extension;");
            generator.AppendLine($@"private static string Extension => "".csv"" + Extension;");

            var keyFieldName = data.FieldInfos[0].FieldName;
            generator.AddStaticMethod("public", "void", "Load", $@"
SheetData sheetData = UniGoogleSheets.SheetDataReader.GetSheetData(typeof({@class}).Namespace, nameof({@class})); 
FieldInfo[] fields = typeof({@class}).GetFields(BindingFlags.Public | BindingFlags.Instance);
int idx = 0;  
for (int row = 0; row < sheetData.RowCount; row++)
{{
var origin = new {@class}();
var datas = sheetData.GetRowValues(row); 


for(int col = 0; col < datas.Count; col++)
{{
var parserData = UniGoogleSheets.ParserContainer[sheetData.FieldInfos[col].FieldTypeKeyword]; 
var fieldValue = parserData.Parser.Read(datas[col]); // TODO :: 여기서 데이터를 처리한다.     
fields[col].SetValue(origin, fieldValue);
}} 

List.Add(origin);
Map.Add(origin.{keyFieldName}, origin); 
}}



");
            return generator.GenerateCode();
        }

        public SheetData GetSheetData(string @namespace, string @class)
        {
            var csv = _csvReader.ReadFile(GetFileFullPath(@namespace, @class));
            SheetData data = null;
            using StringReader reader = new StringReader(csv);
            UniGS.NReco.Csv.CsvReader csvReader = new CsvReader(reader, ",");


            if (reader.Peek() == -1)
                throw new Exception("csv is empty");
            var currentRow = 0;
            int kvCount = -1;
            while (csvReader.Read())
            {
                // read field range
                if (currentRow == 0)
                {
                    data = new SheetData();
                    data.NameSpace = @namespace;
                    data.ClassName = @class;
                    kvCount = csvReader.FieldsCount;
                    for (var i = 0; i < csvReader.FieldsCount; i++)
                    {
                        var nameAndKeyword = csvReader[i].Trim().Replace(" ", null).Split(':');
                        var name = nameAndKeyword[0];
                        var keyword = nameAndKeyword[1];
                        data.FieldInfos.Add(new SheetFieldInfo()
                        {
                            FieldName = name,
                            FieldTypeKeyword = keyword
                        });
                    }
                }

                // read data range
                if (currentRow >= 2)
                {
                    if (kvCount != csvReader.FieldsCount)
                        throw new Exception("csv file is invalid (row field count is not same) : " + @namespace + "." +
                                            @class);
                    for (var i = 0; i < csvReader.FieldsCount; i++)
                        data.Datas.Add(csvReader[i]);

                    data.RowCount++;
                }

                currentRow++;
            }

            return data;
        }
    }
}