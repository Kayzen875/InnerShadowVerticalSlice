using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereIsMyCook : MonoBehaviour
{
    public GameObject cook;
    public int spot;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("prueba");
        cook.GetComponent<Navigation>().SetSpot(spot);
    }
}
