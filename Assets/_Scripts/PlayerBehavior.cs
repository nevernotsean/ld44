using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour {

    Rigidbody rb;
    AudioSource audioS;
    NavMeshAgent nma;

    public UnityAtoms.Vector3Variable LastMouseClickPosition;
    public UnityAtoms.Vector3Variable CursorPosition;

    public AudioClip stepSound;
    public AudioClip jumpSound;
    public AudioClip jumpChargeSound;
    public AudioClip landSound;

    public UnityAtoms.BoolVariable isHoldingDownJump;
    public UnityAtoms.BoolVariable isHoldingDownSneak;

    public UnityAtoms.IntVariable PlayerHealth;

    public LayerMask ignoreSlam;

    bool isLimp = false;
    float timer = 5.0f;
    Collider[] enemyColliders;

    // Start is called before the first frame update
    void Start () {
        isHoldingDownJump.SetValue (false);
        isHoldingDownSneak.SetValue (false);

        rb = GetComponent<Rigidbody> ();
        nma = GetComponent<NavMeshAgent> ();
        audioS = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update () {
        UpdateInputs ();

        if (PlayerHealth.Value <= 0) {
            Die ();
        }

        if (isHoldingDownJump.Value) {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.7f;
            else
                Time.timeScale = 1.0f;

            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        } else {
            if (Time.timeScale != 1.0f)
                Time.timeScale = 1.0f;
        }

        if (!nma.pathPending && !nma.hasPath)
            audioS.Stop ();

        if (isLimp && timer <= 0) {
            timer = timer - Time.deltaTime;
        }

        if (timer < 0) {
            timer = 5.0f;
            isLimp = false;
        }
    }

    private void OnCollisionEnter (Collision other) {
        if (rb.velocity.magnitude > 10 && other.gameObject.tag == "Enemy") {
            // if (nma.enabled) return;
            other.gameObject.SendMessage("Die");

            enemyColliders = Physics.OverlapSphere(transform.position, 2.0f, ignoreSlam.value);
            
            foreach (var enemy in enemyColliders)
            {
                // print(enemy.gameObject.name);
                if (enemy.gameObject.tag == "Enemy") 
                    enemy.SendMessage("Die");
            }
        }

        if (other.gameObject.tag == "oob")
            nma.Warp(Vector3.zero);
    }

    // Inputs
    void UpdateInputs () {
        if (Input.GetMouseButtonDown (0)) {
            // print ("GetMouseButtonDown 1");
            // playStepSound ();
            isHoldingDownJump.SetValue (true);
        }

        if (Input.GetMouseButtonUp (0)) {
            // print ("GetMouseButtonUp 1");
            isHoldingDownJump.SetValue (false);
        }

        if (Input.GetMouseButtonDown (1)) {
            // print ("GetMouseButtonDown 2");
            playJumpChargeSound ();
            isHoldingDownSneak.SetValue (true);
            nma.enabled = false;
        }

        if (Input.GetMouseButtonUp (1)) {
            // print ("GetMouseButtonUp 2");
            HopMove ();
            isHoldingDownSneak.SetValue (false);
        }
    }

    // Actions

    void HopMove () {
        print ("jump");
        rb.AddForce (
            Vector3.Lerp (Vector3.up, (CursorPosition.Value - transform.position).normalized, 0.75f) *
            80 * rb.mass, ForceMode.Impulse);
            isLimp = true;
    }

    void Die () {
        nma.isStopped = true;
        rb.AddExplosionForce (100, transform.position, 100.0f);
        Destroy (gameObject, 1);
    }

    // SFX
    void playStepSound () {
        audioS.PlayOneShot (stepSound);
    }

    void playJumpSound () {
        audioS.PlayOneShot (jumpSound);
    }

    void playJumpChargeSound () {
        audioS.PlayOneShot (jumpChargeSound);
    }

    void playLandSound () {
        audioS.PlayOneShot (landSound);
    }

}