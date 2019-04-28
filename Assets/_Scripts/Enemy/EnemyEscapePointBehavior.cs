using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEscapePointBehavior : MonoBehaviour {
    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Enemy") {
            var anim = other.gameObject.GetComponent<Animator> ();

            if (anim.GetInteger ("walletFullPercent") > 99) {
                other.gameObject.SendMessage ("Escape");
            }
        }
    }
}