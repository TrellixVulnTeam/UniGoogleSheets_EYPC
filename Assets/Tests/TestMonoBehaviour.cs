using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class TestMonoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UniGoogleSheets test = new UniGoogleSheets();
        var parser = test.GetParser("int");
        Debug.Log(parser.Read("1000"));
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
