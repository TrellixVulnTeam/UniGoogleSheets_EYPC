using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Data
{
    public static class DataFile
    {
        private static readonly byte[] SIGNATURE =
        {
            (byte) 'U', (byte) 'N', (byte) 'I', 
            (byte) 'G', (byte) 'S'
        };


        [MenuItem("MyMenu/Do Something")]
        public static void SaveTest(string @namespace, string @class, string csv)
        {
            Stream stream = new FileStream($"{@namespace}.{@class}.bin", FileMode.OpenOrCreate);  
            using GZipStream compressionStream = new GZipStream(stream, CompressionMode.Compress);
            using var writer = new BinaryWriter(compressionStream); 
            writer.Write(@namespace.Length); // 4byte
            writer.Write(@namespace); // length
            writer.Write(@class.Length); // 4byte
            writer.Write(@class); // length 
            writer.Write(csv.Length); // 4byte
            writer.Write(csv); // length 
            writer.Close();  
        }

        public static void LoadTest(string @namespace, string @class)
        { 
            Stream stream = new FileStream($"{@namespace}.{@class}.bin", FileMode.Open);  
            using GZipStream decompressStream = new GZipStream(stream, CompressionMode.Decompress);
            using var reader = new BinaryReader(decompressStream); 
            var readNamepsace = ReadNextString(reader);
            Debug.Log(readNamepsace);
            var readClass = ReadNextString(reader); 
            Debug.Log(readClass); 
            var csv = ReadNextString(reader);
   
            Debug.Log(csv); 
        }
        
        private static string ReadNextString(BinaryReader reader) {
            var length = reader.ReadInt32();
            var bytes = reader.ReadBytes(length);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}