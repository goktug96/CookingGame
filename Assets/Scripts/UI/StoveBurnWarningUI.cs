using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour {

    [SerializeField]
    private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        Debug.Log(stoveCounter.IsBurning());
        float burnShowProgressAmount = .5f;
        if(stoveCounter.IsBurning() && e.progressNormalized >= burnShowProgressAmount) {            
            Show();
        } else {
            Hide();
        } 
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
