using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlanes : MonoBehaviour
{
    public GameObject paxToShadow2D;
    public GameObject planeToAdd;
    private bool firstTime;
    public bool addPlane;
    public int index;

    void OnTriggerEnter()
    {
        if(!firstTime && !addPlane)
        {
            paxToShadow2D.GetComponent<ParticleSystem>().collision.RemovePlane(index);
            firstTime = true;
        }
        else if(addPlane)
        {
            paxToShadow2D.GetComponent<ParticleSystem>().collision.AddPlane(planeToAdd.transform);
            firstTime = true;
        }
    }
}
