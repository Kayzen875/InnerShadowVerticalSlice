using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerFollow : MonoBehaviour
{
    public Camera mainCamera;
    CameraFSMController mainCameraC;

    public Transform nextPoint;
    public Transform lastPoint;

    public Transform trigger;
    CameraTriggerTransition triggerC;

    public float cameraTimeToMove;

    bool following;

    public Vector3 newOffSet;

    public bool activatedTrigger;
    // Start is called before the first frame update
    void Start()
    {
        mainCameraC = mainCamera.GetComponent<CameraFSMController>();
        triggerC = trigger.GetComponent<CameraTriggerTransition>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (activatedTrigger)
            {
                mainCameraC.FollowPlayer(nextPoint, lastPoint);
                if(!following)
                {
                    mainCameraC.CameraOffset(newOffSet);
                    following = true;
                }
                else
                {
                    mainCameraC.CameraOffset(new Vector3(0,0,0));
                    following = false;
                }
                activatedTrigger = false;
                triggerC.activatedTrigger = true;
                mainCameraC.InterpolationTime(cameraTimeToMove);
            }
        }
    }
}
