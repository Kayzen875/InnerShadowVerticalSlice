using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldBox : MonoBehaviour
{
    PaxFSMController paxControllerC;
    //InputSystemKeyboard inputSystemC;
    PlayerInput playerInput;

    PaxSprint paxSprintC;
    

    public bool movingBox;
    public float pushForce;
    bool reduceSpeed;

    public float boxSize;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        paxSprintC = GetComponent<PaxSprint>();
    }

    void OnEnable()
    {
        playerInput.actions["Pull&Push"].performed += Holding;
        playerInput.actions["Pull&Push"].canceled += Holding;
        //GetComponent<InputSystemKeyboard>().Hold += Holding;
    }

    void OnDisable()
    {
        playerInput.actions["Pull&Push"].performed -= Holding;
        playerInput.actions["Pull&Push"].canceled -= Holding;
        //GetComponent<InputSystemKeyboard>().Hold -= Holding;

    }
    void Start()
    {
        paxControllerC = GetComponent<PaxFSMController>();
        //inputSystemC = GetComponent<InputSystemKeyboard>();
        paxControllerC.interactiveBoxC = null;
        movingBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(paxControllerC.interactiveBoxC != null)
        {
            BoxSideController();
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(LayerMask.LayerToName(hit.gameObject.layer).Equals("MovableBox"))
        {
            if(hit.gameObject.GetComponent<InteractiveBox>().bigBox)
            {
                if(paxControllerC.shadowForm && movingBox && transform.position.y <= hit.transform.position.y && !paxSprintC.sprint && !paxControllerC.crouching && !paxControllerC.jumping)
                {
                    GetBoxReference(hit.gameObject);
                }
            }
            else
            {
                if(movingBox && transform.position.y <= hit.transform.position.y && !paxSprintC.sprint && !paxControllerC.crouching && !paxControllerC.jumping)
                {
                    GetBoxReference(hit.gameObject);
                }   
            }
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("MovableBox"))
        {
            reduceSpeed = false;
            paxControllerC.NotGrabbed();
            paxControllerC.RegulateSpeed();
            EliminateBoxReference();
        }
    }*/

    void Holding(InputAction.CallbackContext context)
    {
        
        if (movingBox)
        {
            movingBox = false;
            reduceSpeed = false;
            paxControllerC.NotGrabbed();
            paxControllerC.RegulateSpeed();
            EliminateBoxReference();
        }
        else
        {
            movingBox = true;
        }
    }
    void BoxSideController()
    {
        //CUADRANTE EN CRUZ DESDE D�NDE SE PUEDE EMPUJAR LA CAJA//
        if (movingBox)
        {
            paxControllerC.interactiveBoxC.OnGrabbed();
            boxSize = paxControllerC.interactiveBoxC.transform.localScale.x / 2;
            if (!reduceSpeed)
            {
                if(paxControllerC.shadowForm)
                {
                    paxControllerC.ReduceSpeed(2.5f);
                }
                else
                {
                    paxControllerC.ReduceSpeed(1.5f);
                }
                reduceSpeed = true;
            }

            if (transform.position.x < paxControllerC.interactiveBoxC.transform.position.x && transform.position.z < paxControllerC.interactiveBoxC.transform.position.z + boxSize && transform.position.z > paxControllerC.interactiveBoxC.transform.position.z - boxSize) //Aqu� quiz�s es coger el game object
            {
                if(paxControllerC.movementDirection.x == -1)
                {
                    if(!paxControllerC.shadowForm)
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveHorizontal(-pushForce + 0.01f);
                    }
                    else
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveHorizontal(-pushForce);
                    }
                    Debug.Log("Horizontal1 negativo");
                }
                else if (paxControllerC.movementDirection.x == 1)
                {
                    if (!paxControllerC.shadowForm)
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveHorizontal(pushForce - 0.3f);
                    }
                    else
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveHorizontal(pushForce);
                    }
                    Debug.Log("Horizontal1 positivo");
                }
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);  //Bloquear el rotacion del player seg�n la direcci�n que empuja a la caja
                paxControllerC.GetComponent<PaxFSMController>().OnGrabbed(0);
            }
            else if (transform.position.x > paxControllerC.interactiveBoxC.transform.position.x && transform.position.z < paxControllerC.interactiveBoxC.transform.position.z + boxSize && transform.position.z > paxControllerC.interactiveBoxC.transform.position.z - boxSize)
            {
                if (paxControllerC.movementDirection.x == -1)
                {
                    if (!paxControllerC.shadowForm)
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveHorizontal(-pushForce + 0.3f);
                    }
                    else
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveHorizontal(-pushForce);
                    }
                    Debug.Log("Horizontal2 negativo");
                }
                else if (paxControllerC.movementDirection.x == 1)
                {
                    if(!paxControllerC.shadowForm)
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveHorizontal(pushForce + 0.12f);
                    }
                    else
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveHorizontal(pushForce);
                    }
                    Debug.Log("Horizontal2 positivo");
                }
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z); //Bloquear el rotacion del player seg�n la direcci�n que empuja a la caja
                paxControllerC.GetComponent<PaxFSMController>().OnGrabbed(0);
            }

            if (transform.position.z < paxControllerC.interactiveBoxC.transform.position.z && transform.position.x < paxControllerC.interactiveBoxC.transform.position.x + boxSize && transform.position.x > paxControllerC.interactiveBoxC.transform.position.x - boxSize)
            {
                if (paxControllerC.movementDirection.z == -1)
                {
                    if(!paxControllerC.shadowForm)
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveVertical(-pushForce + 0.01f);
                    }
                    else
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveVertical(-pushForce);
                    }
                    Debug.Log("Vertical1 negativo");
                }
                else if (paxControllerC.movementDirection.z == 1)
                {
                    if(!paxControllerC.shadowForm)
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveVertical(pushForce - 0.3f);
                    }
                    else
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveVertical(pushForce);
                    }
                }
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z); //Bloquear el rotacion del player seg�n la direcci�n que empuja a la caja
                paxControllerC.GetComponent<PaxFSMController>().OnGrabbed(1);
            }
            else if (transform.position.z > paxControllerC.interactiveBoxC.transform.position.z && transform.position.x < paxControllerC.interactiveBoxC.transform.position.x + boxSize && transform.position.x > paxControllerC.interactiveBoxC.transform.position.x - boxSize)
            {

                if (paxControllerC.movementDirection.z == -1)
                {
                    if(!paxControllerC.shadowForm)
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveVertical(-pushForce + 0.3f);
                    }
                    else
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveVertical(-pushForce);
                    }
                    Debug.Log("Vertical2 negativo");
                }
                else if (paxControllerC.movementDirection.z == 1)
                {
                    if(!paxControllerC.shadowForm)
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveVertical(pushForce + 0.12f);
                    }
                    else
                    {
                        paxControllerC.interactiveBoxC.gameObject.GetComponent<InteractiveBox>().MoveVertical(pushForce);
                    }
                }
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, -180, transform.eulerAngles.z); //Bloquear el rotacion del player seg�n la direcci�n que empuja a la caja
                paxControllerC.GetComponent<PaxFSMController>().OnGrabbed(1);
            }
        }
        else
        {
            paxControllerC.interactiveBoxC.NotGrabbed();
        }
    }

    void GetBoxReference(GameObject box)
    {
        paxControllerC.interactiveBoxC = box.GetComponent<InteractiveBox>();
        paxControllerC.interactiveBoxC.GetPlayerReference(gameObject);
    }

    void EliminateBoxReference()
    {
        paxControllerC.interactiveBoxC = null;
    }
}
