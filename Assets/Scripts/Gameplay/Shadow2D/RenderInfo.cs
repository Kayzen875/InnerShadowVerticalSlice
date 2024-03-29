using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderInfo : MonoBehaviour
{
    public GameObject renderPlayerRef;
    public Transform nextPoint;
    public Transform lastPoint;
    public float interpolationTime;
    public bool followCamera;

    public GameObject GivePlayerRef()
    {
        return renderPlayerRef;
    }

    public Transform NextPoint()
    {
        return nextPoint;
    }
    public Transform LastPoint()
    {
        return lastPoint;
    }
    public float InterpolationTime()
    {
        return interpolationTime;
    }

    public bool FollowCamera()
    {
        return followCamera;
    }
}
