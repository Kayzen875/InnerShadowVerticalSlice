using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsInteractions : MonoBehaviour
{
    Light objectLight;
    public bool active;
    public float editorTimer;

    private float timer;
    private float innerTime;

    private void Start()
    {
        objectLight = GetComponent<Light>();
    }

    private void Update()
    {
        if(active)
        {
            timer = editorTimer;
            active = false;
        }

        if(timer > 0)
        {
            if(innerTime > 0.2f)
            {
                objectLight.enabled = false;
                innerTime -= Time.deltaTime;
            }
            else if(innerTime > 0.1f)
            {
                objectLight.enabled = true;
                innerTime -= Time.deltaTime;
            }
            else 
            {
                innerTime = 0.3f;
            }
            timer -= Time.deltaTime;
        }
        //else { objectLight.enabled = true; }
    }

    public void startSpam()
    {
        active = true;
    }
}
