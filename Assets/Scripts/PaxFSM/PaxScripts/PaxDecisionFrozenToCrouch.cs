using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxDecisionFrozenToCrouch")]
public class PaxDecisionFrozenToCrouch : Decision
{
    public override bool Decide(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        if (!p.froze && p.crouching) { return true; }
        else { return false; }
    }
}
