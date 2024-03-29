using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
