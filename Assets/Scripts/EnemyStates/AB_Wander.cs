using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AB_Wander : StateMachineBehaviour
{

    private Enemy npc;
    private NavMeshAgent agent;

    private int destPoint = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (npc == null)
        {
            npc = animator.GetComponent<Enemy>();
            agent = npc.agent;
        }

        Wander();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if ((agent.destination - npc.transform.position).sqrMagnitude <= (agent.stoppingDistance * agent.stoppingDistance) + 0.5f) {
        //    animator.SetBool("isPatrolling", false);
        //}

        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance + 0.5f)
        {
            animator.SetBool("isPatrolling", false);
        }

        npc.DetectPlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPatrolling", false);
        // Reinicia el camino del agente por si acaso.
        agent.ResetPath();
    }

    void Wander()
    {
        if (npc.patrolPoints == null && npc.patrolPoints.Length < 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = npc.patrolPoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % npc.patrolPoints.Length;
    }
}
