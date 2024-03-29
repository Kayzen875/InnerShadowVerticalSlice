using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodTrigger : MonoBehaviour
{

    public bool woodLog;

    // Start is called before the first frame update
    void Start()
    {
        woodLog = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        woodLog = true;     
       
    }

    public bool woodLogActivated()
    {
        return woodLog;
    }

    public void woodLogFalse()
    {
        woodLog = false;
    }
}
