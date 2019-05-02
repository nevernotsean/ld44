using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {
    GameObject player;
    Animator anim;
    NavMeshAgent nma;
    GameObject escapePoint;
    AudioSource aus;
    Rigidbody rb;

    public UnityAtoms.GameObjectList CapturePointsList;
    public GameObject statusAttacking;
    public GameObject statusEscaping;
    public GameObject statusHunting;
    public GameObject statusFleeing;
    public GameObject statusWandering;

    public GameObject Arms;
    public GameObject corpsePrefab;

    public UnityAtoms.IntVariable PlayerHealth;

    [HideInInspector] GameObject currentCapturePoint;

    public AudioClip[] dieSounds;
    public AudioClip[] screamSounds;

    float distanceFromSpawnPoint;
    public int attackDamage = 100;

    int holdingCash = 0;

    void Start () {
        player = GameObject.FindWithTag ("Player");
        rb = GetComponent<Rigidbody>();
        aus = GetComponent<AudioSource> ();
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
        if (nma != null && nma.hasPath) {
            Gizmos.DrawCube (nma.destination, Vector3.one * 3);
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject == currentCapturePoint) {
            // print ("HEY LISTEN I made it!" + other.gameObject.name);
            anim.SetBool ("NextToCapturePoint", true);
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.gameObject == currentCapturePoint) {
            // print ("BYEEE! " + other.gameObject.name);
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
            DropArms();
    }

    public void SetStatusEscaping (bool active) {
        // print ("statusEscaping " + active);
        statusEscaping.SetActive (active);

        if (active) {
            ForwardArms();
            nma.SetDestination (escapePoint.transform.position);
        }
    }

    public void SetStatusHunting (bool active) {
        // print ("statusHunting " + active);
        statusHunting.SetActive (active);

        if (active) {
            GotoClosestCapture (true);
            ForwardArms();
        }
    }

    public void SetStatusFleeing (bool active) {
        // print ("statusFleeing " + active);
        statusFleeing.SetActive (active);

        if (active)
            RaiseArms();
    }

    public void SetStatusWandering (bool active) {
        // print ("statusIdle " + active);
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

    void PlayRandomSFX (AudioClip[] clips) {
        var clip = clips[Random.Range (0, clips.Length)];
        aus.PlayOneShot (clip);
    }

    /* 
    // Action methods
    */
    public void Die () {
        nma.enabled = false;
        HealPlayer ();
        PlayRandomSFX (dieSounds);
        
        
        var body = Instantiate(corpsePrefab, transform.position, transform.rotation);
            body.GetComponent<Rigidbody>().AddExplosionForce (5, transform.position, 1.0f, 0.5f, ForceMode.Force);

        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        Destroy(gameObject, 1);
        Destroy(body, 10);
    }

    public void Escape () {
        Destroy (gameObject);
    }

    public void GetSpooked () {
        PlayRandomSFX (screamSounds);
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

    public void RaiseArms(){
        Arms.transform.rotation = Quaternion.Euler(-180,0,0);
    }

    public void ForwardArms(){
        Arms.transform.rotation = Quaternion.Euler(-90,0,0);
    }

    public void DropArms(){
        Arms.transform.rotation = Quaternion.Euler(0,0,0);
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