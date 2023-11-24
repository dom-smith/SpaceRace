using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPinky : MonoBehaviour
{
    public Transform target;
    [HideInInspector] public EnemyReferences enemyReferences;
    [HideInInspector] public float pathUpdateDeadline;
    private float shootingDistance;
    private float ambushDistance = 5f; // Adjust this distance as needed

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
        if (target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                if (IsPlayerMoving())
                {
                    UpdateAmbushPath(); // Pinky's behavior to ambush the player
                }
                else
                {
                    UpdatePath();
                }
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
        private void UpdatePath()
        {
            if (Time.time >= pathUpdateDeadline)
            {
                pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;

                // Calculate the distance between Clyde and the player
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);

                // Set Clyde's destination differently based on the distance to the player
                if (distanceToPlayer > shootingDistance * 2)
                {
                    // If the player is far away, move Clyde towards a random corner or a random location
                    Vector3 randomDestination = GetRandomCorner(); // Implement a method to get a random corner
                    enemyReferences.navMeshAgent.SetDestination(randomDestination);
                }
                else
                {
                    // If the player is within shooting distance, move Clyde towards the player
                    enemyReferences.navMeshAgent.SetDestination(target.position);
                }
            }
        }

        // Implement a method to get a random corner within your game grid
        private Vector3 GetRandomCorner()
        {
            // Replace this with your logic to determine random corners or locations
            // Example: 
            Vector3 randomCorner = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            return randomCorner;
        }

    }

    private void UpdateAmbushPath()
    {
        // Calculate a position ahead of the player's direction
        Vector3 ambushPosition = target.position + target.forward * ambushDistance;

        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMeshAgent.SetDestination(ambushPosition);
        }
    }

    private bool IsPlayerMoving()
    {
        // Check if the player's velocity is above a certain threshold
        return target.GetComponent<Rigidbody>().velocity.magnitude > 0.1f;
    }
}
