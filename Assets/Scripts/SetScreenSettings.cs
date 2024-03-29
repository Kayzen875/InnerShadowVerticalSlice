using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetScreenSettings : MonoBehaviour
{
    Material screenSettings;

    void OnEnable()
    {
        ChangeOnScreenSliders.ChangeOnSliders += UpdateScreenSettings;

    }

    void OnDisable()
    {
        ChangeOnScreenSliders.ChangeOnSliders -= UpdateScreenSettings;
        
    }

    private void Awake()
    {
        GetComponent<Renderer>().material.SetFloat("_Contraste", PlayerPrefs.GetFloat("Contraste"));
        GetComponent<Renderer>().material.SetFloat("_Brillo", PlayerPrefs.GetFloat("Brillo")); 
    }

    void UpdateScreenSettings()
    {
        GetComponent<Renderer>().material.SetFloat("_Contraste", PlayerPrefs.GetFloat("Contraste"));
        GetComponent<Renderer>().material.SetFloat("_Brillo", PlayerPrefs.GetFloat("Brillo"));
    }
}
