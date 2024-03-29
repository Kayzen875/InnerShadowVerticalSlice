using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MergeWall : MonoBehaviour
{
    [SerializeField]
    private GameObject renderPlayer;
    
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject renderShadow2D;

    public Transform nextPoint;
    public Transform lastPoint;
    bool followCamera;

    CameraFSMController cameraC;

    public GameObject paxRenderer;

    [Header("raycast")]
    private bool inRange;
    public float length;
    public float wallDistance;
    float interpolationTime;
    public Vector3 direction;
    public LayerMask conditionLayer;
    private RaycastHit hit;

    [Header("particles")]
    [SerializeField]
    private GameObject paxToShadow2D;
    public bool tryingToEnter;

    void OnEnable()
    {
        GetComponent<PlayerInput>().actions["ProjectShadow"].performed += FuseWall;
    }
    void OnDisable()
    {
        GetComponent<PlayerInput>().actions["ProjectShadow"].performed -= FuseWall;
    }

    private void Start()
    {
        cameraC = mainCamera.GetComponent<CameraFSMController>();
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, length, conditionLayer))
        {
            inRange = true;
            renderPlayer = hit.collider.GetComponent<RenderInfo>().GivePlayerRef();
            nextPoint = hit.collider.GetComponent<RenderInfo>().NextPoint();
            lastPoint = hit.collider.GetComponent<RenderInfo>().LastPoint();
            interpolationTime = hit.collider.GetComponent<RenderInfo>().InterpolationTime();
            followCamera = hit.collider.GetComponent<RenderInfo>().FollowCamera();
        }
        else
        {
            inRange = false;
        }

        if (paxToShadow2D.GetComponent<ParticleSystem>().isEmitting)
        {
            tryingToEnter = true;
        }

        if (paxToShadow2D.GetComponent<ParticleSystem>().isStopped && tryingToEnter)
        {
            tryingToEnter = false;
            EnterWall();
        }
    }

    public void FuseWall(InputAction.CallbackContext context)
    {
        if (inRange && !GetComponent<PaxFSMController>().shadowForm && !GetComponent<PaxFSMController>().froze)
        {
            paxToShadow2D.SetActive(true);
            GetComponent<PaxFSMController>().froze = true;
        }
    }

    public void EnterWall()
    {
        SoundManager.PlaySound(SoundManager.Sound.Shadow2D, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.PaxAction);
        paxToShadow2D.SetActive(false);
        renderPlayer.SetActive(true);
        renderPlayer.transform.position = new Vector3(hit.point.x, hit.point.y + 0.25f, hit.point.z + wallDistance);
        renderShadow2D.transform.position = new Vector3(renderPlayer.transform.position.x, renderPlayer.transform.position.y, hit.point.z - 0.05f);
        renderPlayer.GetComponent<LeaveWall>().nextPoint = lastPoint;
        renderPlayer.GetComponent<LeaveWall>().lastPoint = nextPoint;
        renderPlayer.GetComponent<LeaveWall>().interpolationTime = interpolationTime;
        renderPlayer.GetComponent<LeaveWall>().followCamera = followCamera;
        //Camera.main.enabled = false;
        //renderCamera.enabled = true;
        MoveCamera();
        //paxRenderer.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * length);
    }

    public void MoveCamera()
    {
        cameraC.SwapPoints(nextPoint, lastPoint);
        cameraC.InterpolationTime(interpolationTime);
    }
}
