using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public  class Controller : MonoBehaviour
    {

        public State currentState;
        public State remainState;
        
       
        virtual public void Start()
        {
            Debug.Log("ControllerStart");

            if (currentState != null) { currentState.EnterState(this); }
        }


        private Animator _animatorController;

        private void Awake()
        {
            _animatorController = GetComponent<Animator>();
        }

        public void SetAnimationLayerWeight(int layer, float weight)
        {
            _animatorController.SetLayerWeight(layer, weight);
        }

        public void SetAnimationBool(string animation, bool value)
        {
            _animatorController.SetBool(animation, value);
        }

        public void SetAnimationFloat(string animation, float value)
        {
            _animatorController.SetFloat(animation, value);
        }

        public void SetAnimationInteger(string animation, int value)
        {
            _animatorController.SetInteger(animation, value);
        }

        public void SetAnimationTrigger(string name)
        {
            _animatorController.SetTrigger(name);
        }


        virtual public void Update()
        {
            if(currentState != null) { currentState.UpdateState(this); }

        }

        public void Transition(State nextState)
        {
            if (nextState != remainState)
            {
                Debug.Log("Transition: Current state is " + currentState);
                if(currentState != null) { currentState.ExitState(this); }
                if(nextState!= null) { nextState.EnterState(this); }

                currentState = nextState;
            }
        }


    }

}


