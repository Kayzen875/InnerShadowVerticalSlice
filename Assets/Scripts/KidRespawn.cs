using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRespawn : MonoBehaviour
{
    public int scene;
    private bool loaded;

    void OnTriggerEnter(Collider other)
    {
        if(!loaded)
        {
            loaded = true;
            LevelManager.Instance.GameOver(scene);
        }
    }

    void OnTriggerExit(Collider other)
    {
        loaded = false;
    }
}
