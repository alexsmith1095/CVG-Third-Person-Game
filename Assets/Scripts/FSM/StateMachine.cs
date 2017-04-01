using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	IState currentState;

	public void ChangeState(IState newState)
	{
	    if (currentState != null)
	        currentState.Exit(); //exit previous state

	    currentState = newState; //assign the next state
	    currentState.Enter(); //enter the next state
	}

	public void Update()
	{
        if (currentState != null)
			currentState.Execute();
	}
}
