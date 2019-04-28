using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingBehavior : StateMachineBehaviour {

    GameObject player;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Debug.Log ("OnStateEnter FLEEING");

        GameObject player = GameObject.FindWithTag ("Player");

        animator.gameObject.SendMessage ("SetStatusFleeing", true);
    }

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.SendMessage ("SetStatusFleeing", false);
    }
}