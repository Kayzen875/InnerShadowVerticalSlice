using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxActionCrouchExit")]
public class PaxActionCrouchExit : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        p.animatorC.SetBool("crouch", false);
        p.animatorC.SetBool("crouchMove", false);
        p.animatorC.SetBool("running", false);
        p.characterControllerC.center = new Vector3(0, p.characterControllerC.center.y + 0.1f, 0);
        p.characterControllerC.height += 0.2f;
        p.RegulateSpeed();

        //if (p.crouching && !p.shadowForm)
    }
}
