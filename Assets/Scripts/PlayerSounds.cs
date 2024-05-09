using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {
    private float footStepTimer;
    private float footStepTimerMax = .1f;

    private void Update() {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer < 0f) {
            footStepTimer = footStepTimerMax;
            if (Player.Instance.IsWalking()) {
                SoundManager.Instance.PlayFootStepSound(Player.Instance.transform.position, 1f);
            }            
        }
    }
} 
