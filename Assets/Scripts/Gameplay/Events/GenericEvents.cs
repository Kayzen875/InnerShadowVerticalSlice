using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenericEvents : MonoBehaviour
{
    public enum EventCommandId
    {
        openLight,
        closeLight,
        wait,
        spamLight,
        transformShadow3D,
        transformNormalPax,
        swapSwitchState,
        endEvent,
        stopParticles,
        playParticles,
        alertCookOvens,
        alertCookLightsOff,
        transformNormalPaxKitchen,
        alertCookLightsOn,
        popUps,
        setActiveFalse,
        setActiveTrue
    };

    [System.Serializable]
    public struct EventCommands
    {
        public EventCommandId id;
        public string parameter;
    };

    [System.Serializable]
    public struct EventSequence
    {
        public EventCommands[] commands;
    };

    [Header("Event")]
    public EventSequence[] sequence;

    [Header("Objects")]
    public GameObject[] objects;

    [Header("Pax")]
    public GameObject character;

    [Header("Cook")]
    public GameObject cook;

    //private

    private int commandIndex;
    private bool executingEvent;
    private float waitingTimer;
    private bool waiting;

    //sequenceIndex
    private int index;

    private void Start()
    {
        executingEvent = false;
        commandIndex = 0;
    }

    private void Update()
    {
        if (executingEvent)
        {
            if (waiting)
            {
                if (waitingTimer <= 0)
                {
                    waiting = false;
                    commandIndex++;
                }
                else
                {
                    waitingTimer -= Time.deltaTime;
                }
            }
            else if (commandIndex < sequence[index].commands.Length)
            {
                EventCommands command = sequence[index].commands[commandIndex];

                if (command.id == EventCommandId.openLight)
                {
                    objects[Int32.Parse(command.parameter)].GetComponent<Light>().enabled = true;
                    commandIndex++;
                }
                else if (command.id == EventCommandId.closeLight)
                {
                    objects[Int32.Parse(command.parameter)].GetComponent<Light>().enabled = false;
                    commandIndex++;
                }
                else if (command.id == EventCommandId.wait)
                {
                    waiting = true;
                    waitingTimer = Single.Parse(command.parameter);
                    commandIndex++;
                }
                else if (command.id == EventCommandId.spamLight)
                {
                    objects[Int32.Parse(command.parameter)].GetComponent<LightsInteractions>().startSpam();
                    commandIndex++;
                }
                else if (command.id == EventCommandId.transformShadow3D)
                {
                    character.GetComponent<PaxFSMController>().FrozePax();
                    character.GetComponent<PaxFSMController>().ShadowTransform();
                    commandIndex++;
                }
                else if (command.id == EventCommandId.transformNormalPax)
                {
                    character.GetComponent<PaxFSMController>().ShadowUnTransform();
                    commandIndex++;
                }
                else if (command.id == EventCommandId.swapSwitchState)
                {
                    objects[Int32.Parse(command.parameter)].GetComponent<SwitchButton>().swapState();
                    commandIndex++;
                }
                else if (command.id == EventCommandId.endEvent)
                {
                    commandIndex = 0;
                    executingEvent = false;
                }
                else if (command.id == EventCommandId.stopParticles)
                {
                    objects[Int32.Parse(command.parameter)].GetComponent<ParticleSystem>().Stop();
                    commandIndex++;
                }
                else if (command.id == EventCommandId.playParticles)
                {
                    objects[Int32.Parse(command.parameter)].GetComponent<ParticleSystem>().Play();
                    commandIndex++;
                }
                else if (command.id == EventCommandId.alertCookOvens)
                {
                    cook.GetComponent<Navigation>().SetOvenOff();                    
                    commandIndex++;
                }
                else if (command.id == EventCommandId.alertCookLightsOff)
                {
                    cook.GetComponent<Navigation>().SetLightsOff(Int32.Parse(command.parameter));
                    commandIndex++;
                }
                else if (command.id == EventCommandId.transformNormalPaxKitchen)
                {
                    //se necesita ya que la cocinera puede apagar las luces y no deberia destransformar a pax si esta en otra sala
                    if (KitchenSpotsManager.whereIsPax == Int32.Parse(command.parameter))
                    {
                        character.GetComponent<PaxFSMController>().ShadowUnTransform();
                    }
                    commandIndex++;
                }
                else if (command.id == EventCommandId.alertCookLightsOn)
                {
                    cook.GetComponent<Navigation>().SetLightsOn(Int32.Parse(command.parameter));
                    commandIndex++;
                }
                else if(command.id == EventCommandId.popUps)
                {
                    objects[Int32.Parse(command.parameter)].GetComponent<Animator>().SetBool("popUp", true);
                    commandIndex++;
                }
                else if(command.id == EventCommandId.setActiveFalse)
                {
                    objects[Int32.Parse(command.parameter)].SetActive(false);
                    commandIndex++;
                }
                else if(command.id == EventCommandId.setActiveTrue)
                {
                    objects[Int32.Parse(command.parameter)].SetActive(true);
                    commandIndex++;
                }
                else
                {
                    Debug.LogWarning("Algo ha salido mal en el GenericEvents de " + gameObject.name);
                }
            }
            //else
            //{
            //    executingEvent = false;
            //}
        }
    }

    public void TriggerEvents(int i)
    {
        index = i;
        commandIndex = 0;
        executingEvent = true;
    }
}