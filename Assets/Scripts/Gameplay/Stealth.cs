using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    public GameObject cook;

    void OnTriggerEnter(Collider other)
    {
        cook.GetComponent<Navigation>().SetHidden(true);
    }

    void OnTriggerExit(Collider other)
    {
        cook.GetComponent<Navigation>().SetHidden(false);
    }
}
