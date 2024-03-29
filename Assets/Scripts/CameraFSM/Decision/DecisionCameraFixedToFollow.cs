using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Decision/DecisionCameraFixedToFollow")]
public class DecisionCameraFixedToFollow : Decision
{
    public override bool Decide(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        if (!c.cameraFixed && c.cameraFollow) { return true; }
        else { return false; }

    }
}
