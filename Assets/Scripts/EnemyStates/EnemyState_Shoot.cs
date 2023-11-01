using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Shoot : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;

    public EnemyState_Shoot(EnemyReferences enemyReferences)
    {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void OnExit()
    {
        enemyReferences.animator.SetBool("Shooting", false);
        target = null;
    }

    public void Tick()
    {
        if (target != null)
        {
            Vector3 lookPosition = target.position - enemyReferences.transform.position;
            lookPosition.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPosition);
            enemyReferences.transform.rotation = Quaternion.Slerp(enemyReferences.transform.rotation, rotation, 0.2f);

            enemyReferences.animator.SetBool("Shooting", true);
        }
    }
}
