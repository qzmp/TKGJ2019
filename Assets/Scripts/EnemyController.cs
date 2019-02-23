using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyController : MonoBehaviour
{

    public float visibilityAngle = 90;
    public float rotationSpeed = 180;
    public float runningSpeed = 5;

    /// <summary>Target points to move to in order</summary>
	public Transform[] patrolTargets;

    /// <summary>Time in seconds to wait at each target</summary>
    public float delay = 0;

    /// <summary>Current target index</summary>
    int currentTargetIndex;

    IAstarAI agent;
    float switchTime = float.PositiveInfinity;

    private PlayerController player;
    private bool isPatrolling = true;

    private float normalSpeed;


    protected void Awake()
    {
        agent = GetComponent<IAstarAI>();
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.normalSpeed = this.agent.maxSpeed;
    }

    /// <summary>Update is called once per frame</summary>
    void Update()
    {
        if(player.IsInLight)
        {
            if(IsFacingPlayer())
            {
                if(this.isPatrolling)
                {
                    this.agent.destination = player.transform.position;
                    this.agent.SearchPath();
                    this.agent.maxSpeed = this.runningSpeed;
                }
                this.isPatrolling = false;
                this.agent.isStopped = false;
            }
            else
            {
                this.isPatrolling = true;
                this.agent.isStopped = true;
                RotateTowardsPlayer();
            }
        }
        else
        {
            this.agent.isStopped = false;
            this.agent.maxSpeed = normalSpeed;
            this.isPatrolling = true;
            if (patrolTargets.Length == 0) return;

            bool search = false;

            // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
            // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
            if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime))
            {
                switchTime = Time.time + delay;
            }

            if (Time.time >= switchTime)
            {
                currentTargetIndex = currentTargetIndex + 1;
                search = true;
                switchTime = float.PositiveInfinity;
            }

            currentTargetIndex = currentTargetIndex % patrolTargets.Length;
            agent.destination = patrolTargets[currentTargetIndex].position;

            if (search) agent.SearchPath();
        }

    }

    private void RotateTowardsPlayer()
    {
        //find the vector pointing from our position to the target
        Vector3 direction = (this.player.transform.position - this.transform.position);
        Quaternion newRotation = Quaternion.LookRotation(direction, -Vector3.forward);

        //rotate us over time according to speed until we are in the required rotation
        Quaternion rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * this.rotationSpeed);
        Vector3 eulerRot = rotation.eulerAngles;
        eulerRot.y = 0;
        eulerRot.x = 0;
        this.transform.rotation = Quaternion.Euler(eulerRot);
    }


    private bool IsFacingPlayer()
    {
        return Vector3.Angle(this.transform.up, player.transform.position - this.transform.position) < this.visibilityAngle / 2;
    }

    private int GetNearestPatrolTargetIndex()
    {
        float nearestDistance = Vector3.Distance(this.patrolTargets[0].position, this.transform.position);
        int nearestIndex = 0;
        for(int i = 1; i < this.patrolTargets.Length; i++)
        {
            float newDistance = Vector3.Distance(this.patrolTargets[i].position, this.transform.position);
            if(newDistance < nearestDistance)
            {
                nearestDistance = newDistance;
                nearestIndex = i;
            }
        }
        return nearestIndex;
    }
}
