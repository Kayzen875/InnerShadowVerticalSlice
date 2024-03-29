using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Cocinera/Action/CocineraActionSwitchesUpdate")]
public class CocineraActionSwitchesUpdate : FSM.Action
{
    public override void Act(Controller controller)
    {
        CocineraController c = (CocineraController)controller;
    }
}
