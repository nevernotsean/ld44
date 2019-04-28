using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshInvisibleBlockers : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {
        foreach (var child in GetComponentsInChildren<MeshRenderer> ()) {
            child.enabled = false;
        }
    }

    // Update is called once per frame
    void Update () {

    }
}