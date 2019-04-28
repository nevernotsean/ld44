using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleBehavior : StateMachineBehaviour {
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
		animator.gameObject.SendMessage ("HideAllStatus");

		// reset the nav destination
		nma.velocity = Vector3.zero;
		nma.SetDestination (animator.gameObject.transform.position);
	}

	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// After 5 updates of Idleing, stop Idleing
		if (timeIdle > 5.0f && animator.GetBool ("isWandering")) {
			Debug.Log ("I'm done idleing");
		} else {
			timeIdle = timeIdle + Time.deltaTime;
		}
	}
}