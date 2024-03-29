using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/DecisionNormalToCrouch")]
public class PaxDecisionNormalToCrouch : Decision
{
    public override bool Decide(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        if (p.crouching && !p.shadowForm) { return true; }
        else { return false; }

    }
}
