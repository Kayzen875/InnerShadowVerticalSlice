using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Decision/DecisionCameraTrackingToTransition")]
public class DecisionCameraTrackingToTransition : Decision
{
    public override bool Decide(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        if (!c.cameraTracking && c.cameraTransition) { return true; }
        else { return false; }
    }
}
