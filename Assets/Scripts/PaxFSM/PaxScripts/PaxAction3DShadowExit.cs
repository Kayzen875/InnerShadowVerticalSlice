using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxAction3DShadowExit")]
public class PaxAction3DShadowExit : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        p.shadow3DAnimator.gameObject.SetActive(false);
        p.paxAnimator.gameObject.SetActive(true);
        p.paxToShadow3D.SetActive(false);
        p.shadow3DToPax.SetActive(true);
        p.animatorC = p.paxAnimator;
        p.characterControllerC.height -= 0.12f;
        p.characterControllerC.center -= new Vector3(0f, 0.06f, 0f);
        p.actualSpeed = p.normalSpeed;
        p.jumpSpeed = p.normalJumpSpeed;
        SoundManager.PlaySound(SoundManager.Sound.ToPaxTransfom, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxAction);

        //p.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);

        //paxSprintC.sprintSpeed -= 1;
    }
}
