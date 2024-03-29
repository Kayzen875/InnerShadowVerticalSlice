using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxDecisionFrozenTo3DShadow")]
public class PaxDecisionFrozenTo3DShadow : Decision
{
    public override bool Decide(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        if (!p.froze && p.shadowForm) { return true; }
        else { return false; }
    }
}

