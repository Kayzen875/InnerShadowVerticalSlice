using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeaveWall : MonoBehaviour
{
    [SerializeField]
    private GameObject pax;
    [SerializeField]
    private GameObject paxRenderer;
    [SerializeField]
    private GameObject shadow3DRenderer;
    [SerializeField]
    private Camera Camera2D;
    [SerializeField]
    private Camera mainCamera;

    public Transform nextPoint;
    public Transform lastPoint;
    public float interpolationTime;
    public bool followCamera;

    CameraFSMController cameraC;

    [Header("raycast")]
    //private bool inRange;
    public float length;
    public Vector3 direction;
    public LayerMask conditionLayer;

    [Header("particles")]
    [SerializeField]
    private GameObject Shadow2DToPax;
    public float particleTimer;
    public bool forceLeave = true;
    public bool tryingToLeave;

    void OnEnable()
    {
        GetComponent<PlayerInput>().actions["LeaveWall"].performed += UnFuseWall;
    }
    void OnDisable()
    {
        GetComponent<PlayerInput>().actions["LeaveWall"].performed -= UnFuseWall;
    }

    private void Start()
    {
        cameraC = mainCamera.GetComponent<CameraFSMController>();
    }
    void Update()
    {
        if(Shadow2DToPax.GetComponent<ParticleSystem>().isEmitting)
        {
            tryingToLeave = true;
        }

        if(!Shadow2DToPax.GetComponent<ParticleSystem>().isEmitting && tryingToLeave)
        {
            tryingToLeave = false;
            ExitWall();
        }
    }

    public void UnFuseWall(InputAction.CallbackContext context)
    {
        LeaveAnimation();
        GetComponent<ControllerShadow2D>().SwapToFrozen(true);
        forceLeave = false;
    }

    public void LeaveAnimation()
    {
        Shadow2DToPax.SetActive(true);         
    }

    public void ExitWall()
    {
        if (!forceLeave)
        {
            paxRenderer.SetActive(true);
            pax.GetComponent<PaxFSMController>().UnFrozePax();
            if(!followCamera)
            {
                MoveCameraTransition();
            }
            else
            {
                MoveCameraFollow();
            }
        }
        else
        {
            pax.GetComponent<PaxFSMController>().paxAnimator.gameObject.SetActive(false);
            pax.GetComponent<PaxFSMController>().shadow3DAnimator.gameObject.SetActive(true);
            pax.GetComponent<PaxFSMController>().paxToShadow3D.SetActive(true);
            pax.GetComponent<PaxFSMController>().shadow3DToPax.SetActive(false);
            pax.GetComponent<PaxFSMController>().animatorC = pax.GetComponent<PaxFSMController>().shadow3DAnimator;
            pax.GetComponent<PaxFSMController>().frozeTimer = 0;
            pax.GetComponent<PaxFSMController>().FrozePax();
            if (!followCamera)
            {
                MoveCameraTransition();
            }
            else
            {
                MoveCameraFollow();
            }
        }

        GetComponent<ControllerShadow2D>().SwapToFrozen(false);
        Shadow2DToPax.SetActive(false);
        Camera2D.enabled = false;
        mainCamera.enabled = true;
        gameObject.SetActive(false);
    }

    public void ForceLeave()
    {
        forceLeave = true;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.forward * 1);
    }

    public void MoveCameraTransition()
    {
        cameraC.SwapPoints(nextPoint, lastPoint);
        cameraC.InterpolationTime(interpolationTime);
    }
    public void MoveCameraFollow()
    {
        cameraC.FollowPlayer(nextPoint, lastPoint);
        cameraC.InterpolationTime(interpolationTime);
    }
}
