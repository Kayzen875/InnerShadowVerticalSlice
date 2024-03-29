using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject popUp;

    void OnTriggerEnter(Collider other)
    {
        popUp.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        popUp.SetActive(false);
    }
}
