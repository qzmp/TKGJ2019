using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Assertions;


[RequireComponent(typeof(IAstarAI))]
public class EnemyAnimation : MonoBehaviour
{
    public Animator _legsAnimator;
    public Animator _topAnimator;

    private IAstarAI _agent;

    private void Start()
    {
        Assert.IsNotNull(_legsAnimator);
        Assert.IsNotNull(_topAnimator);

        _agent = GetComponent<IAstarAI>();
    }

    private void Update()
    {
        UpdateAnimations(_agent.velocity.magnitude);
    }

    public void UpdateAnimations(float walkingSpeed)
    {
        if (walkingSpeed < 0.1f)
        {
            _legsAnimator.SetBool("is_wallking", false);
            _topAnimator.SetBool("is_walking", false);
            _topAnimator.SetBool("is_idle", true);
        }
        else
        {
            _legsAnimator.SetBool("is_wallking", true);
            _topAnimator.SetBool("is_walking", true);
            _topAnimator.SetBool("is_idle", false);

            _legsAnimator.SetFloat("walking_speed", walkingSpeed);
            _topAnimator.SetFloat("walking_speed", walkingSpeed);
        }
    }
}
