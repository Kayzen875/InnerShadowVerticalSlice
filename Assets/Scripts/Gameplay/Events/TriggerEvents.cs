using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    private bool triggered;
    private bool oneTimeOnly;
    public int eventIndex; 

    private void Triggered()
    {
        if(!triggered)
        {
            GetComponent<GenericEvents>().TriggerEvents(eventIndex);
            triggered = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!oneTimeOnly)
        {   
            Triggered();
            oneTimeOnly = true;
        }      
    }
}
