using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCinematic : MonoBehaviour
{
    private Animator _anim;
    public Animator _animPax;

    public Transform pax;
    public bool cinematic;
    public bool chaseAnimation;
    public bool shakeAnimation;
    public bool endCinematic;
    public float movementSpeed;
    public float stopDistance;
    public float shakeTimer;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(cinematic)
        {
            if(Vector3.Distance(pax.transform.position, transform.position) > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, pax.position, movementSpeed);

                if(!chaseAnimation)
                {
                    chaseAnimation = true;
                    pax.GetComponent<PaxFSMController>().FrozePax();
                    _anim.SetBool("walking", true);
                }
            }
            else
            {
                if(!shakeAnimation)
                {
                    shakeAnimation = true;
                    _anim.SetBool("walking", false);
                    _anim.SetBool("shake", true);
                    _animPax.SetBool("shake", true);
                }
                else if(shakeTimer > 0)
                {
                    shakeTimer -= Time.deltaTime;
                }
                else if(!endCinematic)
                {
                    endCinematic = true;
                    cinematic = false;
                    _animPax.SetBool("shake", false);
                    _anim.SetBool("shake", false);
                    GetComponent<GenericEvents>().TriggerEvents(0);
                    pax.GetComponent<PaxFSMController>().UnFrozePax();
                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        cinematic = true;
        GetComponent<Collider>().enabled = false;
    }
}
