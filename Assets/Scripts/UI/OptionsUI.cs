using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField]
    private Button soundEffectsButton, musicButton, closeButton;
    [SerializeField]
    private Button moveUpButton, moveDownButton, moveRightButton, moveLeftButton;
    [SerializeField]
    private Button interactButton, interactAltButton;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private TextMeshProUGUI soundEffectsText, musicText;
    [SerializeField]
    private TextMeshProUGUI moveUpText, moveDownText, moveRightText, moveLeftText;
    [SerializeField]
    private TextMeshProUGUI interactText, interactAltText;
    [SerializeField]
    private TextMeshProUGUI pauseText;
    [SerializeField]
    private Transform pressToRebindKey;

    private Action onCloseButtonAction;


    private void Awake() {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() => {
            onCloseButtonAction();
            Hide();
        });

        moveUpButton.onClick.AddListener(() => {
            RebindKey(GameInput.Binding.Move_Up);
        });

        moveDownButton.onClick.AddListener(() => {
            RebindKey(GameInput.Binding.Move_Down);
        });

        moveLeftButton.onClick.AddListener(() => {
            RebindKey(GameInput.Binding.Move_Left);
        });

        moveRightButton.onClick.AddListener(() => {
            RebindKey(GameInput.Binding.Move_Right);
        });

        interactButton.onClick.AddListener(() => {
            RebindKey(GameInput.Binding.Interact);
        });

        interactAltButton.onClick.AddListener(() => {
            RebindKey(GameInput.Binding.Interact_Alt);
        });

        pauseButton.onClick.AddListener(() => {
            RebindKey(GameInput.Binding.Pause);
        });
    }

    private void Start() {
        CookingGameManager.Instance.OnGameReset += CookingGameManager_OnGameReset;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void CookingGameManager_OnGameReset(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void ShowPressToRebindKey() {
        pressToRebindKey.gameObject.SetActive(true);
    }

    public void HidePressToRebindKey() {
        pressToRebindKey.gameObject.SetActive(false);
    }

    private void RebindKey(GameInput.Binding binding) {
        ShowPressToRebindKey();
        GameInput.Instance.Rebind(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
