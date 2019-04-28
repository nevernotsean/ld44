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
    public UnityAtoms.IntVariable SceneIntensity;

    int startingBalance = 1000000;
    int lastTier;
    int balance;
    // Start is called before the first frame update
    void Start () {
        PlayerHealth.SetValue (startingBalance);
        lastTier = startingBalance;
        balance = startingBalance;

        UpdateHealth ();

        SceneIntensity.SetValue (0);
    }

    // Update is called once per frame
    void Update () {
        if (balance != PlayerHealth.Value) {
            UpdateHealth ();
        }

        if (PlayerHealth.Value < lastTier - 1000) {
            print ("lastTier: " + lastTier);
            RampIntesity (false);
            lastTier = lastTier - 1000;
            print ("newTier: " + lastTier);
        }

        if (PlayerHealth.Value > lastTier + 1000) {
            RampIntesity (true);
            lastTier = lastTier + 1000;
        }

    }

    public void UpdateHealth () {
        balance = PlayerHealth.Value;
        PlayerHealthUI.text = balance.ToString ();
    }

    public void RampIntesity (bool down) {
        if (down)
            SceneIntensity.SetValue (SceneIntensity.Value - 1);
        else
            SceneIntensity.SetValue (SceneIntensity.Value + 1);
    }

    private void OnApplicationQuit () {
        SceneIntensity.SetValue (0);
    }
}