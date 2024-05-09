using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingGameManager : MonoBehaviour {

    public static CookingGameManager Instance {  get; private set; }

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 90f;
    private bool isGamePaused = false;

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameReset;

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f) {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer < 0f) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {                    
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:              
                break;
        }
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetCountdownToStartTimer() {
        return countdownTimer;
    }

    public float GetGamePlayingTimerNormalized() {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
    
    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            OnGameReset?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
