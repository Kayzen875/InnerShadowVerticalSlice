using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Action/CameraFollowEnter")]
public class ActionCameraFollowEnter : FSM.Action
{
    public override void Act(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        if (c.goToFollow)
        {
            c.goToFollow = false;
            c.player.GetComponent<PaxFSMController>().UnFrozePax();
        }

    }
}
