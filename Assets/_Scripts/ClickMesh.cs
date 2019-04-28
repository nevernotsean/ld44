using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMesh : MonoBehaviour {
    Camera cam;
    public UnityAtoms.Vector3Variable LastMouseClickPosition;
    void Start () {
        cam = Camera.main;
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            var viewportPos = cam.ScreenToViewportPoint (Input.mousePosition);
            Ray ray = cam.ViewportPointToRay (viewportPos);

            // print("Click" + viewportPos);

            bool didHit = Physics.Raycast (ray, out RaycastHit hit);

            if (didHit) {
                // print("did hit" + hit.collider.name);
                // Debug.DrawRay(ray.origin, ray.direction * 100, Color.white, 1000);
                LastMouseClickPosition.SetValue (hit.point);
            }
        }
    }
}