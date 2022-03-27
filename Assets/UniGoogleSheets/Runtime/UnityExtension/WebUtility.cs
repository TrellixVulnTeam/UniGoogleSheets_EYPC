using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
 

public class WebUtility : IWebRequester
{  
    private bool isRequesting = false;
    public async Task<string> Get(string uri, string queryParams)
    { 
        while (isRequesting) Task.Yield();
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.timeout = 60; 
            isRequesting = true;
            var operation = webRequest.SendWebRequest(); 
            while (!operation.isDone) Task.Yield();
            if (webRequest.error == null)
            { 
                isRequesting = false;
                string responseText = webRequest.downloadHandler.text;
                return responseText;
            }
            else
            {
                isRequesting = false; 
                throw new Exception("[WebUtility Error]" + webRequest.error); 
            } 
        } 
    } 
    
    public async Task<string> Post(string uri, string queryParams)
    {
        throw new System.NotImplementedException();
    }
}