using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

namespace AI
{
    public class StateController : MonoBehaviour
    {

        public State currentState;
        public EnemyStats enemyStats;
        public State remainState;


        [HideInInspector] public IAstarAI navMeshAgent;
        [HideInInspector] public List<Transform> wayPointList;
        [HideInInspector] public int nextWayPoint;
        [HideInInspector] public Transform chaseTarget;
        [HideInInspector] public float stateTimeElapsed;

        private bool aiActive;


        void Awake()
        {
            navMeshAgent = GetComponent<IAstarAI>();
        }

        public void SetupAI(bool aiActive, List<Transform> wayPoints)
        {
            wayPointList = wayPoints;
            this.aiActive = aiActive;
            if (aiActive)
            {
                navMeshAgent.isStopped = true;
            }
            else
            {
                navMeshAgent.isStopped = false;
            }
        }

        void Update()
        {
            if (!aiActive)
                return;
            currentState.UpdateState(this);
        }

        void OnDrawGizmos()
        {
            //if (currentState != null && eyes != null)
            //{
            //    Gizmos.color = currentState.sceneGizmoColor;
            //    Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
            //}
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }
    }
}
