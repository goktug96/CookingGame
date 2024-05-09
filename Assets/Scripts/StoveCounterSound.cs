using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    [SerializeField]
    private StoveCounter stoveCounter;

    private AudioSource audioSource;

    private bool on = false;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged1; ;
    }

    private void StoveCounter_OnStoveStateChanged1(object sender, StoveCounter.OnStoveStateChangedEventArgs e) {
        if (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried) {
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }
}
