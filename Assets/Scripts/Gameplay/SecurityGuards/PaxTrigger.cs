using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaxTrigger : MonoBehaviour
{
    public bool paxTrigger;

    // Start is called before the first frame update
    void Start()
    {
        paxTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("PAX");
        
        paxTrigger = true;
       
    }

    public bool paxTriggerActivated()
    {
        return paxTrigger;
    }
}
