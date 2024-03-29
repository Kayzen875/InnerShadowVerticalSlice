using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Decision/DecisionCameraTransitionToFollow")]
public class DecisionCameraTransitionToFollow : Decision
{
    public override bool Decide(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        if (!c.cameraTransition && c.goToFollow) { return true; }
        else { return false; }

    }
}
