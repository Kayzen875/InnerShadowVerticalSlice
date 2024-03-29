using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
            }

            return _instance;
        }
    }

    public bool loadedScene;
    public bool loadingScreen;
    public bool pauseScreen;
    public bool endgameScreen;
    public bool navegation;
    public bool mainMenu;
    private GameObject UiManager;
    private UIManager uiManagerC;
    public bool gameOver;

    private GameObject character;
    public int secondsToLoad;

    public int whatIndex;
    int tryAgainScene;

    private void Awake()
    {
        if (_instance == null)
        {
            GameObject.DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }

    }

    void OnEnable()
    {
        GetComponent<PlayerInput>().actions["Escape"].performed += Pause;
    }

    void OnDisable()
    {
        GetComponent<PlayerInput>().actions["Escape"].performed -= Pause;
    }

    public void LoadAsyncScene(int index)
    {
        StartCoroutine(LoadingScreen(index));
    }

    public void GameOver(int scene)
    {
        tryAgainScene = scene;
        gameOver = true;
        StartCoroutine(loadUIScene());
    }

    public void EndGameScene()
    {
        endgameScreen = true;
        StartCoroutine(loadUIScene());
    }

    private void Update()
    {
        // UI Scene
        if (loadedScene)
        {
            UiManager = GameObject.FindGameObjectWithTag("UIManager");
            if(UiManager)
            {
                if (UiManager.TryGetComponent(out UIManager manager))
                {
                    uiManagerC = manager;
                    uiManagerC.GetComponent<UIManager>().ExitMenu(0);
                }
            }


            if (!navegation && pauseScreen && UiManager)
            {
                Debug.Log("asdas");
                UiManager.GetComponent<UIManager>().EnterMenu(2);
            }
            
            if (gameOver && !navegation && UiManager)
            {
                UiManager.GetComponent<UIManager>().EnterMenu(3);
                Time.timeScale = 0;
                //character.GetComponent<PaxFSMController>().FrozePax();
            }

            if (loadingScreen && !navegation && UiManager)
            {
                if(whatIndex == 2)
                {
                    UiManager.GetComponent<UIManager>().EnterMenu(4);
                }
                else if(whatIndex == 3)
                {
                    UiManager.GetComponent<UIManager>().EnterMenu(6);
                }
                else if(whatIndex == 1)
                {
                    UiManager.GetComponent<UIManager>().EnterMenu(7);
                }
                
                
                //character.GetComponent<PaxFSMController>().FrozePax();
            }

            if (endgameScreen && !navegation && UiManager)
            {
                UiManager.GetComponent<UIManager>().EnterMenu(5);
                endgameScreen = false;
                UiManager.GetComponent<UIManager>().ExitMenu(2);
                //character.GetComponent<PaxFSMController>().FrozePax();
            }          
        }
    }

    void Pause(InputAction.CallbackContext context)
    {

        if (SceneManager.sceneCount > 1 && (SceneManager.GetSceneByBuildIndex(1) != null || SceneManager.GetSceneByBuildIndex(2) != null || SceneManager.GetSceneByBuildIndex(3) != null) && SceneManager.GetActiveScene().buildIndex != 0 && !pauseScreen)
        {
            SceneManager.UnloadSceneAsync(0);
            Time.timeScale = 1;
            loadedScene = false;
            pauseScreen = false;
            gameOver = false;
        }
        else if(SceneManager.GetActiveScene().buildIndex != 0 && !pauseScreen)
        {             
            pauseScreen = true;                
            StartCoroutine(loadUIScene());               
            Time.timeScale = 0;               
        }
        
    }
    
    IEnumerator loadUIScene()
    {
        loadedScene = false;
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        yield return null;
        loadedScene = true;
        GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    IEnumerator LoadingScreen(int index)
    {
        Scene actualScene = SceneManager.GetActiveScene();

        GC.Collect();
        Resources.UnloadUnusedAssets();


        if (secondsToLoad != 0)
        {
            whatIndex = index;
            loadingScreen = true;
            StartCoroutine(loadUIScene());
            yield return new WaitForSeconds(secondsToLoad);
        }

        loadingScreen = false;
        loadedScene = false;

        var sceneToLoad = SceneManager.LoadSceneAsync(index);

        while (!sceneToLoad.isDone)
        {
            yield return sceneToLoad;
        }
        
        SceneManager.UnloadSceneAsync(actualScene);
        //character.GetComponent<PaxFSMController>().UnFrozePax();
    }

    IEnumerator LoadingGame()
    {
        Scene actualScene = SceneManager.GetActiveScene();
        var sceneToLoad = SceneManager.LoadSceneAsync("InitialCinematic");

        while (!sceneToLoad.isDone)
        {
            yield return sceneToLoad;
        }

        //SceneManager.UnloadSceneAsync(actualScene);
        GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    public void StartGame()
    {
        StartCoroutine(LoadingGame());
    }

    public void ReloadScene(int index)
    {
        if(tryAgainScene == 2)
        {
            gameOver = false;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(2);
            GC.Collect();
            Resources.UnloadUnusedAssets();
        }

        if(tryAgainScene == 3)
        {
            gameOver = false;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(3);
            GC.Collect();
            Resources.UnloadUnusedAssets();
        }
        
        
    }

    public void BackMainMenu()
    {
        loadedScene = false;
        pauseScreen = false;
        Time.timeScale = 1;
        Scene actualScene = SceneManager.GetActiveScene();        
        SceneManager.LoadScene("UI");
        SceneManager.UnloadSceneAsync(actualScene);    
    }

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync(0);
        Time.timeScale = 1;
        loadedScene = false;
        pauseScreen = false;                
    }

    public void UINavegation()
    {
        navegation = true;
    }

    public void unUINavegation()
    {
        navegation = false;
    }
}