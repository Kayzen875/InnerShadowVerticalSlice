using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Decision/DecisionCameraFixedToTracking")]
public class DecisionCameraFixedToTracking : Decision
{
    public override bool Decide(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        if (!c.cameraFixed && c.cameraTracking) { return true; }
        else { return false; }

    }
}
