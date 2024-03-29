using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Camera/Action/ActionCameraFollowUpdate")]
public class ActionCameraFollowUpdate : FSM.Action
{
    public float smoothMove;
    public bool cameraMoved;
    
    public override void Act(Controller controller)
    {
        CameraFSMController c = (CameraFSMController)controller;

        //SEGUIR A PAX SIN LOOK AT Y SOLO EN X//
        Vector3 playerPosition = c.player.position - new Vector3(-0.1f, c.player.position.y, c.player.position.z);
        Vector3 desiredPosition = playerPosition + (c.offset - new Vector3(c.offset.x, 0, 0));
        Vector3 smoothedPosition = Vector3.Lerp(c.transform.position, desiredPosition, smoothMove);
        c.transform.position = smoothedPosition;
        //SEGUIR A PAX SIN LOOK AT Y SOLO EN X//


        //SEGUIR A PAX SIN PROFUNDIDAR Y CON LOOK AT//
        /*Vector3 playerPosition = c.player.position - new Vector3(0, 0, c.player.position.z);
        Vector3 desiredPosition = playerPosition + c.offset;
        Vector3 smoothedPosition = Vector3.Lerp(c.transform.position, desiredPosition, smoothMove);
        c.transform.position = smoothedPosition;
        c.transform.LookAt(c.player);*/
        //SEGUIR A PAX SIN PROFUNDIDAR Y CON LOOK AT//

        //SEGUIR A PAX CON PROFUNDIDAD//
        /*Vector3 desiredPosition = c.player.position + c.offset;
        Vector3 smoothedPosition = Vector3.Lerp(c.transform.position, desiredPosition, smoothMove);
        c.transform.position = smoothedPosition;

        c.transform.LookAt(c.player);*/
        //SEGUIR A PAX CON PROFUNDIDAD//

        /*//position = c.lastPoint.position + (c.lastPoint.position - c.nextPoint.position) * t / total;
        Vector3 desiredPosition = Vector3.Lerp(c.lastPoint.position, c.nextPoint.position, smoothMove);

        Vector3 position = desiredPosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(c.transform.position, position, smoothMove);

        c.transform.position = smoothedPosition;
        c.transform.LookAt(c.player);
        //c.transform.rotation = rotation;*/

    }
}
