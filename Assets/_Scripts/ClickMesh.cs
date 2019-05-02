using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickMesh : MonoBehaviour {
    Camera cam;

    public UnityAtoms.Vector3Variable LastMouseClickPosition;
    public UnityAtoms.Vector3Variable CursorPosition;

    public GameObject destinationPrefab;
    public GameObject crosshairPrefab;

    GameObject destinationInstance;
    GameObject crossHairInstance;

    NavMeshAgent nma;

    void Start () {
        cam = Camera.main;
        nma = GameObject.FindWithTag ("Player").GetComponent<NavMeshAgent> ();
    }

    void Update () {
        var viewportPos = cam.ScreenToViewportPoint (Input.mousePosition);
        Ray ray = cam.ViewportPointToRay (viewportPos);
        bool didHit = Physics.Raycast (ray, out RaycastHit hit);

        if (didHit) {
            CursorPosition.SetValue(hit.point);
            if (crossHairInstance == null)
                crossHairInstance = Instantiate (crosshairPrefab, hit.point, Quaternion.identity, transform);

            crossHairInstance.transform.position = hit.point;
            crosshairPrefab.SetActive (true);

            // print("did hit" + hit.collider.name);
            // Debug.DrawRay(ray.origin, ray.direction * 100, Color.white, 1000);

            if (Input.GetMouseButtonDown (0)) {
                LastMouseClickPosition.SetValue (hit.point);

                if (destinationInstance == null) {
                    destinationInstance = Instantiate (crosshairPrefab, LastMouseClickPosition.Value, Quaternion.identity, transform);
                }

               
                destinationInstance.transform.position = LastMouseClickPosition.Value;
                destinationInstance.SetActive (true);
            }

            if (destinationInstance != null)
                if (!nma.pathPending && !nma.hasPath)
                    destinationInstance.SetActive (false);
        } else {
            crosshairPrefab.SetActive (false);
        }
    }
}