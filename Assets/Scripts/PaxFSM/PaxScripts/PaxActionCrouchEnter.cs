using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxActionCrouchEnter")]
public class PaxActionCrouchEnter : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        //if (!p.crouching && !p.shadowForm)
        p.RegulateSpeed();
        p.animatorC.SetBool("crouch", true);
        p.animatorC.SetBool("running", false);
        p.actualSpeed -= 1;
        p.characterControllerC.center = new Vector3(0, p.characterControllerC.center.y - 0.1f, 0);
        p.characterControllerC.height -= 0.2f;
    }
}
