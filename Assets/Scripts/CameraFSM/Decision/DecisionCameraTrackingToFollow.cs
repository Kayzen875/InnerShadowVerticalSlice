using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Decision/DecisionCameraTrackingToFollow")]
public class DecisionCameraTrackingToFollow : Decision
{
    public override bool Decide(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        if (!c.cameraTracking && c.cameraFollow) { return true; }
        else { return false; }
    }
}
