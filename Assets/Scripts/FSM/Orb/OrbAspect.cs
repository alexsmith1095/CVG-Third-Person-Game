using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAspect : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();

    void Start()
    {
        stateMachine.ChangeState(new OrbFollowState(this));
    }

    void Update()
    {
        stateMachine.Update();
    }
}
