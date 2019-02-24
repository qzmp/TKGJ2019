using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AudioController _audioController;

    public float visibilityAngle = 90;
    public float rotationSpeed = 180;
    public float runningSpeed = 5;

    /// <summary>Target points to move to in order</summary>
	public Transform[] patrolTargets;

    /// <summary>Time in seconds to wait at each target</summary>
    public float delay = 0;

    private LightFlicker lightFlicker;

    /// <summary>Current target index</summary>
    private int currentTargetIndex;

    IAstarAI agent;
    private float switchTime = float.PositiveInfinity;

    private PlayerController player;
    private bool hasBeenPatrolling = true;

    private float normalSpeed;
    private bool hasSeenPlayer = false;

    private Tween tween;

    protected void Awake()
    {
        agent = GetComponent<IAstarAI>();
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.normalSpeed = this.agent.maxSpeed;
        this.lightFlicker = GetComponentInChildren<LightFlicker>(true);
    }

    /// <summary>Update is called once per frame</summary>
    void Update()
    {
        if (this.lightFlicker && player.IsInLight)
        {
            if (this.hasBeenPatrolling)
            {
                Debug.Log("enemy started moving toward player");
                this.agent.SearchPath();
                this.agent.maxSpeed = this.runningSpeed;
                this.hasBeenPatrolling = false;
            }

            this.lightFlicker.SetIntensity(true);
            if (IsFacingPlayer() || this.hasSeenPlayer)
            {
                this.hasSeenPlayer = true;
                Debug.Log("moving towards player");

                if(player.IsInLight)
                {
                    this.agent.destination = player.transform.position;
                }

                this.agent.isStopped = false;
                if (this.tween != null)
                {
                    this.tween.Kill();
                    this.tween = null;

                }
            }
            else
            {
                Debug.Log("turning towards player");
                this.agent.isStopped = true;
                RotateTowardsPlayer();
            }
        }
        else
        {
            if(!this.hasBeenPatrolling)
            {
                this.hasSeenPlayer = false;
                Debug.Log("enemy started to return to patrol");
                this.lightFlicker.SetIntensity(false);
                this.agent.isStopped = true;
                this.agent.maxSpeed = normalSpeed;
                this.hasBeenPatrolling = true;

                currentTargetIndex = GetNearestPatrolTargetIndex();
                switchTime = Time.time + delay;

            }

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
                this.agent.isStopped = false;
                if(this.tween != null)
                {
                    this.tween.Kill();
                    this.tween = null;
                }
            }
            else if (!float.IsPositiveInfinity(switchTime))
            {
                LookAround();
            }

            if (patrolTargets.Length > 0)
            {
                currentTargetIndex = currentTargetIndex % this.patrolTargets.Length;
                agent.destination = patrolTargets[currentTargetIndex].position;
            }

            if (search) agent.SearchPath();
        }

        try
        {
            _audioController.SetWalkAudio(agent.velocity);
        }
        catch { }
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

    private void LookAround()
    {
        Debug.Log("lookingAround");
        //anim?
        Vector3 startingRotation = this.transform.rotation.eulerAngles;
        if(this.tween == null)
        {
            Rotate(startingRotation, true);
        }
    }

    private void Rotate(Vector3 startingRotation, bool right)
    {
        this.tween = this.transform.DORotate(new Vector3(0, 0, startingRotation.z + (right ? 45 : -45)), 1).SetEase(Ease.InOutCirc).OnComplete(() => Rotate(startingRotation, !right));

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
