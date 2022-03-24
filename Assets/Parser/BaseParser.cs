using System;
using System.Collections.Generic;


public abstract class BaseParser : IBaseParser
    { 
        public abstract string[] TypeKeywords { get; } 
        private HashSet<string> _typeDeclarationsCache = null;
        public string GetRepresentativeType(string name)
        { 
            if (TypeKeywords.Length == 0) throw new Exception($"TypeDeclarations is empty");
            if (TypeKeywords.Length == 1 && name == TypeKeywords[0]) return TypeKeywords[0];
            else if (_typeDeclarationsCache == null)
            {
                _typeDeclarationsCache = new HashSet<string>();
                for (var i = 0; i < TypeKeywords.Length; i++) 
                    _typeDeclarationsCache.Add(TypeKeywords[i]); 
            } 
            if (_typeDeclarationsCache.Contains(name)) return TypeKeywords[0];
            else
            {
                throw new Exception("Cannot found representative type, please check TypeDeclarations. and TypeDeclarationsCache");
            }
        }
        public abstract Type Type { get; }
        public abstract object Read(string value);
        public abstract string Write(object value); 
    } 