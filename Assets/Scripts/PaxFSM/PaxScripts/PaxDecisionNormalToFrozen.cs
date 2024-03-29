using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxDecisionNormalToFrozen")]
public class PaxDecisionNormalToFrozen : Decision
{
    public override bool Decide(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        if (p.froze) { return true; }
        else { return false; }

    }
}
