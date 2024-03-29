using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangeOnScreenSliders : MonoBehaviour
{
    public Slider contraste;
    public Slider brillo;

    public static event Action ChangeOnSliders = delegate { };

    public void ChangeSlider()
    {
        PlayerPrefs.SetFloat("Brillo", brillo.value);
        PlayerPrefs.SetFloat("Contraste", contraste.value);
        PlayerPrefs.Save();

        ChangeOnSliders();
    }
    
}
