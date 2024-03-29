using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/ActionFrozenUpdate")]
public class PaxActionFrozenUpdate : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        //p.FrozePax();
        if (p.shadowForm)
        {
            p.frozeTimer += Time.deltaTime;
            if(p.frozeTimer >= 1.3f)
            {
                p.UnFrozePax();
            }
        }

        if(p.unTransform)
        {
            p.frozeTimer += Time.deltaTime;
            if (p.frozeTimer >= 1.5f)
            {
                p.UnFrozePax();
                p.unTransform = false;
            }
        }
        

    }
}
