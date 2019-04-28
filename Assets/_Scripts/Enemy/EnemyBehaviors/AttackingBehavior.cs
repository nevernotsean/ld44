using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackingBehavior : StateMachineBehaviour {
    NavMeshAgent nma;
    bool isFull = false;
    float timeAttacking = 0.0f;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Debug.Log ("OnStateEnter ATTACKING");
        animator.gameObject.SendMessage ("SetStatusAttacking", true);
        nma = animator.gameObject.GetComponent<NavMeshAgent> ();
    }

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (timeAttacking > 1.0f && !animator.GetBool ("isWandering")) {
            var pct = animator.GetInteger ("walletFullPercent");

            if (pct < 100) {
                pct = pct + 1;
                animator.SetInteger ("walletFullPercent", pct);
                animator.gameObject.SendMessage ("UpdateAttackProgress", pct);
            } else {
                if (!isFull) {
                    animator.gameObject.SendMessage ("DamagePlayer");
                    isFull = true;
                }
            }

        } else {
            timeAttacking = timeAttacking + Time.deltaTime;
        }
    }

    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.SendMessage ("SetStatusAttacking", false);
    }
}