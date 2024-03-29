using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class ControllerShadow2D : MonoBehaviour
{
    //Animations & Particles
    public Animator _anim;
    public ParticleSystem _exitParticles;
    private float microTimer;
    private float animationTimer;
    private float pushAnimationTimer;
    private bool pushAnimationEnded;
    private bool SwitchAnimationEnded;
    private RaycastHit hit;

    //Movement
    private Rigidbody rb;
    public float jumpForce;
    public float speed;
    public bool frozeShadow2D;

    [Header("Player")]
    public Camera camera2D;
    public Camera mainCamera;
    public LayerMask renderInteractive;
    public LayerMask MovableBox;
    public LayerMask renderScenario;
    public Transform leftFoot;
    public Transform rightFoot;
    Vector2 movement;

    public event Action WallLeave = delegate { };

    void OnEnable()
    {
        GetComponent<PlayerInput>().actions["Movement2D"].performed += OnMovement2D;
        GetComponent<PlayerInput>().actions["Movement2D"].canceled += OnMovement2D;
        GetComponent<PlayerInput>().actions["Jump2D"].performed += OnJump2D;
        GetComponent<PlayerInput>().actions["Switches"].performed += OnPressSwitch;
        GetComponent<PlayerInput>().actions["PushBoxes"].performed += OnBoxPushed;
    }

    void OnDisable()
    {
        GetComponent<PlayerInput>().actions["Movement2D"].performed -= OnMovement2D;
        GetComponent<PlayerInput>().actions["Movement2D"].canceled -= OnMovement2D;
        GetComponent<PlayerInput>().actions["Jump2D"].performed -= OnJump2D;
        GetComponent<PlayerInput>().actions["Switches"].performed -= OnPressSwitch;
        GetComponent<PlayerInput>().actions["PushBoxes"].performed -= OnBoxPushed;
    }
    private void Start()
    {
        animationTimer = 1;
        pushAnimationTimer = 1;
        SwitchAnimationEnded = true;
        pushAnimationEnded = true;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.35f, renderScenario) && microTimer <= 0)
        {
            _anim.SetBool("Jumping", false);
        }

        if (animationTimer > 0 && !SwitchAnimationEnded)
        {
            animationTimer -= Time.deltaTime;
        }
        else if (!SwitchAnimationEnded && animationTimer < 0)
        {
            SwitchAnimationEnded = true;
            OnPressSwitchEndAnimation();
            animationTimer = 1;
        }
        else if (pushAnimationTimer > 0 && !pushAnimationEnded)
        {
            pushAnimationTimer -= Time.deltaTime;
        }
        else if (!pushAnimationEnded && pushAnimationTimer < 0)
        {
            pushAnimationEnded = true;
            OnPushEndAnimation();
            pushAnimationTimer = 1;
        }

        if (microTimer > 0)
        {
            microTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0);
    }

    private void OnMovement2D(InputAction.CallbackContext context)
    {
        if (!frozeShadow2D)
        {
            movement = context.ReadValue<Vector2>();
            _anim.SetInteger("Direction", (int)movement.x);
        }
    }

    private void OnJump2D(InputAction.CallbackContext context)
    {
        if (!frozeShadow2D)
        {
            if (Physics.Raycast(transform.position, -transform.up, 0.35f, renderScenario))
            {
                _anim.SetBool("Jumping", true);
                microTimer = 0.2f;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            }
        }
    }

    private void OnPressSwitch(InputAction.CallbackContext context)
    {
        if (!frozeShadow2D)
        {
            if (Physics.Raycast(transform.position, -transform.forward, out hit, 1.0f, renderInteractive))
            {
                _anim.SetTrigger("Push");
                SwitchAnimationEnded = false;
                animationTimer = 0.4f;
            }
        }

    }

    private void OnBoxPushed(InputAction.CallbackContext context)
    {
        if (!frozeShadow2D)
        {
            if (Physics.Raycast(transform.position, -transform.forward, out hit, 1.0f, MovableBox))
            {
                _anim.SetTrigger("Push");
                pushAnimationEnded = false;
                pushAnimationTimer = 0.4f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position - Vector3.forward * 1);
    }

    public void SwapToFrozen(bool bol)
    {
        frozeShadow2D = bol;
    }

    public void OnPressSwitchEndAnimation()
    {
        hit.collider.gameObject.GetComponent<GenericEvents>().TriggerEvents(0);
        SoundManager.PlaySound(SoundManager.Sound.Switch, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.Switch);
        frozeShadow2D = true;
        GetComponent<LeaveWall>().ForceLeave();
        GetComponent<LeaveWall>().LeaveAnimation();
    }

    public void OnPushEndAnimation()
    {
        hit.collider.gameObject.GetComponent<InteractBox2D>().AddRigidBody();
        SoundManager.PlaySound(SoundManager.Sound.BoxFall, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.Box);
    }
}
