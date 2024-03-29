using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerTransition : MonoBehaviour
{
    public Camera mainCamera;
    CameraFSMController mainCameraC;

    public Transform nextPoint;
    public Transform lastPoint;

    public Transform trigger;
    CameraTriggerTransition triggerTransitionC;
    CameraTriggerFollow triggerFollowC;
    CameraTriggerOffset triggerOffsetC;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if(activatedTrigger)
            {
                mainCameraC.InterpolationTime(cameraTimeToMove);
                mainCameraC.SwapPoints(nextPoint, lastPoint);
                activatedTrigger = false;
                if (triggerTransitionC != null)
                {
                    triggerTransitionC.activatedTrigger = true;
                }
                else if (triggerFollowC != null)
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
