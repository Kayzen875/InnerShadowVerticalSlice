using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBox2D : MonoBehaviour
{
    private Rigidbody _rb;
    private bool pushed;
    public Quaternion boxRotation;
    public float raycastLength;
    public float pushSpeed;
    public LayerMask floorLayer;

    public bool triggerBox;
    public GameObject cook;

    InteractiveBox interactiveBoxC;

    public void AddRigidBody()
    {
        gameObject.AddComponent<Rigidbody>();
        _rb = GetComponent<Rigidbody>();
        interactiveBoxC = GetComponent<InteractiveBox>();
        pushed = true;        
    }

    private void Update()
    {
        if (pushed)
        {
            if (Physics.Raycast(transform.position, -transform.up, raycastLength, floorLayer) || Physics.Raycast(transform.position, transform.up, raycastLength, floorLayer)
                || Physics.Raycast(transform.position, -transform.right, raycastLength, floorLayer) || Physics.Raycast(transform.position, transform.right, raycastLength, floorLayer))
            {
                RemoveRigidBody();
                pushed = false;
                transform.rotation = boxRotation;
                if(triggerBox)
                {
                    cook.GetComponent<Navigation>().BoxHasFallen();
                    triggerBox = false;
                }
            }
            else
            {
                _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, pushSpeed);
            }
        }
    }

    void RemoveRigidBody()
    {
        _rb = null;
        Invoke("ActivateScript", 1);
        gameObject.layer = LayerMask.NameToLayer("MovableBox");
        Destroy(GetComponent<Rigidbody>());
    }

    void ActivateScript()
    {
        interactiveBoxC.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.forward * raycastLength);
    }
}
