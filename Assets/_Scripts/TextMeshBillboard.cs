﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshBillboard : MonoBehaviour {
    Transform cam;
    // Start is called before the first frame update
    void Start () {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update () {
        transform.rotation = Quaternion.LookRotation (transform.position - cam.position);
    }
}