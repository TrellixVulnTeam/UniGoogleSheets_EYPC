using System;
using System.Threading.Tasks;
using Data;
using UniGS.Runtime.Protocol;
using UnityEngine;


namespace UniGS.Runtime
{
    public class UniGoogleSheetsMonoContext : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}