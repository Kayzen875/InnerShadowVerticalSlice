using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushAction : MonoBehaviour
{
    public LayerMask pushLayer;
    public float raycastLength;
    private RaycastHit hit;
    public Transform raycastStart;
    public PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        playerInput.actions["Pull&Push"].performed += PushBox;
    }
    void OnDisable()
    {
        playerInput.actions["Pull&Push"].performed -= PushBox;
    }


    private void PushBox(InputAction.CallbackContext context)
    {
        if(Physics.Raycast(raycastStart.position, raycastStart.forward, out hit, raycastLength, pushLayer))
        {
            Debug.Log("prueba");
            hit.collider.gameObject.GetComponent<InteractBox2D>().AddRigidBody();
            hit.collider.gameObject.layer = LayerMask.NameToLayer("MovableBox");
        }
    }

}
