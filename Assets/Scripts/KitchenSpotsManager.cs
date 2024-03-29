using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSpotsManager : MonoBehaviour
{
    public static int whereIsPax;
    public int spot;

    private void Start()
    {
        whereIsPax = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        whereIsPax = spot;
    }
}
