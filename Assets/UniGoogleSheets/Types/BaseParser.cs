using System;
using System.Collections.Generic;


public abstract class BaseParser : IBaseType
    {
        public BaseParser()
        {
            
        }
        public abstract string[] TypeDeclarations { get; } 
        private HashSet<string> TypeDeclarationsCache = null;
        public string GetRepresentativeType(string name)
        { 
            if (TypeDeclarations.Length == 0) throw new Exception($"TypeDeclarations is empty");
            if (TypeDeclarations.Length == 1 && name == TypeDeclarations[0]) return TypeDeclarations[0];
            else if (TypeDeclarationsCache == null)
            {
                TypeDeclarationsCache = new HashSet<string>();
                for (var i = 0; i < TypeDeclarations.Length; i++) 
                    TypeDeclarationsCache.Add(TypeDeclarations[i]); 
            } 
            if (TypeDeclarationsCache.Contains(name)) return TypeDeclarations[0];
            else
            {
                throw new Exception("Cannot found representative type, please check TypeDeclarations. and TypeDeclarationsCache");
            }
        }
        public abstract Type Type { get; }
        public abstract object Read(string value);
        public abstract string Write(object value); 
    } 