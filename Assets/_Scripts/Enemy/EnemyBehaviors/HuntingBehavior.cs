using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuntingBehavior : StateMachineBehaviour {

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Debug.Log ("OnStateEnter HUNTING");
        animator.gameObject.SendMessage ("SetStatusHunting", true);
    }

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.SendMessage ("SetStatusHunting", false);
    }

}