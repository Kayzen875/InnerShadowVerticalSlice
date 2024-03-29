using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetQualityLevel : MonoBehaviour
{
    public Dropdown dropdown;

    void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
        dropdown.value = 2;
    }

    public void UpdateSettings(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
