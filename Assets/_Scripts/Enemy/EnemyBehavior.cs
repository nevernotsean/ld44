using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {
    GameObject player;
    Animator anim;
    NavMeshAgent nma;

    public GameObject escapePoint;

    public UnityAtoms.GameObjectList CapturePointsList;
    public GameObject statusAttacking;
    public GameObject statusEscaping;
    public GameObject statusHunting;
    public GameObject statusFleeing;

    float distanceFromSpawnPoint;

    void Start () {
        player = GameObject.FindWithTag ("Player");
        nma = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
        anim.SetInteger ("walletFullPercent", 0);
    }

    void Update () {
        if (anim.GetBool ("isSpooked")) {
            var dist = Vector3.Distance (transform.position, player.transform.position);

            if (dist < 10.0f) {
                anim.SetBool ("isSpooked", false);
            }
        }
    }

    private void OnCollisionEnter (Collision other) {
        if (other.gameObject.tag == "MoneyCapture") {
            print ("HIT " + other.gameObject.tag);

            GetComponent<Rigidbody> ().velocity = Vector3.zero;
            nma.SetDestination (transform.position);

            anim.SetBool ("NextToCapturePoint", true);
        }

        if (other.gameObject.tag == "Enemy") {
            print ("HIT " + other.gameObject.tag);
        }
    }

    private void OnCollisionExit (Collision other) {
        if (other.gameObject.tag == "MoneyCapture") {
            anim.SetBool ("NextToCapturePoint", false);
        }
    }
    /* 
    // Status Methods
    */
    public void SetStatusAttacking (bool active) {
        // print ("statusAttacking " + active);
        statusAttacking.SetActive (active);

        if (active)
            StartCoroutine ("Attack");
    }

    public void SetStatusEscaping (bool active) {
        // print ("statusEscaping " + active);
        statusEscaping.SetActive (active);

        if (active) {
            nma.SetDestination (escapePoint.transform.position);
        }
    }

    public void SetStatusHunting (bool active) {
        // print ("statusHunting " + active);
        statusHunting.SetActive (active);

        if (active)
            GotoClosestCapture ();
    }

    public void SetStatusFleeing (bool active) {
        // print ("statusFleeing " + active);

        // if (active)
        statusFleeing.SetActive (active);
    }

    IEnumerator Attack () {

        var pct = anim.GetInteger ("walletFullPercent");

        while (pct < 100) {

            yield return new WaitForSeconds (0.01f);

            pct = pct + 1;

            anim.SetInteger ("walletFullPercent", pct);

            statusAttacking.GetComponent<TMPro.TextMeshPro> ().SetText ((pct).ToString ("N0"));

            yield return null;
        }
    }
    /* 
    // Action methods
    */
    public void Die () {
        Destroy (gameObject);
    }

    public void Escape () {
        Destroy (gameObject);
    }

    public void GetSpooked () {
        anim.SetBool ("isSpooked", true);
    }

    private void OnApplicationQuit () {
        CapturePointsList.Clear ();
    }

    public void GotoClosestCapture () {
        GameObject destination = FindClosestCapture (transform.position, CapturePointsList.List);
        nma.SetDestination (destination.transform.position);
    }

    /* 
        Utils 
    */
    // Returns the closest GameObject
    GameObject FindClosestCapture (Vector3 origin, List<GameObject> points) {
        GameObject closestPoint = null;
        var distance = 10000.0f;

        foreach (var point in points) {
            var thisDist = Vector3.Distance (origin, point.transform.position);

            // Debug.Log (point.name + " distance is" + distance);

            if (thisDist < distance) {
                distance = thisDist;
                closestPoint = point;
            }
        }

        return closestPoint;
    }
}