using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicOpenDoors : MonoBehaviour
{
    public Animator _paxAnim;
    public Animator _doorAnim;

    public float animationTime;
    public bool animationStarted;
    public bool animationEnded;

    public Transform pax;
    public Transform lastPoint;

    private void Update()
    {
        if (!animationEnded)
        {
            if (!animationStarted)
            {
                animationStarted = true;
                pax.GetComponent<PaxFSMController>().FrozePax();
                _doorAnim.SetBool("door", true);
                _paxAnim.SetBool("door", true);
                SoundManager.PlaySound(SoundManager.Sound.DoorSqueak, SoundManager.SoundType.Ambient, SoundManager.AudioSourcesType.Door);
            }
            else if (animationTime > 0)
            {
                animationTime -= Time.deltaTime;
            }
            else if (animationTime <= 0)
            {
                pax.GetComponent<PaxFSMController>().SetPlayerPosition(lastPoint.position);
                _doorAnim.SetBool("door", false);
                _paxAnim.SetBool("door", false);
                animationEnded = true;
                Invoke("UnFroze", 1);
            }
        }
    }

    public void UnFroze()
    {
        pax.GetComponent<PaxFSMController>().UnFrozePax();
    }
}
