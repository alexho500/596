using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState  != null){
            activeState.Perform();
        }
    }

    public void Initialize(BaseState initialState){
        ChangeState(initialState);
    }
     
    public void ChangeState(BaseState newState)
    {
        // check activeState != null
        if(activeState != null)
        {
            //run cleanup on activeState
            activeState.Exit();
        }

        //change to new state
        activeState = newState;

        //fail-safe null check
        if(activeState != null)
        {
            /// setup new state
            activeState.StateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }

    }
}
