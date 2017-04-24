using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbFollowState : IState {

	OrbAspect owner;

    public OrbFollowState(OrbAspect owner) { this.owner = owner; }

    private UnityEngine.AI.NavMeshAgent agent;
    private Transform player;

	//executed upon entry of the state
    public void Enter()
    {
		// Code for animator > walk here

		agent = owner.GetComponent<UnityEngine.AI.NavMeshAgent>();
		player = GameObject.FindWithTag("Player").transform;
    }

	//executed throughout
    public void Execute()
    {
		Vector3 direction = (player.position - owner.transform.position).normalized;
		float distance = Vector3.Distance(player.position, owner.transform.position);

        agent.updateRotation = false;
		Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        owner.transform.rotation = rotation;

        if(distance > agent.stoppingDistance)
			agent.destination = player.position;
		else if(distance < agent.stoppingDistance)
            agent.destination -= direction;
    }

	//executed upon exit of the state
    public void Exit()
    {
        Debug.Log("Orb Exited Follow state");
    }
}
