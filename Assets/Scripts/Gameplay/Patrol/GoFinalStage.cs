using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFinalStage : MonoBehaviour
{
    public Transform cook;
    private bool done;

    private void OnTriggerEnter(Collider other)
    {
        if (!done)
        {
            done = true;
            cook.GetComponent<Navigation>().FinalPhase();
        }
    }
}
