using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UniGS.Runtime.Protocol
{


    [System.Serializable]
    public class ParameterBase
    {
        public string method;
        public string action;
        public Dictionary<string, object> kv;
        public string ToQueryParameter()
        {
            string query = $"?method={method}&action={action}";
            foreach(var model in kv) 
                query += $"&{model.Key}={model.Value}"; 
            return query;
        }
    }

    public class SpreadSheetReqParams : ParameterBase
    {
        public SpreadSheetReqParams(string spreadSheetId)
        {
            
        }
    }
 
    
    
    [System.Serializable]
    public class RequestBase
    {
        public ParameterBase parameter;
        [NonSerialized]
        public string Uri;    
        public async Task<T> Get<T>() where T : ReponseBase
        {
            try
            {
                var response = await UniGoogleSheets.WebRequester.Get(Uri, parameter.ToQueryParameter());
                var jsonNode = SimpleJSON.JSON.Parse(response); 
                var result = JsonUtility.FromJson<T>(jsonNode.ToString());
                return result;
            }
            catch (Exception e)
            {
#if UNITY_STANDALONE
                Debug.LogError(e);
#endif
            }

            return null;
        }
    }

    public class ReponseBase
    {
    }
}