using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using UnityEngine.InputSystem;

public class PaxFSMController : Controller
{
    [SerializeField]
    public float actualSpeed;

    public float normalSpeed;

    [SerializeField]
    private float rotationSpeed;

    public float gravity;
    Vector3 moveVector;
    public float jumpSpeed;
    public float normalJumpSpeed;
    public float ySpeed;
    public bool isgrounded;
    bool paxRespawn;


    //public InputSystemKeyboard inputSystemC;

    public Animator paxAnimator;
    public Animator shadow3DAnimator;

    public Animator animatorC;

    public Rigidbody m_rigidbodyC;

    public bool crouching;
    public bool jumping;

    public bool firstGrounded = false;
    public bool falling = false;

    public bool unTransform;
    public bool shadowForm;
    public bool shadow2D;
    public bool leaveWall;

    public bool boxGrabbedY;
    public bool boxGrabbedX;

    public bool froze = false;

    public float frozeTimer;
    public bool frozeTimerRunning;

    public InteractiveBox interactiveBoxC;

    public CharacterController characterControllerC;

    public Vector3 movementDirection;

    public Transform respawnPoint;

    //Player Input
    public PlayerInput playerInput;
    public Vector2 movement;

    //Particles
    [Header("Particles")]
    public GameObject paxToShadow3D;
    public GameObject shadow3DToPax;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        playerInput.actions["Movement"].performed += OnMovement;
        playerInput.actions["Movement"].canceled += OnMovement;
        playerInput.actions["Movement"].canceled += OnMovementEnd;
        playerInput.actions["Jump"].performed += OnJump;
        playerInput.actions["Jump"].canceled += OnJumpEnd;
        playerInput.actions["Crouch"].performed += CrouchPax;
    }

    void OnDisable()
    {
        playerInput.actions["Movement"].performed -= OnMovement;
        playerInput.actions["Movement"].canceled -= OnMovement;
        playerInput.actions["Movement"].canceled -= OnMovementEnd;
        playerInput.actions["Jump"].performed -= OnJump;
        playerInput.actions["Jump"].canceled -= OnJumpEnd;
        playerInput.actions["Crouch"].performed -= CrouchPax;
    }
    
    // Start is called before the first frame update
    override public void Start()
    {
        //inputSystemC = GetComponent<InputSystemKeyboard>();
        animatorC = paxAnimator;
        m_rigidbodyC = GetComponent<Rigidbody>();
        characterControllerC = GetComponent<CharacterController>();
        characterControllerC.detectCollisions = true;
        crouching = false;
        jumping = false;
        frozeTimer = 0;

        normalSpeed = actualSpeed;
        normalJumpSpeed = jumpSpeed;

        base.Start();
    }

    // Update is called once per frame
    override public void Update()
    {
       base.Update();




        if (movementDirection != Vector3.zero && interactiveBoxC == null && !boxGrabbedX && !boxGrabbedY)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (movement.x != 0 || movement.y != 0 && !froze)
        {
            animatorC.SetBool("running", true);
            characterControllerC.Move(movementDirection * actualSpeed * Time.deltaTime);
            SoundManager.PlaySound(SoundManager.Sound.PaxSteps, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxSteps);
        }
        else
        {
            animatorC.SetBool("running", false);
        }

        // HOLDING BOX //

        if (interactiveBoxC != null)
        {
            SoundManager.StopSound(SoundManager.Sound.PaxSteps, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxSteps);
            SoundManager.PlaySound(SoundManager.Sound.HoldBox, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxAction);
            animatorC.SetBool("holding", true);
        }
        else
        {
            animatorC.SetBool("holding", false);
        }

        //GRAVITY AND JUMP//


        ySpeed += Physics.gravity.y * gravity * Time.deltaTime;
        

        if (characterControllerC.isGrounded && !jumping)
        {
            ySpeed = -0.5f;
            isgrounded = true;
        }
        else
        {            
            isgrounded = false;
        }

        Vector3 velocity = movementDirection;
        velocity.y = ySpeed;
        if (!froze)
        {
            characterControllerC.Move(velocity * Time.deltaTime); //Para a�adirle gravedad
        }

        if(characterControllerC.isGrounded && jumping)
        {
            jumping = false;
            animatorC.SetBool("jump", false);
            SoundManager.PlaySound(SoundManager.Sound.PaxFall, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxAction);
        }
        
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        //MOVEMENT//
        if (!froze)
        {
            movement = context.ReadValue<Vector2>();
            movementDirection = new Vector3(movement.x, 0, movement.y).normalized;            

            /*if (movement.x != 0 || movement.y != 0)
            {
                characterControllerC.Move(movementDirection * actualSpeed * Time.deltaTime);
            }*/
        }
    }
    public void OnMovementEnd(InputAction.CallbackContext context)
    {
        SoundManager.StopSound(SoundManager.Sound.PaxSteps, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxSteps);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (!jumping && !froze && !crouching && !boxGrabbedX && !boxGrabbedY && interactiveBoxC == null)
        {
            jumping = true;
            animatorC.SetBool("jump", true);
            SoundManager.PlaySound(SoundManager.Sound.PaxJump, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxAction);
            //characterControllerC.Move(new Vector3(characterControllerC.velocity.x, jumpSpeed, characterControllerC.velocity.z) * 0.1f);
            ySpeed = jumpSpeed;
            Debug.Log(ySpeed);
            Debug.Log(jumpSpeed);
        }
    }

    public void OnJumpEnd(InputAction.CallbackContext context)
    {
        if(jumping && !froze)
        {
            
        }    
    }

    public void IncreaseSpeed(float increase)
    {
        if(!froze)
        {
            animatorC.SetBool("sprint", true);
            actualSpeed += increase;
        }
    }

    public void RegulateSpeed()
    {
        if(!froze && !shadowForm)
        {
            animatorC.SetBool("sprint", false);
            actualSpeed = normalSpeed;
        }

        if(!froze && shadowForm)
        {
            animatorC.SetBool("sprint", false);
            actualSpeed = normalSpeed + 1;
        }
    }

    public void ReduceSpeed(float decrease)
    {
        if(!froze)
        {
            actualSpeed -= decrease;
        } 
    }

    public void OnGrabbed(int direction)
    {
        if(!froze)
        {
            if (direction == 0)
            {
                movement.x = 0;
                boxGrabbedY = true;
            }
            else
            {
                movement.y = 0;
                boxGrabbedX = true;
            }
        }
    }
    public void NotGrabbed()
    {
        if(!froze)
        {
            boxGrabbedY = false;
            boxGrabbedX = false;
        }
    }

    public void CrouchPax(InputAction.CallbackContext context)
    {
        if(!shadowForm && !jumping && !froze && !boxGrabbedX && !boxGrabbedY && interactiveBoxC == null)
        {
            if (!crouching)
            {
                RegulateSpeed();
                crouching = true;
            }
            else
            {
                RegulateSpeed();
                crouching = false;
            }
        }
    }

    public void ShadowTransform()
    {
        if (!shadowForm)
        {
            SoundManager.PlaySound(SoundManager.Sound.Shadow3DTransform, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxAction);
            shadowForm = true;
        }
    }

    public void ShadowUnTransform()
    {
        if (shadowForm)
        {                
            shadowForm = false;
            unTransform = true;
            SoundManager.PlaySound(SoundManager.Sound.ToPaxTransfom, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxAction);
            FrozePax();
        }
    }

    public void FrozePax()
    {
        froze = true;
        movement.x = 0;
        movement.y = 0;
        movementDirection.x = 0;
        movementDirection.y = 0;
        movementDirection.z = 0;
    }
    
    public void UnFrozePax()
    {
        frozeTimer = 0;
        froze = false;
    }

    public void SetPlayerPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }



    //TRYING TO FIX EDGE PROBLEMS WITH COLLIDER
    /*private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!Physics.Raycast(transform.position - new Vector3(0.1f, -0.08f, 0), Vector3.down, 0.1f) && characterControllerC.isGrounded)
        {
            Vector3 edgeFallMovement = -transform.forward;
            edgeFallMovement.y = 0;
            movementDirection -= (edgeFallMovement * 1);
        }
    }*/

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position - new Vector3(0.1f, -0.08f, 0), (transform.position - new Vector3(0.1f, -0.08f, 0)) + new Vector3(0, -0.1f, 0), Color.red);
    }
}
