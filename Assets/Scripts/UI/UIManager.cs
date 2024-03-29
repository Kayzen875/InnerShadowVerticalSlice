using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class UIManager : MonoBehaviour
{
    public Transform mainMenu;
    public Transform settingsMenu;
    public Transform pauseMenu;
    public Transform gameOver;
    public Transform thanksScreen;

    public Transform generalInfo;
    public Transform audioInfo;
    public Transform controlsInfo;
    public Transform keyboardControls;
    public Transform controllerControls;
    public Transform scrollKeyboard;
    public Transform scrollController;

    [Header("Percentages")]
    public Transform brightPercentage;
    public Transform contrastPercentage;
    public Transform masterPercentage;
    public Transform musicPercentage;
    public Transform ambientPercentage;


    [Header("Loading Screen")]
    public Transform ToGame1;
    public Transform ToGame2;
    public Transform ToGame3;
    
    [Header("Screen settings")]
    public Transform valorBrillo;
    public Transform valorContraste;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider ambientSlider;

    public static bool setSettings;

    public AudioMixer auidoMixer;

    /*INDEX
     
    0 - Main
    1 - Settings
    2 - Pause
    3 - Game Over
     
    */

    Slider valorBrilloC;
    Slider valorContrasteC;
    bool generalSettings;

    private int menuIndex;


    private void OnEnable()
    {
        mainMenu.gameObject.SetActive(true);
        generalInfo.gameObject.SetActive(false);
        audioInfo.gameObject.SetActive(false);
        controlsInfo.gameObject.SetActive(false);
        keyboardControls.gameObject.SetActive(false);
        controllerControls.gameObject.SetActive(false);
        scrollKeyboard.gameObject.SetActive(false);
        scrollController.gameObject.SetActive(false);
        thanksScreen.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        menuIndex = 0;
        //mainMenu.gameObject.SetActive(true);
        generalInfo.gameObject.SetActive(false);
        audioInfo.gameObject.SetActive(false);
        controlsInfo.gameObject.SetActive(false);
        keyboardControls.gameObject.SetActive(false);
        controllerControls.gameObject.SetActive(false);
        scrollKeyboard.gameObject.SetActive(false);
        scrollController.gameObject.SetActive(false);

        valorBrilloC = valorBrillo.GetComponent<Slider>();
        valorContrasteC = valorContraste.GetComponent<Slider>();

        if(setSettings)
        {
            //ARREGLANDO PEDRO//

            valorBrilloC.value = PlayerPrefs.GetFloat("Brillo");
            valorContrasteC.value = PlayerPrefs.GetFloat("Contraste");

            masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeValue");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeValue");
            ambientSlider.value = PlayerPrefs.GetFloat("AmbientVolumeValue");

;
            brightPercentage.GetComponent<Percentage>().textUpdate(valorBrilloC.value);
            contrastPercentage.GetComponent<Percentage>().textUpdate(valorContrasteC.value);
            masterPercentage.GetComponent<Percentage>().textUpdate(masterSlider.value);
            musicPercentage.GetComponent<Percentage>().textUpdate(musicSlider.value);
            ambientPercentage.GetComponent<Percentage>().textUpdate(ambientSlider.value);
        }


        if (!setSettings)
        {
            SetSettings();
            setSettings = true;
        }
    }

    public void ExitMenu(int index)
    {
        if (index == 0)
        {
            mainMenu.gameObject.SetActive(false);
        }
        else if (index == 1)
        {
            settingsMenu.gameObject.SetActive(false);
        }
        else if (index == 2)
        {
            pauseMenu.gameObject.SetActive(false);
        }
        else if (index == 3)
        {
            gameOver.gameObject.SetActive(false);
        }
        else if (index == 4)
        {
            ToGame1.gameObject.SetActive(false);
        }
        else if (index == 5)
        {
            thanksScreen.gameObject.SetActive(false);
        }

        menuIndex = index;
    }

    public void EnterMenu(int index)
    {        
        if (index == 0)
        {
            mainMenu.gameObject.SetActive(true);
        }
        else if (index == 1)
        {

            settingsMenu.gameObject.SetActive(true);
        }
        else if (index == 2)
        {
            pauseMenu.gameObject.SetActive(true);
        }
        else if (index == 3)
        {
            gameOver.gameObject.SetActive(true);
        }
        else if (index == 4)
        {
            ExitMenu(0);
            ToGame2.gameObject.SetActive(true);
        }
        else if (index == 5)
        {
            thanksScreen.gameObject.SetActive(true);
        }
        else if (index == 6)
        {
            ToGame3.gameObject.SetActive(true);
        }
        else if (index == 7)
        {
            ToGame1.gameObject.SetActive(true);
        }

        menuIndex = index;
    }

    public void OnMainMenu()
    {
        if (LevelManager.Instance.pauseScreen)
        {
            LevelManager.Instance.BackMainMenu();
            ExitMenu(2);
        }
        else
        {
            ExitMenu(menuIndex);
            EnterMenu(0);
        }
    }

    public void OnNewGame()
    {
        ExitMenu(0);
        LevelManager.Instance.StartGame();
        valorBrilloC.value = PlayerPrefs.GetFloat("Brillo");
        valorContrasteC.value = PlayerPrefs.GetFloat("Contraste");
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeValue");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeValue");
        ambientSlider.value = PlayerPrefs.GetFloat("AmbientVolumeValue");
    }

    public void ReloadScene(int index)
    { 
        Time.timeScale = 1;
        LevelManager.Instance.ReloadScene(index);
    }

    public void OnSettingsMenu()
    {
        ExitMenu(menuIndex);
        LevelManager.Instance.UINavegation();
        EnterMenu(1);
    }

    public void OnGeneralInfo()
    {
        generalInfo.gameObject.SetActive(true);
        generalSettings = true;

        valorBrilloC.value = PlayerPrefs.GetFloat("Brillo");
        valorContrasteC.value = PlayerPrefs.GetFloat("Contraste");

        audioInfo.gameObject.SetActive(false);
        controlsInfo.gameObject.SetActive(false);
        keyboardControls.gameObject.SetActive(false);
        controllerControls.gameObject.SetActive(false);
    }

    public void LastMenu()
    {
        EnterMenu(menuIndex);
    }

    public void OnAudioInfo()
    {
        audioInfo.gameObject.SetActive(true);
        generalSettings = false;

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolumeValue");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeValue");
        ambientSlider.value = PlayerPrefs.GetFloat("AmbientVolumeValue");

        generalInfo.gameObject.SetActive(false);
        controlsInfo.gameObject.SetActive(false);
        keyboardControls.gameObject.SetActive(false);
        controllerControls.gameObject.SetActive(false);
    }

    public void OnControlsInfo()
    {
        controlsInfo.gameObject.SetActive(true);
        keyboardControls.gameObject.SetActive(true);
        scrollKeyboard.gameObject.SetActive(true);
        generalSettings = false;

        generalInfo.gameObject.SetActive(false);
        audioInfo.gameObject.SetActive(false);
        controllerControls.gameObject.SetActive(false);
    }

    public void OnKeyboard()
    {
        controlsInfo.gameObject.SetActive(true);
        keyboardControls.gameObject.SetActive(true);
        scrollKeyboard.gameObject.SetActive(true);
        generalSettings = false;

        generalInfo.gameObject.SetActive(false);
        audioInfo.gameObject.SetActive(false);
        controllerControls.gameObject.SetActive(false);
    }

    public void OnController()
    {
        controlsInfo.gameObject.SetActive(true);
        keyboardControls.gameObject.SetActive(true);
        controllerControls.gameObject.SetActive(true);
        generalSettings = false;

        generalInfo.gameObject.SetActive(false);
        audioInfo.gameObject.SetActive(false);
        scrollKeyboard.gameObject.SetActive(false);
    }

    public void OnBackButton()
    {
        if (LevelManager.Instance.pauseScreen)
        {
            LevelManager.Instance.unUINavegation();
            ExitMenu(1);
        }
        else
        {
            LevelManager.Instance.unUINavegation();
            OnMainMenu();
        }
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnResumeGame()
    {
        LevelManager.Instance.ResumeGame();
    }

    public void SetSettings()
    {
        PlayerPrefs.SetFloat("MasterVolumeValue", masterSlider.value);
        PlayerPrefs.SetFloat("MusicVolumeValue", musicSlider.value);
        PlayerPrefs.SetFloat("AmbientVolumeValue", ambientSlider.value);

        PlayerPrefs.SetFloat("Brillo", valorBrilloC.value);
        PlayerPrefs.SetFloat("Contraste", valorContrasteC.value);
        PlayerPrefs.Save();

        auidoMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolumeValue"));
        auidoMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolumeValue"));
        auidoMixer.SetFloat("AmbientVolume", PlayerPrefs.GetFloat("AmbientVolumeValue"));

        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
    }

    public void MasterSliderValue()
    {       
        auidoMixer.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolumeValue", masterSlider.value);
        PlayerPrefs.Save();
    }

    public void MusicSliderValue()
    {
        auidoMixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolumeValue", musicSlider.value);
        PlayerPrefs.Save();
    }

    public void AmbientSliderValue()
    {
        auidoMixer.SetFloat("AmbientVolume", ambientSlider.value);
        PlayerPrefs.SetFloat("AmbientVolumeValue", ambientSlider.value);
        PlayerPrefs.Save();
    }

    public void ResetMenus()
    {
        mainMenu.gameObject.SetActive(false);
        generalInfo.gameObject.SetActive(false);
        audioInfo.gameObject.SetActive(false);
        controlsInfo.gameObject.SetActive(false);
        keyboardControls.gameObject.SetActive(false);
        controllerControls.gameObject.SetActive(false);
        scrollKeyboard.gameObject.SetActive(false);
        scrollController.gameObject.SetActive(false);
    }
}
