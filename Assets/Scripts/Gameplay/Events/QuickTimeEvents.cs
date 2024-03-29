using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuickTimeEvents : MonoBehaviour
{
    public Animator _anim;
    public GameObject window;
    public Transform lastPoint;
    public Transform raycastStart;
    private RaycastHit hit;
    public LayerMask conditionLayer;
    public LayerMask newConditionLayer;
    public float maxDistance;
    public bool onRange;
    public bool eventStarted;
    public bool finalEvent;
    public int counter;
    public int counterMax;
    public float interpolation;

    void OnEnable()
    {
        GetComponent<PlayerInput>().actions["QuickTimeEvent"].performed += StartEvent;
    }

    void OnDisable()
    {
        GetComponent<PlayerInput>().actions["QuickTimeEvent"].performed -= StartEvent;
    }

    void Start()
    {
        eventStarted = false;
        finalEvent = false;
    }

    private void Update()
    {
        if(Physics.Raycast(raycastStart.position, transform.forward, out hit, maxDistance, conditionLayer))
        {
            onRange = true;
        }
        else
        {
            onRange = false;
            counter = 0;
            _anim.SetBool("QuickTime", false);
            //_anim.SetBool("FinalQuickTime", false);
        }

        if(eventStarted)
        {
            window.transform.position = Vector3.Lerp(window.transform.position, lastPoint.position, interpolation);
        }
    }

    private void StartEvent(InputAction.CallbackContext context)
    {
        Debug.Log("funciona?");
        if(onRange)
        {
            
            if(counter > 0 && !eventStarted)
            {
                if(finalEvent)
                {
                    _anim.SetBool("QuickTime", true);
                }
                else
                {
                    _anim.SetBool("QuickTime", true);
                }
            }

            counter++;

            if(counter == counterMax && finalEvent)
            {
                FinalEvent();
            }
            else if(counter == counterMax)
            {
                SuccedEvent();
            }
        }
    }

    private void SuccedEvent()
    {
        if(onRange)
        {
            eventStarted = true;
            finalEvent = true;
            _anim.SetTrigger("SuccedEvent");
            conditionLayer = newConditionLayer;
            _anim.SetBool("QuickTime", false);
        }
    }

    private void FinalEvent()
    {
        hit.collider.GetComponent<FinalQuickTime>().FinalQuickTimeEvent();
    }
}
