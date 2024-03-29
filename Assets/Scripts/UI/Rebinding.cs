using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Rebinding : MonoBehaviour
{
    public Text text;
    public GameObject rebindingMessage;
    public GameObject textHolder;
    public PlayerInput playerInput;
    public String actionName;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void RebindingProcess()
    {
        rebindingMessage.SetActive(true);
        textHolder.SetActive(false);

        //rebindingOperation = playerInput.actions.[actionName].PerformInteractiveRebinding().WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete().Start);
    }

    public void RebindComplete()
    {
        //int bindingIndex = playerInput.actions.GetBindingIndexForControl(playerInput.actions.controls[0]);
        
        //bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(playerInput.actions.bindings[0].effectivePath, InputControlPath.HumanReadableString.OmitDevice);

        rebindingOperation.Dispose();

        rebindingMessage.SetActive(false);
        textHolder.SetActive(true);
    }
}
