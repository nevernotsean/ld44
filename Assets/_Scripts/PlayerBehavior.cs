using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour {

    Rigidbody rb;
    AudioSource audioS;
    NavMeshAgent nma;

    public UnityAtoms.Vector3Variable LastMouseClickPosition;

    public AudioClip stepSound;
    public AudioClip jumpSound;
    public AudioClip jumpChargeSound;
    public AudioClip landSound;

    public UnityAtoms.BoolVariable isHoldingDownJump;
    public UnityAtoms.BoolVariable isHoldingDownSneak;

    public UnityAtoms.IntVariable PlayerHealth;

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
    }

    // Inputs
    void UpdateInputs () {
        if (Input.GetMouseButtonDown (0)) {
            // print ("GetMouseButtonDown 1");
            playStepSound ();
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
        }

        if (Input.GetMouseButtonUp (1)) {
            // print ("GetMouseButtonUp 2");
            HopMove ();
            isHoldingDownSneak.SetValue (false);
        }
    }

    // Actions

    void HopMove () {
        rb.AddForce (Vector3.Lerp (transform.up, transform.forward, 0.2f) * rb.mass * 100, ForceMode.Impulse);
    }

    void SneekMove () {

    }

    void Die () {

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