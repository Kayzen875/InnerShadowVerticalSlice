using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(menuName = "FSM/Cocinera/Decision/CocineraDecisionPersecutionToDistracted")]
public class CocineraDecisionPersecutionToDistracted : Decision
{
    public override bool Decide(Controller controller)
    {
        CocineraController c = (CocineraController)controller;

        return true; // Borrar
    }
}

