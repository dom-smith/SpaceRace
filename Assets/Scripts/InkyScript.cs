using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInky : MonoBehaviour
{
    public Transform target; // Player's position
    public Transform blinky; // Reference to Blinky or another enemy ghost

    [HideInInspector] public EnemyReferences enemyReferences;
    [HideInInspector] public float pathUpdateDeadline;
    private float shootingDistance;

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        shootingDistance = enemyReferences.navMeshAgent.stoppingDistance;
    }

    void Update()
    {
        if (target != null && blinky != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }

            enemyReferences.animator.SetBool("Shooting", inRange);
        }
        enemyReferences.animator.SetFloat("Speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    private void LookAtTarget()
    {
        Vector3 lookAtPosition = target.position - transform.position;
        lookAtPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookAtPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;

            // Calculate the target position for Inky based on Blinky's position
            Vector3 blinkyPosition = blinky.position;
            Vector3 offset = (blinkyPosition - target.position) * 2; // Adjust this factor as needed

            Vector3 finalTarget = target.position + offset;

            enemyReferences.navMeshAgent.SetDestination(finalTarget);
        }
    }
}
