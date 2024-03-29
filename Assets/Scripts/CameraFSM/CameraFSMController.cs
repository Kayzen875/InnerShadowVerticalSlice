using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class CameraFSMController : Controller
{
    public Transform[] cameraPoints;

    public Transform[] cameraTrackPoint1;
    public Transform[] cameraTrackPoints2;
    public Transform nextPoint;
    public Transform actualPoint;
    public Transform lastPoint;
    public Transform player;

    public bool cameraFixed;
    public bool cameraFollow;
    public bool cameraTransition;
    public bool cameraTracking;
    public bool cameraRotation;
    public bool cameraHideandShow;
    public bool goToFollow;

    public float factorT = 0;
    public float factorTotal;
    public int i = 0;

    public Vector3 offset;

    override public void Start()
    {
        actualPoint = cameraPoints[0];
        nextPoint = cameraPoints[1];
        cameraRotation = false;
        offset = new Vector3(0, 3.23f, -2.69f);
        base.Start();
    }

    // Update is called once per frame
    override public void Update()
    {
        base.Update();
    }

    public void SwapPoints(Transform point1, Transform point2)
    {
        if(!cameraTransition)
        {
            nextPoint = point1;
            lastPoint = point2;
            cameraTransition = true;
            cameraFixed = false;
            cameraFollow = false;
            cameraHideandShow = false;
        }
        else
        {
            lastPoint = transform;
            nextPoint = point1;
            factorT = 0;
        }

    }
    public void InterpolationTime(float total)
    {
        factorTotal = total;
    }

    public void FollowPlayer(Transform point1, Transform point2)
    {
        //if(!cameraTransition)
        //{
            nextPoint = point1;
            lastPoint = point2;
            cameraTransition = false;
            cameraFixed = false;
            cameraFollow = true;
        //}
        //else
        //{
            
        //}

    }

    public void Tracking(Transform point1, Transform point2)
    {
        nextPoint = point1;
        lastPoint = point2;
        cameraTransition = false;
        cameraFixed = false;
        cameraFollow = false;
        cameraTracking = true;
    }

    public void CameraRotate()
    {
        Vector3 variation;
        variation = new Vector3(0, 2, 0);
        offset -= variation;
    }

    public void CameraOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
}
