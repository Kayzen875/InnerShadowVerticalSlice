using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxDecisionNormalToCaught")]
public class PaxDecisionNormalToCaught : Decision
{
    public override bool Decide(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        if (p.shadowForm && !p.crouching) { return true; }
        else { return false; }

    }
}