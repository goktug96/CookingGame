using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {
    private const string NUMBER_POPUP = "NumberPopup";
    
    [SerializeField]
    private TextMeshProUGUI countdownText;   

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start () {
        CookingGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update () {
        int countDownNumber = Mathf.CeilToInt(CookingGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countDownNumber.ToString();
        if(previousCountdownNumber != countDownNumber ) {
            previousCountdownNumber = countDownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (CookingGameManager.Instance.IsCountdownToStartActive()) {
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
