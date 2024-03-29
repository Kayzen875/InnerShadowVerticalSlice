using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxActionCaughtEnter")]
public class PaxActionCaughtEnter : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        //LevelManager.Instance.loadAsyncScene(sceneToLoad);

    }
}
