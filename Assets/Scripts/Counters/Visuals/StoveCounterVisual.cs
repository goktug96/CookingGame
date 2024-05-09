using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField]
    private StoveCounter stoveCounter;
    [SerializeField]
    private GameObject stoveOnGameObject;
    [SerializeField]
    private GameObject particlesGameObject;

    private void Start() {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
    }

    private void StoveCounter_OnStoveStateChanged(object sender, StoveCounter.OnStoveStateChangedEventArgs e) {
        if(e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried) {
            stoveOnGameObject.SetActive(true);
            particlesGameObject.SetActive(true);
        } else {
            stoveOnGameObject.SetActive(false);
            particlesGameObject.SetActive(false);
        }
    }    
}
