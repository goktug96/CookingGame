using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour{

    [SerializeField]
    private Button mainMenuButton, resetButton, optionsButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        resetButton.onClick.AddListener(() => {
            CookingGameManager.Instance.TogglePauseGame();
        });
        optionsButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }

    private void Start() {
        CookingGameManager.Instance.OnGamePaused += CookingGame_OnGamePaused;
        CookingGameManager.Instance.OnGameReset += CookingGame_OnGameReset;        

        Hide();
    }

    private void CookingGame_OnGameReset(object sender, System.EventArgs e) {
        Hide();
    }

    private void CookingGame_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
