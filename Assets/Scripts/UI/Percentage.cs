using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Percentage : MonoBehaviour
{
    private float minValue;
    private float maxValue;
    public Text percentageText;
    Text savedPercetage;
    public Slider slider;
    bool started;
    void Start()
    {
        percentageText = GetComponent<Text>();
        started = true;
    }

    public void textUpdate(float value)
    {
        minValue = slider.minValue;
        maxValue = slider.maxValue;
        float range = maxValue - minValue;
        float correctedStartValue = value - minValue;
        float percentage = (correctedStartValue * 100) / range;


        percentageText.text = (int)percentage + "%";

    }

}
