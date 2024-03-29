using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Pax/PaxActionFrozenEnter")]
public class PaxActionFrozenEnter : FSM.Action
{
    public override void Act(Controller controller)
    {
        PaxFSMController p = (PaxFSMController)controller;

        if (p.shadowForm && !p.shadow2D && p.froze)
        {
            SoundManager.PlaySound(SoundManager.Sound.Shadow3DTransform, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxAction);
            p.paxAnimator.gameObject.SetActive(false);
            p.shadow3DAnimator.gameObject.SetActive(true);
            p.paxToShadow3D.SetActive(true);
            p.shadow3DToPax.SetActive(false);
            p.animatorC = p.shadow3DAnimator;
            p.frozeTimer = 0;
            p.FrozePax();
        }

        /*if(!p.shadowForm && !p.shadow2D && p.unTransform && p.froze)
        {

        }*/

    }
}
