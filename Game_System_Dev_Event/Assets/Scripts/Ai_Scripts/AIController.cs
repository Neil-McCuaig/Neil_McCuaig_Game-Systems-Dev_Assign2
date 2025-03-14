using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Movement dealDamage;
    public StateMachine stateMachine;
    public Transform player;
    public Transform[] patrolWaypoints;
    public int currentWaypointIndex;
    public float patrolSpeed = 5;
    public float detectionRange = 3;

    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new StateIdle(this));
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void ChangeState(State newState)
    {
        stateMachine.ChangeState(newState);
    }

    public bool CanSeePlayer()
    {
        return Vector3.Distance(transform.position, player.position) < detectionRange;
    }

    public void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * patrolSpeed);
    }

    public void Patrol()
    {
        if (patrolWaypoints.Length == 0)
        {
            return;
        }

        Transform targetWaypoint = patrolWaypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, Time.deltaTime * patrolSpeed);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }

    }

    public void OnStateExit()
    {

    }
}
