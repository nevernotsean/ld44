using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuntingBehavior : StateMachineBehaviour {

    NavMeshAgent nma;
    float timeHunting = 0.0f;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Debug.Log ("OnStateEnter HUNTING");
        nma = animator.gameObject.GetComponent<NavMeshAgent> ();
        animator.gameObject.SendMessage ("SetStatusHunting", true);
    }

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!nma.pathPending && !nma.hasPath) {
            Debug.Log ("I have reached my destination! nextToCapturePoint");
        } else {
            if (timeHunting > 500.0f && !animator.GetBool ("isWandering")) {
                Debug.Log ("I give up hunting! gonna wander...");
                animator.SetBool ("isWandering", true);
            } else {
                timeHunting = timeHunting + Time.deltaTime;
            }
        }
    }

    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.SendMessage ("SetStatusHunting", false);
    }

}