using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Action/CameraTrackingEnter")]
public class ActionCameraTrackingEnter : FSM.Action
{
    public override void Act(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

    }
}
