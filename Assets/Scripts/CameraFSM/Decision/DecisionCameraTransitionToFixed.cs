using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Decision/DecisionCameraTransitionToFixed")]
public class DecisionCameraTransitionToFixed : Decision
{
    public override bool Decide(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        if (!c.cameraTransition && c.cameraFixed && !c.goToFollow) { return true; }
        else { return false; }

    }
}
