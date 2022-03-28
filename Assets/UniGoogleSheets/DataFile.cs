using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class BinaryCSVData
    {
        public BinaryCSVData(string @namespace, string @class, string csv)
        {
            this.@namespace = @namespace;
            this.@class = @class;
            this.csv = csv;
        } 
 
        public string @namespace;
        public string @class;
        public string csv;
    }
    public static class DataFile
    {
        private static readonly byte[] SIGNATURE =
        {
            (byte) 'U', (byte) 'N', (byte) 'I', 
            (byte) 'G', (byte) 'S'
        };

        [UnityEditor.MenuItem("Test/Save")]
         static void SaveTestMenu()
        {

            string csv = @$"a:int,b:int,c:int
1,2,3
2,3,4
3,4,5";
            WriteFile("Assets/","A.B", "C", csv);
        }

        
        
        [UnityEditor.MenuItem("Test/Load")]
         static void LoadTestMenu()
        {

            string csv = @$"a:int,b:int,c:int
1,2,3
2,3,4
3,4,5";
            ReadFile("Assets/","A.B", "C");
        }

        public static void WriteFile(string directoryPath, string @namespace, string @class, string csv)
        { 
            string savePath = Path.Combine(directoryPath, @namespace + "." + @class + ".bin"); 
            Stream stream = new FileStream(savePath, FileMode.OpenOrCreate);  
            using GZipStream compressionStream = new GZipStream(stream, CompressionMode.Compress);
            using var writer = new BinaryWriter(compressionStream); 
            byte[] namespaceBytes = System.Text.Encoding.UTF8.GetBytes(XOR(@namespace)); 
            writer.Write(@namespace.Length); // 4byte
            writer.Write(@namespaceBytes); // length 
            
            byte[] classBytes = System.Text.Encoding.UTF8.GetBytes(XOR(@class));
            writer.Write(@class.Length); // 4byte 
            writer.Write(classBytes); // length
            
            
            byte[] csvBytes = System.Text.Encoding.UTF8.GetBytes(XOR(@csv));
            writer.Write(csv.Length); // 4byte 
            writer.Write(csvBytes); // length
            writer.Close();  
        }

        public static BinaryCSVData ReadFile(string directoryPath, string @namespace, string @class)
        {       
        
            
            string loadPath = Path.Combine(directoryPath, @namespace + "." + @class + ".bin"); 
            Stream stream = new FileStream(loadPath, FileMode.Open);  
            using GZipStream decompressStream = new GZipStream(stream, CompressionMode.Decompress);
            using var reader = new BinaryReader(decompressStream); 
            var readNameSpace = XOR(ReadNextString(reader));
            Debug.Log(readNameSpace);
            var readClass = XOR(ReadNextString(reader));
            Debug.Log(readClass); 
            var csv = XOR(ReadNextString(reader));
            Debug.Log(csv);

            return new BinaryCSVData(readNameSpace, readClass, csv);
        }
        
 
        private static string XOR(string input, char[] keys = null)
        { 
            char[] key = keys;
            if (keys == null) key = "2a분의 -b, +- 루트b제곱 -4ac".GetHashCode().ToString().ToCharArray();
            char[] output = new char[input.Length]; 
            for(int i = 0; i < input.Length; i++) {
                output[i] = (char) (input[i] ^ key[i % key.Length]);
            } 
            return new string(output);
        }
        
        private static string ReadNextString(BinaryReader reader) {
            var length = reader.ReadInt32();
            var bytes = reader.ReadBytes(length); 
            return Encoding.UTF8.GetString(bytes);
        }
        
        
        
    }
}