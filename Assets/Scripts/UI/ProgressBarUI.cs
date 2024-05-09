using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour{
    [SerializeField]
    private Image barImage;
    [SerializeField]
    private GameObject hasProgressGameObject;

    private IHasProgress hasProgress;

    private void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null ) {
            Debug.LogError("Game object " + gameObject + " does not have a componenet that implements IHasProgress");
            return;
        }
        hasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {        
        barImage.fillAmount = e.progressNormalized;
        if(barImage.fillAmount == 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
