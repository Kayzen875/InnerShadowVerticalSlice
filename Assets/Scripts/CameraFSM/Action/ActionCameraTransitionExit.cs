using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Action/ActionCameraTransitionExit")]
public class ActionCameraTransitionExit : FSM.Action
{
    public AnimationCurve animationCurve;
    public override void Act(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        c.factorT = 0;
        c.factorTotal = 0;
    }
}
