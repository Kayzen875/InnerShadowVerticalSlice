using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipFirstCinematic : MonoBehaviour
{
    public float cinematicTime;
    public bool alredyActivated;

    private void Update()
    {
        if(cinematicTime > 0)
        {
            cinematicTime -= Time.deltaTime;
        }
        else if(!alredyActivated)
        {
            alredyActivated = true;
            LevelManager.Instance.LoadAsyncScene(1);
        }
    }
}
