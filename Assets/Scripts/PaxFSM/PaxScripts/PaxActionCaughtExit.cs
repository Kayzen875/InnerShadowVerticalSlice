using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxActionCaughtExit")]
public class PaxActionCaughtExit : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

    }
}
