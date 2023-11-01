using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Reload : IState
{
    private EnemyReferences enemyReferences;

    public EnemyState_Reload(EnemyReferences enemyReferences)
    {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter()
    {
        enemyReferences.animator.SetFloat("Cover", 1);
        enemyReferences.animator.SetTrigger("Reload");
    }

    public void OnExit()
    {
        enemyReferences.animator.SetFloat("Cover", 0);
    }

    public void Tick()
    {
    }
}
