using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerOffset : MonoBehaviour
{
    public Camera mainCamera;
    CameraFSMController mainCameraC;

    public Vector3 newOffset;

    public Transform trigger;
    CameraTriggerOffset triggerOffsetC;
    CameraTriggerFollow triggerFollowC;
    CameraTriggerTransition triggerTransitionC;

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
                triggerTransitionC = transition;
            }
            else if (trigger.TryGetComponent(out CameraTriggerFollow follow))
            {
                triggerFollowC = trigger.GetComponent<CameraTriggerFollow>();
            }
            else if (trigger.TryGetComponent(out CameraTriggerOffset offset))
            {
                triggerOffsetC = trigger.GetComponent<CameraTriggerOffset>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (activatedTrigger)
            {
                activatedTrigger = false;
                mainCameraC.CameraOffset(newOffset);
                mainCameraC.InterpolationTime(cameraTimeToMove);
                if (triggerTransitionC != null)
                {
                    triggerTransitionC.activatedTrigger = true;
                }
                else if(triggerFollowC != null)
                {
                    triggerFollowC.activatedTrigger = true;
                }
                else if (triggerOffsetC != null)
                {
                    triggerOffsetC.activatedTrigger = true;
                }
                
                
            }
        }
    }
}
