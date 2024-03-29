using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchAction : MonoBehaviour
{
    public PlayerInput playerInput;
    public Animator _animPax;
    public Animator _animShadow;
    public float cooldown;

    [Header("raycast")]
    public Transform raycastStart;
    public float length;
    public LayerMask switchLayer;
    public LayerMask leverLayer;
    private RaycastHit hit;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        playerInput.actions["Switches"].performed += SwitchesActivation;
    }

    void OnDisable()
    {
        playerInput.actions["Switches"].performed -= SwitchesActivation;
    }

    private void SwitchesActivation(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(raycastStart.position, transform.forward, out hit, length, switchLayer))
        {
            _animPax.SetTrigger("switch");
            SoundManager.PlaySound(SoundManager.Sound.Switch, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.Switch);
            hit.collider.GetComponent<SwitchButton>().SwitchesAction();
            //GetComponent<PaxFSMController>().FrozeTimer(0.4f);
        }
        else if(Physics.Raycast(raycastStart.position, transform.forward, out hit, length, leverLayer) && GetComponent<PaxFSMController>().shadowForm)
        {
            _animShadow.SetTrigger("lever");
            SoundManager.PlaySound(SoundManager.Sound.Lever, SoundManager.SoundType.Pax, SoundManager.AudioSourcesType.Lever);
            hit.collider.GetComponent<SwitchLever>().SwitchesAction();
            //GetComponent<PaxFSMController>().FrozeTimer(0.4f);
        }
        else { Debug.Log("No he encontrado nada"); }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(raycastStart.position, raycastStart.position + transform.forward * length);
    }
    //HAY QUE TENER CUIDADO CON EL TRANSFORM.FORWARD
}
