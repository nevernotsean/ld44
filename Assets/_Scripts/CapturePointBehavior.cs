using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePointBehavior : MonoBehaviour {

    public bool isBusy = false;

    public void SetBusy (bool busy) {
        isBusy = busy;
    }

    private void OnTriggerEnter (Collider other) {
        SetBusy (true);
    }

    private void OnTriggerExit (Collider other) {
        SetBusy (false);
    }
}