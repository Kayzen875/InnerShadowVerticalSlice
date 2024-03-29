using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxActionCaughtUpdate")]
public class PaxActionCaughtUpdate : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

    }
}
