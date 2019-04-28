using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBehavior : MonoBehaviour {
    public TMPro.TMP_Text PlayerHealthUI;

    public GameObject isHoldingDownJumpUI;
    public GameObject isHoldingDownSneakUI;

    public UnityAtoms.BoolVariable isHoldingDownJump;
    public UnityAtoms.BoolVariable isHoldingDownSneak;
    public UnityAtoms.IntVariable PlayerHealth;

    int balance;
    // Start is called before the first frame update
    void Start () {
        UpdateHealth ();
    }

    // Update is called once per frame
    void Update () {
        if (balance != PlayerHealth.Value) {
            UpdateHealth ();
        }
    }

    public void UpdateHealth () {
        balance = PlayerHealth.Value;
    }
}