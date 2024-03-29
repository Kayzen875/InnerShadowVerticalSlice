using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuards : MonoBehaviour
{
    Animator animatorDobbC;
    Animator animatorRottC;

    public Transform rott;
    public Transform dobb;

    public Transform pax;

    public bool paxHasPassed;

    public float distance;

    public float movement;

    public Transform woodLogTrigger1;

    WoodTrigger woodTrigger1C;

    public Transform woodLogTrigger2;

    WoodTrigger woodTrigger2C;

    public Transform paxTrigger;

    PaxTrigger paxTriggerC;

    public bool woodLogTrigger;

    public bool paxTriggerHasBeenActivated;


    // Start is called before the first frame update
    void Start()
    {
        paxHasPassed = false;
        animatorRottC = rott.gameObject.GetComponent<Animator>();
        animatorDobbC = dobb.gameObject.GetComponent<Animator>();
        woodTrigger1C = woodLogTrigger1.gameObject.GetComponent<WoodTrigger>();
        woodTrigger2C = woodLogTrigger2.gameObject.GetComponent<WoodTrigger>();
        paxTriggerC = paxTrigger.gameObject.GetComponent<PaxTrigger>();
        paxTriggerHasBeenActivated = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (paxTriggerC.paxTriggerActivated() && !paxTriggerHasBeenActivated)
        {
            paxTriggerHasBeenActivated = true;
            woodLogTrigger2.gameObject.SetActive(false);
            woodLogTrigger = false;
            Debug.Log(woodLogTrigger);
            woodTrigger2C.woodLogFalse();
            
        }

        else if (woodTrigger2C.woodLogActivated() || woodTrigger1C.woodLogActivated())
        {
            woodLogTrigger = true;
        }
        
        if (!woodLogTrigger)
        {
            if (Mathf.Abs(transform.position.x - pax.position.x) > distance && paxHasPassed)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + movement, transform.position.y, transform.position.z), movement);
                animatorDobbC.SetBool("PaxWalk", true);
                animatorRottC.SetBool("PaxWalk", true);
                SoundManager.PlaySound(SoundManager.Sound.DogsSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Dogs);
            }
            else
            {
                animatorDobbC.SetBool("PaxWalk", false);
                animatorRottC.SetBool("PaxWalk", false);
                SoundManager.StopSound(SoundManager.Sound.DogsSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Dogs);
            }
        }
        else
        {
            animatorDobbC.SetBool("PaxWalk", false);
            animatorRottC.SetBool("PaxWalk", false);
            SoundManager.StopSound(SoundManager.Sound.DogsSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Dogs);
        }

    }

    void OnTriggerExit(Collider other)
    {
        paxHasPassed = true;
        GetComponent<BoxCollider>().isTrigger = false;
    }

    




}
