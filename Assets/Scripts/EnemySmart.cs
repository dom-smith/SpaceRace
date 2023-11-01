using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmart : MonoBehaviour
{
    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;

    private void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
        stateMachine = new StateMachine();

        CoverArea coverArea = FindObjectOfType<CoverArea>();

        // STATES
        var runToCover = new EnemyState_RunToCover(enemyReferences, coverArea);
        var delayAfterRun = new EnemyState_Delay(2f);
        var cover = new EnemyState_Cover(enemyReferences);

        At(runToCover, delayAfterRun, () => runToCover.HasArrivedAtDestination());
        At(delayAfterRun, cover, () => delayAfterRun.IsDone());

        stateMachine.SetState(runToCover);

        // CONDITIONS
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);
    }

    private void Update()
    {
        stateMachine.Tick();

    }
}
