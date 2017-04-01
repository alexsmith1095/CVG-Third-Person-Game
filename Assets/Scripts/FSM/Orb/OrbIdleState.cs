using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class OrbIdleState : IState {

	OrbAspect owner;

    public OrbIdleState(OrbAspect owner) { this.owner = owner; }

	//executed upon entry of the state
    public void Enter()
    {
        Debug.Log("Entering Base state");

		// code for animator > idle here
    }

	//executed throughout
    public void Execute()
    {
		Collider[] hitColliders = Physics.OverlapSphere(owner.transform.position, 6f);
		foreach(Collider col in hitColliders) {
			if(col.gameObject.CompareTag("Player"))
				owner.stateMachine.ChangeState(new OrbFollowState(owner));
        }
    }

	//executed upon exit of the state
    public void Exit()
    {
        Debug.Log("Exiting Base state");
    }
}
