using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Delay : IState
{
    private float waitForSeconds;
    private float deadline;

    public EnemyState_Delay(float waitForSeconds)
    {
        this.waitForSeconds = waitForSeconds;
    }

    public void OnEnter()
    {
        deadline = Time.time + waitForSeconds;
    }

    public void OnExit()
    {
        Debug.Log("ES_Delay Exit");
    }

    public void Tick()
    {
    }

    public bool IsDone()
    {
        return Time.time >= deadline;
    }
}
