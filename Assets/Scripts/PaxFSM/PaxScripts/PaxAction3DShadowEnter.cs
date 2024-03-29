using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxAction3DShadowEnter")]
public class PaxAction3DShadowEnter : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        //if (!p.crouching && !p.shadowForm)
        /*p.paxAnimator.gameObject.SetActive(false);
        p.shadow3DAnimator.gameObject.SetActive(true);
        p.paxToShadow3D.SetActive(true);
        p.shadow3DToPax.SetActive(false);
        p.animatorC = p.shadow3DAnimator;*/
        p.actualSpeed += 1;
        p.characterControllerC.height += 0.12f;
        p.characterControllerC.center += new Vector3(0f, 0.06f, 0f);
        p.jumpSpeed += 1.0f;
        //p.shadowForm = true;

        //p.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);

        //paxSprintC.sprintSpeed += 1;
    }
}
