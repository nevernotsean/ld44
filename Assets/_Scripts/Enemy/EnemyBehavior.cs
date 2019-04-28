using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {
    GameObject player;
    Animator anim;
    NavMeshAgent nma;
    GameObject escapePoint;

    public UnityAtoms.GameObjectList CapturePointsList;
    public GameObject statusAttacking;
    public GameObject statusEscaping;
    public GameObject statusHunting;
    public GameObject statusFleeing;
    public GameObject statusWandering;

    public UnityAtoms.IntVariable PlayerHealth;

    [HideInInspector] GameObject currentCapturePoint;

    public AudioClip[] dieSounds;
    public AudioClip[] screamSounds;

    float distanceFromSpawnPoint;
    int attackDamage = 100;

    int holdingCash = 0;

    void Start () {
        player = GameObject.FindWithTag ("Player");
        nma = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
        anim.SetInteger ("walletFullPercent", 0);
        escapePoint = GameObject.FindWithTag ("ExitPoint");
    }

    void Update () {
        if (anim.GetBool ("isSpooked")) {
            var dist = Vector3.Distance (transform.position, player.transform.position);

            if (dist < 10.0f) {
                anim.SetBool ("isSpooked", false);
            }
        }
    }

    private void OnDrawGizmos () {

        if (nma.hasPath) {
            Gizmos.DrawCube (nma.destination, Vector3.one * 3);
        }
    }

    private void OnTriggerEnter (Collider other) {
        print ("HEY LISTEN " + other.gameObject.name);
        if (other.gameObject == currentCapturePoint) {
            print ("I triggered my selected capture point");
        }
    }

    private void OnTriggerExit (Collider other) {
        print ("BYEEE" + other.gameObject.name);
    }

    // private void OnCollisionEnter (Collision other) {
    //     if (other.gameObject.tag == "MoneyCapture") {
    //         print ("HIT " + other.gameObject.tag);

    //         GetComponent<Rigidbody> ().velocity = Vector3.zero;
    //         nma.SetDestination (transform.position);

    //         anim.SetBool ("NextToCapturePoint", true);
    //     }

    //     if (other.gameObject.tag == "Enemy") {
    //         print ("HIT " + other.gameObject.tag);

    //         GetComponent<Rigidbody> ().velocity = Vector3.zero;
    //         nma.SetDestination (transform.position);
    //     }
    // }

    // private void OnCollisionExit (Collision other) {
    //     if (other.gameObject.tag == "MoneyCapture") {
    //         anim.SetBool ("NextToCapturePoint", false);
    //     }

    //     if (other.gameObject.tag == "Enemy") {
    //         print ("HIT " + other.gameObject.tag);

    //         if (currentCapturePoint)
    //             nma.SetDestination (currentCapturePoint.transform.position);
    //     }
    // }
    /* 
    // Status Methods
    */
    public void SetStatusAttacking (bool active) {
        print ("statusAttacking " + active);
        statusAttacking.SetActive (active);

        // if (active)
    }

    public void SetStatusEscaping (bool active) {
        print ("statusEscaping " + active);
        statusEscaping.SetActive (active);

        if (active) {
            nma.SetDestination (escapePoint.transform.position);
        }
    }

    public void SetStatusHunting (bool active) {
        print ("statusHunting " + active);
        statusHunting.SetActive (active);

        if (active) {
            GotoClosestCapture (true);
        }
    }

    public void SetStatusFleeing (bool active) {
        print ("statusFleeing " + active);
        statusFleeing.SetActive (active);

        // if (active)
    }

    public void SetStatusWandering (bool active) {
        print ("statusIdle " + active);
        statusWandering.SetActive (active);

        // if (active)

    }

    public void HideAllStatus () {
        SetStatusAttacking (false);
        SetStatusEscaping (false);
        SetStatusHunting (false);
        SetStatusFleeing (false);
        SetStatusWandering (false);
    }

    /* 
    // Action methods
    */
    public void Die () {
        HealPlayer ();
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

    public void GotoClosestCapture (bool limitToNotBusy) {
        FindClosestCapture (transform.position, CapturePointsList.List, limitToNotBusy);

        if (currentCapturePoint == null) {
            FindClosestCapture (transform.position, CapturePointsList.List, false);
        }

        nma.SetDestination (currentCapturePoint.transform.position);
    }

    public void DamagePlayer () {
        PlayerHealth.SetValue (PlayerHealth.Value - attackDamage);
        holdingCash = attackDamage;
    }

    public void HealPlayer () {
        PlayerHealth.SetValue (PlayerHealth.Value + holdingCash);
        holdingCash = 0;
    }

    /* 
        Utils 
    */
    public void UpdateAttackProgress (int pct) {
        statusAttacking.GetComponent<TMPro.TextMeshPro> ().SetText ((pct).ToString ("N0"));
    }

    // Returns the closest GameObject
    public void FindClosestCapture (Vector3 origin, List<GameObject> points, bool limitToNotBusy) {
        GameObject closestPoint = null;
        var distance = 10000.0f;

        foreach (var point in points) {
            if (limitToNotBusy) {
                if (point.GetComponent<CapturePointBehavior> ().isBusy)
                    continue;
            }
            var thisDist = Vector3.Distance (origin, point.transform.position);

            // Debug.Log (point.name + " distance is" + distance);

            if (thisDist < distance) {
                distance = thisDist;
                closestPoint = point;
            }
        }

        currentCapturePoint = closestPoint;
    }
}