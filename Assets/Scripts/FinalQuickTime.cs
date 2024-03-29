using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalQuickTime : MonoBehaviour
{
    public Transform startPointCamera;
    public Transform finalPointCamera;
    public Camera mainCamera;
    public Camera finalCamera;
    public float interpolationSpeed;

    public float timeFirstAnimation;
    public float timeSecondAnimation;
    public bool finalAnimation;
    public float timeFinalTransition;

    public Transform pax;
    public Transform startPoint;
    public Transform finalPoint;
    public float interpolationSpeedPax;
    public Animator _anim;

    private bool startEvent;
    private bool started;
    private bool secondAnimation;
    private bool endEvent;

    void Update()
    {
        if(startEvent)
        {
            if(!started)
            {
                started = true;
                _anim.SetBool("walk", true);
            }
            else if(timeFirstAnimation > 0)
            {
                timeFirstAnimation -= Time.deltaTime;
                pax.position = Vector3.MoveTowards(pax.position, finalPoint.position, interpolationSpeedPax);
            }
            else if(!secondAnimation)
            {
                secondAnimation = true;
                pax.rotation = new Quaternion(0, 180, 0, 0);
                _anim.SetBool("walk", false);
                _anim.SetBool("openDumbwaiter", true);
                _anim.SetBool("openDumbwaiter", false);
            }
            else if(timeSecondAnimation > 0)
            {
                timeSecondAnimation -= Time.deltaTime;
            }
            else if(timeFinalTransition > 0)
            {
                timeFinalTransition -= Time.deltaTime;
                finalCamera.transform.position = Vector3.Lerp(finalCamera.transform.position, finalPointCamera.position, interpolationSpeed);
            }
            else if(!finalAnimation)
            {
                finalAnimation = true;
                endEvent = true;
            }

            if(endEvent)
            {
                LevelManager.Instance.EndGameScene();
                endEvent = false;
            }
        }
    }

    public void FinalQuickTimeEvent()
    {
        if(!endEvent)
        {
            endEvent = true;
            pax.GetComponent<PaxFSMController>().FrozePax();
            finalCamera.enabled = true;
            mainCamera.enabled = false;      
            //startEvent = true;
            LevelManager.Instance.EndGameScene();
        }
    }
}
