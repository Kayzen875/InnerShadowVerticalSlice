using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerTracking : MonoBehaviour
{
    public Camera mainCamera;
    CameraFSMController mainCameraC;

    public Transform nextPointToFollow;
    public Transform lastPointTrack;
    public Transform player;
    public Transform newPlayerPosition;

    public float cameraTimeToMove;

    public bool activatedTrigger;
    // Start is called before the first frame update
    void Start()
    {
        mainCameraC = mainCamera.GetComponent<CameraFSMController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            mainCameraC.Tracking(nextPointToFollow, lastPointTrack);
            mainCameraC.InterpolationTime(cameraTimeToMove);
            mainCameraC.CameraOffset(nextPointToFollow.position);
        }
    }
}
