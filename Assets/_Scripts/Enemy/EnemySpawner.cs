using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject EnemyPrefab;
    public UnityAtoms.GameObjectList CapturePointList;
    public Transform SpawnPoint;

    public int waveAmount = 10;
    public float spawnWait;
    public float startWait;
    // public float waveWait;

    public bool waveSpawned = false;

    GameObject[] CapturePoints;

    // Start is called before the first frame update
    void Start () {
        AddCapturePoints ();
        SpawnWave ();
    }

    // Update is called once per frame
    void Update () {

    }

    public void SpawnWave () {
        StartCoroutine (SpawnWaves ());
    }

    // Spawn a wave of prefabs
    IEnumerator SpawnWaves () {
        // yield return new WaitForSeconds (startWait);

        while (!waveSpawned) {
            for (int i = 0; i < waveAmount; i++) {
                Vector3 c = Random.insideUnitCircle * 10;
                Vector3 pos = SpawnPoint.position + new Vector3 (c.x, 0, c.y);

                var newEnemy = Instantiate (EnemyPrefab, pos, Quaternion.identity);
                newEnemy.transform.parent = transform;
                yield return new WaitForSeconds (spawnWait);
            }
            waveSpawned = true;
            // yield return new WaitForSeconds (waveWait);
        }
    }

    // Cache the capturepoints
    void AddCapturePoints () {
        CapturePoints = GameObject.FindGameObjectsWithTag ("MoneyCapture");

        foreach (var item in CapturePoints) {
            CapturePointList.Add (item);
        }
    }
}