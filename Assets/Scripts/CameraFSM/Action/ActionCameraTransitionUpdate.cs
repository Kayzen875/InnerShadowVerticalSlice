using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Action/ActionCameraTransitionUpdate")]
public class ActionCameraTransitionUpdate : FSM.Action
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

        position = Vector3.Lerp(c.lastPoint.position, c.nextPoint.position, curvePercent);
        rotation = Quaternion.Lerp(c.lastPoint.rotation, c.nextPoint.rotation, curvePercent);

        c.transform.position = position;
        c.transform.rotation = rotation;

        if(c.factorT >= c.factorTotal)
        {

            c.cameraTransition = false;
            c.cameraFixed = true;

        }
    }
}
