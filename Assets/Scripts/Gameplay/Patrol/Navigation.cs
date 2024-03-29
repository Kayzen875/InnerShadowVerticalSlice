using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class Navigation : MonoBehaviour
{
    public enum Animations
    {
        none,
        shelf,
        dishes,
        cookingPot,
        door,
        pan,
        potato,
        checkOven,
        meat,
        confused,
        lever
    };

    [System.Serializable]
    public struct NavigationPoints
    {
        public Transform[] points;
        public int[] nextRoutes;
        public float waitingTime;
        public Animations animation;
        public SoundManager.Sound sound;
        public SoundManager.AudioSourcesType type;
        public GameObject[] items;
    }

    [System.Serializable]
    public struct PatrolRoutes
    {
        public NavigationPoints[] routes;
    }

    //NavMesh objectives
    [Header("Navmesh Objectives")]
    public PatrolRoutes patrols;
    public Transform target;
    public Transform oven;
    public Transform[] lights;
    public Transform midPoint;
    public Transform finalMidPoint;
    public Transform dumbWaiter;
    public Transform finalPhasePoint;
    
    //Navmesh variables
    [Header("Navmesh Variables")]
    private NavMeshAgent navmesh;
    private int route;
    private int lastRoute;
    private int point;
    public float speed;

    //Time
    private float waitingTimer;
    private bool waiting;

    //Chase variables
    [Header("Chasing")]
    public float chaseSpeed;
    public float confusionTime;
    public float detectionDistance;
    public bool chaseTest;
    public bool hiddentest;
    private bool resetPosition;
    public bool detected;
    public float grabTimer;
    public bool grabbed;
    public bool stopped;

    //FSM
    private bool seen;

    //Tests
    public bool middlePhase;
    private bool alreadyMiddlePhase;
    private bool finalPhase;
    private bool alreadyFinalPhase;

    //Checks
    [Header("Things to check")]
    public bool ovenOff;
    public bool firstLightsOff;
    public bool secondLightsOff;
    public bool seeFirstLightsOff;
    public bool seeSecondLightsOff;
    private bool seeOvenOff;


    //Animations
    private Animator _anim;
    private string lastAnimation;
    private SoundManager.Sound lastSound;
    private SoundManager.AudioSourcesType lastSoundtype;
    private bool starting;
    public Transform hand;

    [Header("BuildTemporal")]
    public GameObject lever;
    public GameObject[] switches;
    public int spot;

    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

        route = 0;
        point = 0;
        spot = 0;

        starting = true;

        _anim.SetBool("walk", true);
        lastSound = SoundManager.Sound.None;
        lastSoundtype = SoundManager.AudioSourcesType.None;
        SoundManager.PlaySound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
    }

    void Update()
    {
        if(!grabbed && !stopped)
        {
            CheckPlayerDistance();
            CheckOven();
            CheckStarting();
            CheckLightsOff();

            if (chaseTest)
            {
                Chasing();
            }
            else if(seeOvenOff)
            {
                if(navmesh.destination != oven.position && resetPosition)
                {
                    navmesh.destination = oven.position;
                    waiting = false;
                    waitingTimer = 0;

                }
                else if(ovenOff)
                {
                    navmesh.destination = oven.position;
                    waiting = false;
                    waitingTimer = 0;
                    navmesh.speed = speed;
                    _anim.SetBool(lastAnimation, false);
                    SoundManager.PlaySound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
                    _anim.SetBool("walk", true);

                    ovenOff = false;
                }
                else if(navmesh.remainingDistance < 1)
                {
                    SoundManager.PlaySound(SoundManager.Sound.FireOven, SoundManager.SoundType.Ambient, SoundManager.AudioSourcesType.Oven);
                    seeOvenOff = false;
                    _anim.SetBool("walk", false);
                    //SoundManager.StopSound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
                    _anim.SetBool(lastAnimation, false);
                    _anim.SetBool("lever", true);
                    lastAnimation = "lever";
                    lever.GetComponent<SwitchLever>().SwitchesAction();
                    Debug.Log("enciendo el horno");                    
                }
            }
            else if(seeFirstLightsOff || seeSecondLightsOff)
            {
                if(spot == 0 && seeFirstLightsOff)
                {
                    TurnOffLights(0);
                }
                else if(spot == 1 && seeSecondLightsOff)
                {
                    TurnOffLights(1);
                }
                else if(spot == 1 && seeFirstLightsOff)
                {
                    TurnOffLights(0);
                }
                else if(spot == 0 && seeSecondLightsOff)
                {
                    TurnOffLights(1);
                }
            }
            else if(resetPosition && !finalPhase)
            {
                navmesh.destination = midPoint.position;

                if(navmesh.remainingDistance < 1)
                {
                    resetPosition = false;
                    _anim.SetBool("walk", false);
                    //SoundManager.StopSound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
                    _anim.SetBool(lastAnimation, false);
                    _anim.SetBool("confused", true);
                    lastAnimation = "confused";
                    Debug.Log("He vuelto al medio");
                    route = 20;
                    waiting = true;
                    waitingTimer = confusionTime;
                }
            }
            else if(resetPosition && finalPhase)
            {
                navmesh.destination = finalMidPoint.position;

                if (navmesh.remainingDistance < 1)
                {
                    resetPosition = false;
                    _anim.SetBool("walk", false);
                    _anim.SetBool(lastAnimation, false);
                    _anim.SetBool("confused", true);
                    lastAnimation = "confused";
                    Debug.Log("He vuelto al medio");
                    route = 30;
                    waiting = true;
                    waitingTimer = confusionTime;
                }
            }

            if(navmesh.remainingDistance < 1 && !waiting && !chaseTest && !seeOvenOff)
            {
                Debug.Log("He llegado");
                if(!alreadyFinalPhase && alreadyMiddlePhase)
                {
                    alreadyFinalPhase = true;
                }               
                else if (patrols.routes[route].points.Length > point)
                {
                    Debug.Log("ruta actual " + route);
                    Debug.Log("siguiente punto " + point);
                    navmesh.destination = patrols.routes[route].points[point].position;
                    NextPoint();              
                }
                else 
                { 
                    NextAction(); 
                }
            }

            if(waitingTimer >= 0 && waiting)
            {
                waitingTimer -= Time.deltaTime;
                navmesh.speed = 0;
                Debug.Log("sigo esperando ");
            }
            else if(waiting && waitingTimer < 0)
            {
                point = 0;
                Debug.Log("He acabado de esperar " + point);
                waiting = false;
                navmesh.speed = speed;
                NextRoute();
            }
        }
        else if(!stopped)
        {
            if(grabTimer > 0)
            {
                grabTimer -= Time.deltaTime;
                target.position = hand.position;
            }
            else
            {
                stopped = true;
                target.position = hand.position;
                LevelManager.Instance.GameOver(3);
            }
        }
       
    }

    private void Chasing()
    {
        navmesh.destination = target.position;

        if(navmesh.remainingDistance < 0.8f)
        {
            Debug.Log("Atrapado");
            grabbed = true;
            target.GetComponent<PaxFSMController>().FrozePax();
            _anim.SetBool("run", false);
            _anim.SetBool("walk", false);
            _anim.SetBool(lastAnimation, false);
            _anim.SetTrigger("grab");
        }
        else if(hiddentest)
        {
            chaseTest = false;
            _anim.SetBool("run", false);
            _anim.SetBool("walk", true);
            SoundManager.PlaySound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
            waitingTimer = 1;
            resetPosition = true;
        }
        else
        {
            _anim.SetBool(lastAnimation, false);
            _anim.SetBool("walk", false);
            _anim.SetBool("run", true);
            SoundManager.StopSound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
        }
    }
    
    private void NextAction()
    {
        Wait();
    }

    private void NextRoute()
    {
        if (patrols.routes[route].items != null)
        {
            for (int i = 0; i < patrols.routes[route].items.Length; i++)
            {
                patrols.routes[route].items[i].GetComponent<Animator>().SetBool(lastAnimation, false);
            }
        }

        if (!middlePhase || alreadyMiddlePhase)
        {
            int rand = Random.Range(0, patrols.routes[route].nextRoutes.Length);

            route = patrols.routes[route].nextRoutes[rand];
            _anim.SetBool(lastAnimation, false);
            _anim.SetBool("walk", true);
            SoundManager.PlaySound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
        }
        else if(!alreadyMiddlePhase)
        { 
            alreadyMiddlePhase = true;
            route = 29;
            _anim.SetBool(lastAnimation, false);
            _anim.SetBool("walk", true);
            SoundManager.PlaySound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
            Debug.Log("Update middle Phase"); 
        }
    }

    private void NextPoint()
    {
        point++;     
    }

    private void Wait()
    {
        waiting = true;
        _anim.SetBool("walk", false);
        SoundManager.StopSound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
        string enumAnimation = System.Enum.GetName(typeof(Animations),patrols.routes[route].animation);

        if(enumAnimation != "none")
        {
            _anim.SetBool(enumAnimation, true);

            SoundManager.PlaySound(patrols.routes[route].sound, SoundManager.SoundType.NPC, patrols.routes[route].type);            

            if (patrols.routes[route].items != null)
            {
                for (int i = 0; i < patrols.routes[route].items.Length; i++)
                {
                    patrols.routes[route].items[i].GetComponent<Animator>().SetBool(enumAnimation, true);
                }
            }

            lastSound = patrols.routes[route].sound;
            lastSoundtype = patrols.routes[route].type;
            lastAnimation = enumAnimation;
        }
        waitingTimer = patrols.routes[route].waitingTime;
        Debug.Log(enumAnimation);
    }

    public bool OnPlayerSeen()
    {
        return seen;
    }

    public void CheckPlayerDistance()
    {
        if(Vector3.Distance(transform.position, target.position) < detectionDistance && !hiddentest)
        {
            chaseTest = true;
        }
    }

    public void CheckOven()
    {
        if(lastAnimation == "checkOven" && ovenOff)
        {
            seeOvenOff = true;
        }
    }

    public void CheckFrontPlayerDistance()
    {
        if(detected)
        {
            detected = false;
            chaseTest = true;
        }
    }

    public void OnBoxFalling()
    {
        middlePhase = true;
    }

    public void SetHidden(bool parameter)
    {
        hiddentest = parameter;
    }

    public void SetOvenOff()
    {
        ovenOff = true;
    }

    public void CheckLightsOff()
    {
        if(spot == 0 && firstLightsOff)
        {
            seeFirstLightsOff = true;
        }
        else if(spot == 1 && secondLightsOff)
        {
            seeSecondLightsOff = true;
        }
    }

    public void SetLightsOff(int number)
    {
        if(number == 0)
        {
            firstLightsOff = true;
        }
        else if(number == 1)
        {
            secondLightsOff = true;
        }
    }

    public void SetLightsOn(int number)
    {
        if (number == 0)
        {
            if(!firstLightsOff)
            {
                seeFirstLightsOff = false;
                Debug.Log("Alguien ha apagado las luces");
                resetPosition = true;
            }
            else { firstLightsOff = false; }        
        }
        else if (number == 1)
        {
            if (!secondLightsOff)
            {
                seeSecondLightsOff = false;
                Debug.Log("Alguien ha apagado las luces");
                resetPosition = true;
            }
            else { secondLightsOff = false; }
        }
    }

    public void SetSpot(int newSpot)
    {
        spot = newSpot;
    }

    public void BoxHasFallen()
    {
        middlePhase = true;
    }

    public void TurnOffLights(int number)
    {
        if (navmesh.destination != lights[number].position && resetPosition)
            {
                navmesh.destination = lights[number].position;
                waiting = false;
                waitingTimer = 0;

        }
        else if((firstLightsOff && number == 0) || (secondLightsOff && number == 1))
        {
            navmesh.destination = lights[number].position;
            waiting = false;
            waitingTimer = 0;
            navmesh.speed = speed;
            _anim.SetBool(lastAnimation, false);
            SoundManager.PlaySound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
            _anim.SetBool("walk", true);

            if(number == 0)
            {
                firstLightsOff = false;
            }
            else if(number == 1)
            {
                secondLightsOff = false;
            }      
        }
        else if (navmesh.remainingDistance < 1)
        {
            if(number == 0)
            {
                seeFirstLightsOff = false;
            }
            else if(number == 1)
            {
                seeSecondLightsOff = false;
            }
            _anim.SetBool(lastAnimation, false);
            _anim.SetBool("walk", false);
            //SoundManager.StopSound(SoundManager.Sound.CookSteps, SoundManager.SoundType.NPC, SoundManager.AudioSourcesType.Cook);
            _anim.SetTrigger("switch");
            Debug.Log("enciendo las luces");
            switches[number].GetComponent<SwitchButton>().SwitchesAction();
            resetPosition = true;
        }
    }

    private void CheckStarting()
    {
        if(starting && lastAnimation == "lever")
        {
            starting = false;
            lever.GetComponent<SwitchLever>().SwitchesAction();
        }
    }

    public void FinalPhase()
    {
        finalPhase = true;
    }
}