using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxDecisionFrozenToFrozen")]
public class PaxFrozenNormalToFrozen : Decision
{
    public override bool Decide(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        if (p.froze && p.shadowForm) { return true; }
        else { return false; }

    }
}
