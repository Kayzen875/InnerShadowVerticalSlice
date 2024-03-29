using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    bool canMoveV;
    bool canMoveH;
    bool triggerCanMove;
    bool grabbed;

    public float lenght;

    public bool bigBox;

    float boxSizeX;
    float boxSizeZ;

    public LayerMask WallLayer;
    public GameObject playerReference;
    public PaxFSMController paxControllerC;

    // Start is called before the first frame update
    void Start()
    {
        triggerCanMove = true;
        boxSizeX = transform.localScale.x / 2;
        boxSizeZ = transform.localScale.z / 2; //La resta sirve para que no se choque con el archivador
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGrabbed()
    {
        grabbed = true;
        Debug.Log("OnGrabbed is: " + grabbed);
    }     
    
    public void NotGrabbed()
    {
        grabbed = false;
        DeletePlayerReference();    
        Debug.Log("OnGrabbed is: " + grabbed);
    } 
    
    public void MoveVertical(float z)//A�adir par�metros
    {
        Debug.Log("me deberia mover");
        if(CanMoveVertical(z) && grabbed /*&& triggerCanMove*/)
        {
            transform.position += new Vector3(0, 0, z * Time.deltaTime);
        }
    }
    public void MoveHorizontal(float x)//A�adir par�metros
    {
        if (CanMoveHorizontal(x) && grabbed /*&& triggerCanMove*/)
        {
            transform.position += new Vector3(x * Time.deltaTime, 0, 0);
        }
    }

    //OPCION UNO//
    public bool CanMoveHorizontal(float x) //Hay que delimitar un espacio d�nde se pueda desplazar esa caja, aparte de cuando choca o no con obstaculos para que cambie el booleano y pueda permitir mover a la caja o no
    {
        if(x > 0)
        {
            if (Physics.Raycast(transform.position, new Vector3(boxSizeX, 0, 0), lenght, WallLayer) ||
                Physics.Raycast(transform.position + new Vector3(0, 0, boxSizeZ/2), new Vector3(boxSizeX, 0, 0), lenght, WallLayer) ||
                Physics.Raycast(transform.position + new Vector3(0, 0, -boxSizeZ/2), new Vector3(boxSizeX, 0, 0), lenght, WallLayer)) //Esto tambi�n en vez de hacer tres raycasts se puede lograr con un boxcast
            {
                Debug.Log("Toca la pared Horizontal");
                canMoveH = false;
                if(paxControllerC.movementDirection.x >= 0)
                {
                    paxControllerC.movementDirection.x = 0;
                }
            }
            else
            {
                canMoveH = true;
            }
        }
        else
        {
            if (Physics.Raycast(transform.position, new Vector3(-boxSizeX, 0, 0), lenght, WallLayer) ||
                Physics.Raycast(transform.position + new Vector3(0, 0, boxSizeZ/2), new Vector3(-boxSizeX, 0, 0), lenght, WallLayer) ||
                Physics.Raycast(transform.position + new Vector3(0, 0, -boxSizeZ/2), new Vector3(-boxSizeX, 0, 0), lenght, WallLayer))
            {
                Debug.Log("Toca la pared Horizontal");
                canMoveH = false;
                if (paxControllerC.movementDirection.x <= 0)
                {
                    paxControllerC.movementDirection.x = 0;
                }
            }
            else
            {
                canMoveH = true;
            }
        }
        return canMoveH;
    }    
    public bool CanMoveVertical(float z)
    {
        if (z > 0)
        {
            if (Physics.Raycast(transform.position, new Vector3(0, 0, boxSizeZ), lenght, WallLayer) ||
                Physics.Raycast(transform.position + new Vector3(boxSizeZ/2, 0, 0), new Vector3(0, 0, boxSizeZ), lenght, WallLayer) ||
                Physics.Raycast(transform.position + new Vector3(-boxSizeZ/2, 0, 0), new Vector3(0, 0, boxSizeZ), lenght, WallLayer))
            {
                Debug.Log("Toca la pared Vertical");
                canMoveV = false;
                if (paxControllerC.movementDirection.z >= 0)
                {
                    paxControllerC.movementDirection.z = 0;
                }
            }
            else
            {
                canMoveV = true;
            }
        }
        else
        {
            if (Physics.Raycast(transform.position, new Vector3(0, 0, -boxSizeZ), lenght, WallLayer) ||
                Physics.Raycast(transform.position + new Vector3(boxSizeX/2, 0, 0), new Vector3(0, 0, -boxSizeZ), lenght, WallLayer) ||
                Physics.Raycast(transform.position + new Vector3(-boxSizeX/2, 0, 0), new Vector3(0, 0, -boxSizeZ), lenght, WallLayer))
            {
                Debug.Log("Toca la pared Vertical");
                canMoveV = false;
                if (paxControllerC.movementDirection.z <= 0)
                {
                    paxControllerC.movementDirection.z = 0;
                }
            }
            else
            {
                canMoveV = true;
            }
        }
        return canMoveV;
    }
    //OPCION UNO//


    //OPCION DOS//
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(playerReference != null)
            {
                playerReference.GetComponent<PaxFSMController>().interactiveBoxC = null;
                playerReference = null;
            }
        }
    }*/

    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            triggerCanMove = true;
        }
    }*/
    //OPCION DOS//
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + new Vector3(0, 0, 0.5f), transform.position + new Vector3(1.5f,0,0.5f));
    }

    public void GetPlayerReference(GameObject playerRef)
    {
        playerReference = playerRef;
        paxControllerC = playerReference.GetComponent<PaxFSMController>();
    }

    public void DeletePlayerReference()
    {
        playerReference = null;
        paxControllerC = null;
    }
}
