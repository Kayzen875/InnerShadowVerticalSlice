using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaxSprint : MonoBehaviour
{
    [SerializeField]
    public float sprintSpeed;

    float sprintDuration;

    [SerializeField]
    float actualSprintDuration;

    [SerializeField]
    float actualSprintCoolDown;

    float sprintCoolDown;

    public PlayerInput playerInput;
    public bool sprint;

    PaxFSMController paxControllerC;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        playerInput.actions["Sprint"].performed += Sprinting;
        //GetComponent<InputSystemKeyboard>().Sprint += Sprinting;
    }

    void OnDisable()
    {
        playerInput.actions["Sprint"].performed -= Sprinting;
        //GetComponent<InputSystemKeyboard>().Sprint -= Sprinting;

    }
    void Start()
    {
        paxControllerC = GetComponent<PaxFSMController>();
        sprintDuration = actualSprintDuration;
        sprintCoolDown = actualSprintCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
            if(sprint)
            {
                sprintDuration -= Time.deltaTime;
            }

            if(sprintDuration <= 0)
            {
                if(!paxControllerC.crouching)
                {
                paxControllerC.RegulateSpeed();
                }
                
                sprintCoolDown -= Time.deltaTime;
            }

            if(sprintCoolDown <= 0)
            {
                sprintDuration = actualSprintDuration;
                sprint = false;
                sprintCoolDown = actualSprintCoolDown;
            }
    }

    private void Sprinting(InputAction.CallbackContext context)
    {
        if(!sprint && !paxControllerC.crouching && paxControllerC.interactiveBoxC == null)
        {
            paxControllerC.IncreaseSpeed(sprintSpeed);
            sprint = true;
        }
    }
}
