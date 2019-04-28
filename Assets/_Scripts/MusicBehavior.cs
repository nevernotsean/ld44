using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehavior : MonoBehaviour {
    public UnityAtoms.IntVariable SceneIntensity;
    public AudioClip[] Tracks;

    AudioSource[] audioSources;

    public float onVolume = 0.7f;

    void Start () {
        SceneIntensity.SetValue (0);

        foreach (var clip in Tracks) {
            AudioSource aus = gameObject.AddComponent (typeof (AudioSource)) as AudioSource;
            aus.clip = clip;
            aus.loop = true;
            aus.Play ();
        }

        audioSources = GetComponents<AudioSource> ();
    }

    void Update () {
        UpdateMusic ();
    }

    void UpdateMusic () {
        for (int i = 0; i < audioSources.Length; i++) {
            if (SceneIntensity.Value >= i) {
                audioSources[i].volume = onVolume;
            } else {
                audioSources[i].volume = 0f;
            }
        }
    }
}