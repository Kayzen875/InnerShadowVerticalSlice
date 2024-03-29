using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenLightsManager : MonoBehaviour
{
    [Header("Lights")]
    public Light lumen;

    [Header("Pax")]
    public GameObject pax;

    private void OnTriggerEnter(Collider other)
    {
        if(lumen.enabled)
        {
            pax.GetComponent<PaxFSMController>().ShadowUnTransform();
        }
        else { pax.GetComponent<PaxFSMController>().ShadowTransform(); }
    }
}
