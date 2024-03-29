using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Decision/DecisionCameraFixedToTransition")]
public class DecisionCameraFixedToTransition : Decision
{
    public override bool Decide(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        if (!c.cameraFixed && c.cameraTransition) { return true; }
        else { return false; }

    }
}
