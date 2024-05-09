using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI recipesDeliveredText;

    private void Start() {
        CookingGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }  

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (CookingGameManager.Instance.IsGameOver()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        recipesDeliveredText.text = DeliveryManager.Instance.GetSuccesfullDeliveryCount().ToString();
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
