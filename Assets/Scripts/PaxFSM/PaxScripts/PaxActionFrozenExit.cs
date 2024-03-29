using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxActionFrozenExit")]
public class PaxActionFrozenExit : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        p.frozeTimer = 0;
        p.UnFrozePax();

    }
}
