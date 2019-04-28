using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeingBehavior : StateMachineBehaviour {

    GameObject player;
    NavMeshAgent nma;
    float timer = 0.0f;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Debug.Log ("OnStateEnter FLEEING");
        nma = animator.gameObject.GetComponent<NavMeshAgent> ();
        player = GameObject.FindWithTag ("Player");

        animator.gameObject.SendMessage ("SetStatusFleeing", true);

        var c = Random.insideUnitCircle * 5;
        var position = animator.gameObject.transform.position;
        var newPos = position + new Vector3 (c.x, 0, c.y);

        animator.gameObject.GetComponent<NavMeshAgent> ().SetDestination (newPos);

        animator.gameObject.SendMessage ("SetStatusHunting", true);
    }

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!nma.pathPending && !nma.hasPath) {
            animator.SetBool ("isFleeing", false);
        } else {
            if (timer > 100.0f) {
                animator.SetBool ("isWandering", true);
            } else {
                timer = timer + Time.deltaTime;
            }
        }
    }

    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.SendMessage ("SetStatusFleeing", false);
    }
}