using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class CopController : MonoBehaviour
{
    [SerializeField] State state;
    [SerializeField] float speedRun;
    [SerializeField] List<GameObject> patrolPoints = new List<GameObject>();

    float speedWalk;
    float stoppingDist = 0.3f;

    Animator animator;
    NavMeshAgent navMeshAgent;

    enum State
    {
        isIdle,
        isRun,
        isWalk,
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        speedWalk = navMeshAgent.speed;

        StartCoroutine(Move());
    }

    void CopState()
    {
        foreach(State s in Enum.GetValues(typeof(State)))
        {
            animator.SetBool(s.ToString(), false);
        }
        animator.SetBool(state.ToString(), true);
    }

    IEnumerator Move()
    {
        float speed = 0;
        int p = 0;
        Vector3 targetPos;
        while (true)
        {
            
            switch (state)
            {
                case State.isIdle:
                    speed = 0;
                    break;
                case State.isRun:
                    speed = speedRun;
                    break;
                case State.isWalk:
                    speed = speedWalk;
                    break;
            }

            CopState();

            navMeshAgent.speed = speed;
            targetPos = patrolPoints[p].transform.position;
            navMeshAgent.destination = targetPos;

            state = (State)Enum.Parse(typeof(State), patrolPoints[p].tag);

            if (Vector3.Distance(transform.position, targetPos) < stoppingDist)
            {
                
                if(p < patrolPoints.Count-1)p++;
                else
                {
                    state = State.isIdle;
                }
            }
            
            yield return null;
        }
               
    }
}
