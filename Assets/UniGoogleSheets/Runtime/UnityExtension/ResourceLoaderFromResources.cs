using System;
using UnityEngine;

namespace UniGS.Runtime
{
    public class ResourceLoaderFromResources : AbstractResourceLoader
    {
        public override string ReadFile(string path)
        {
            var filePath = GetFullPathWithoutExtension(path);
            var asset = Resources.Load<TextAsset>(filePath);
            if (asset == null)
                throw new Exception("File not found : " + filePath);
            if (asset != null) return asset.text;
            return null;
        }
    }
}