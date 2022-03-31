using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UniGS.Runtime.Protocol
{
    [System.Serializable]
    public abstract class ParameterBase
    { 
        public abstract string action { get; }
        public Dictionary<string, object> kv;

        public string ToQueryParameter()
        {
            string query = $"?action={action}";
            foreach (var model in kv)
                query += $"&{model.Key}={model.Value}";
            return query;
        }
    }

 
}