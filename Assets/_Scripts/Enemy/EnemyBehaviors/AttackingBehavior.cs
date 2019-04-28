using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingBehavior : StateMachineBehaviour {
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log ("OnStateEnter ATTACKING");
        animator.gameObject.SendMessage ("SetStatusAttacking", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Instantiate (effect, animator.transform.position, Quaternion.identity);
        animator.gameObject.SendMessage ("SetStatusAttacking", false);
    }
}