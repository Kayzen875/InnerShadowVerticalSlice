using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLever : SwitchesPax
{

    public override void SwitchesAction()
    {
        if (!activated && cooldown <= 0)
        {
            gameObject.GetComponent<GenericEvents>().TriggerEvents(0);
            activated = true;
            cooldown = 1.0f;
        }
        else if(activated && cooldown <= 0)
        {
            gameObject.GetComponent<GenericEvents>().TriggerEvents(1);
            activated = false;
            cooldown = 1.0f;
        }
    }
}
