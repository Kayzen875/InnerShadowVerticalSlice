using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicFreeze : MonoBehaviour
{
    public float freezeTimer;
    public bool unFreezed;

    public Transform character;

    void Start()
    {
        character.GetComponent<PaxFSMController>().FrozePax();
    }

    void Update()
    {
        if(freezeTimer > 0 && !unFreezed)
        {
            freezeTimer -= Time.deltaTime;
        }
        else if(!unFreezed)
        {
            character.GetComponent<PaxFSMController>().UnFrozePax();
            unFreezed = true;
        }
    }
}
