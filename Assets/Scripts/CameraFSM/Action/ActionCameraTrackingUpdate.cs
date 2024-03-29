using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Action/ActionCameraTrackingUpdate")]
public class ActionCameraTrackingUpdate : FSM.Action
{
    Vector3 position;
    Quaternion rotation;
    public AnimationCurve animationCurve;
    public override void Act(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        float factor = c.factorT / c.factorTotal;

        c.factorT += Time.deltaTime;

        float curvePercent = animationCurve.Evaluate(factor);
        //position = c.lastPoint.position + (c.lastPoint.position - c.nextPoint.position) * t / total;
        if (c.cameraTrackPoint1[c.i] != null || c.cameraTrackPoint1[c.i+1] != null)
        {
            position = Vector3.Lerp(c.cameraTrackPoint1[c.i].position, c.cameraTrackPoint1[c.i + 1].position, curvePercent);
            rotation = Quaternion.Lerp(c.cameraTrackPoint1[c.i].rotation, c.cameraTrackPoint1[c.i + 1].rotation, curvePercent);
            c.transform.position = position;
            c.transform.rotation = rotation;
        }

        if (c.factorT >= c.factorTotal)
        {
            c.Tracking(c.nextPoint,c.lastPoint);
            c.i++;
        }

        if(c.i + 1 >= c.cameraTrackPoint1.Length)
        {
            c.cameraTracking = false;
            c.cameraFollow = true;
            c.goToFollow = true;
            c.factorTotal = 2;
            Debug.Log("PASA POR AQUÍ");
            c.i = 0;
        }
    }
}
