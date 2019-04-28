using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    AudioSource audioS;

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

        audioS = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update () {
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

    // Actions

    void HopMove () {

    }

    void SneekMove () {

    }

    void Die () {

    }

    // SFX
    void playStepSound () {
        audioS.clip = stepSound;
        audioS.Play ();
    }

    void playJumpSound () {
        audioS.clip = jumpSound;
        audioS.Play ();
    }

    void playJumpChargeSound () {
        audioS.clip = jumpChargeSound;
        audioS.Play ();
    }

    void playLandSound () {
        audioS.clip = landSound;
        audioS.Play ();
    }

}