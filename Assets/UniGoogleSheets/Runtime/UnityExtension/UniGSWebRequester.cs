using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace UniGS.Runtime
{
    public class UniGSWebRequester : IWebRequester
    {
        private bool isRequesting = false;

        public Task<string> Get(string uri, string queryParams)
        {
            while (isRequesting) Task.Yield();
            using (UnityWebRequest request = UnityWebRequest.Get(uri))
            {
                request.timeout = 40;
                request.SetRequestHeader("Content-Type", "application/json");
                isRequesting = true;
                var operation = request.SendWebRequest();
                while (!operation.isDone) Task.Yield();
                if (request.error == null)
                {
                    isRequesting = false;
                    string responseText = request.downloadHandler.text;
                    return Task.FromResult(responseText);
                }
                else
                {
                    isRequesting = false;
                    throw new Exception("[WebUtility Error]" + request.error);
                }
            }
        }

        public Task<string> Post(string uri, string queryParams)
        {
            throw new NotImplementedException();
        }


        public Task<string> Post(string uri, string queryParams, string body)
        {
            while (isRequesting) Task.Yield();
            using var request = new UnityWebRequest(uri, "POST");

            byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);

            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            request.timeout = 40;
            var operation = request.SendWebRequest();
            while (!operation.isDone) Task.Yield();
            if (request.error == null)
            {
                isRequesting = false;
                string responseText = request.downloadHandler.text;
                return Task.FromResult(responseText);
            }
            else
            {
                isRequesting = false;
                throw new Exception("[WebUtility Error]" + request.error);
            }
        }
    }
}