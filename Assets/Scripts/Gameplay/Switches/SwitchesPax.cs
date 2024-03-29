using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchesPax : MonoBehaviour
{
    protected float cooldown;
    protected bool activated;

    public abstract void SwitchesAction();

    virtual public void Update()
    {
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }

    }

    public virtual void swapState()
    {
        activated = !activated;
    }
}
