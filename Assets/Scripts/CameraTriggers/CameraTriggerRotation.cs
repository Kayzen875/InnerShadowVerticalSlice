using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerRotation : MonoBehaviour
{
    public Camera mainCamera;
    CameraFSMController mainCameraC;

    public Transform nextPoint;
    public Transform lastPoint;

    public Transform trigger;
    CameraTriggerTransition triggerC;
    CameraTriggerFollow triggerFollowC;

    public float cameraTimeToMove;

    public bool activatedTrigger;
    // Start is called before the first frame update
    void Start()
    {
        mainCameraC = mainCamera.GetComponent<CameraFSMController>();
        if (trigger != null)
        {
            if (trigger.TryGetComponent(out CameraTriggerTransition transition))
            {
                triggerC = transition;
            }
            else
            {
                triggerFollowC = trigger.GetComponent<CameraTriggerFollow>();
            }
        }
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
                mainCameraC.CameraRotate();
                activatedTrigger = false;
                if (triggerC != null)
                {
                    triggerC.activatedTrigger = true;
                }
                else
                {
                    triggerFollowC.activatedTrigger = true;
                }
                mainCameraC.InterpolationTime(cameraTimeToMove);
            }
        }
    }
}
