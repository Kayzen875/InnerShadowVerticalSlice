using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Cocinera/Decision/CocineraDecisionSwitchesToPatrol")]
public class CocineraDecisionSwitchesToPatrol : Decision
{
    public override bool Decide(Controller controller)
    {
        CocineraController c = (CocineraController)controller;

        return true; // Borrar
    }
}
