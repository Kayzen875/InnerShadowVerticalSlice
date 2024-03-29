using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxDecision3DShadowToFrozen")]
public class PaxDecision3DShadowToFrozen : Decision
{
    public override bool Decide(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        if (p.froze && !p.shadowForm) { return true; }
        else { return false; }
    }
}
