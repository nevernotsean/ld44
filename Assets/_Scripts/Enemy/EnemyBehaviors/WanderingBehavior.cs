using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingBehavior : StateMachineBehaviour {

    // public GameObject effect;
    NavMeshAgent nma;
    Bounds wanderingArea;
    float timeIdle = 0.0f;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Debug.Log ("OnStateEnter IDLE");

        // Init refs and vals
        nma = animator.gameObject.GetComponent<NavMeshAgent> ();
        wanderingArea = GameObject.FindWithTag ("WanderingArea").GetComponent<BoxCollider> ().bounds;
        timeIdle = 0.0f;

        // Trigger Idle Status
        animator.gameObject.SendMessage ("SetStatusWandering", true);

        WanderToRandomPoint ();
    }

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (!nma.pathPending && !nma.hasPath && animator.GetBool ("isWandering")) {
            Debug.Log ("I have reached my destination!");
            // Code that you want to execute when this event occurs.
            animator.SetBool ("isWandering", false);
        }

        // After 5 seconds of Idleing, stop Idleing
        if (timeIdle > 5.0f && animator.GetBool ("isWandering")) {
            Debug.Log ("I'm done idleing");
            animator.SetBool ("isWandering", false);
        } else {
            timeIdle = timeIdle + Time.deltaTime;
        }
    }

    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.SendMessage ("SetStatusWandering", false);
        animator.SetBool ("isWandering", false);
    }

    void WanderToRandomPoint () {
        var dest = RandomPointInBounds (wanderingArea);
        nma.SetDestination (dest);
    }

    public static Vector3 RandomPointInBounds (Bounds bounds) {
        return new Vector3 (
            Random.Range (bounds.min.x, bounds.max.x),
            Random.Range (bounds.min.y, bounds.max.y),
            Random.Range (bounds.min.z, bounds.max.z)
        );
    }
}