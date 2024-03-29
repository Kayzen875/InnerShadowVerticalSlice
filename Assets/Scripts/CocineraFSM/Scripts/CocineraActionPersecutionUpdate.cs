using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Cocinera/Action/CocineraActionPersecutionUpdate")]
public class CocineraActionPersecutionUpdate : FSM.Action
{
    public override void Act(Controller controller)
    {
        CocineraController c = (CocineraController)controller;
    }
}
