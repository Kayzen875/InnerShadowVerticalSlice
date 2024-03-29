using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaxTo : MonoBehaviour
{
    public Transform newPlayerPos;
    bool paxMoved;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if(!paxMoved)
            {
                other.GetComponent<PaxFSMController>().FrozePax();
                other.GetComponent<PaxFSMController>().SetPlayerPosition(newPlayerPos.position);
                paxMoved = true;
            }
        }
    }
}
