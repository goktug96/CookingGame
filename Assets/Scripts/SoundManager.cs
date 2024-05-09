using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private const string PLAYER_PREFS_SOUND_EFFECT_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField]
    private AudioClipsRefsSO audioClipsRefsSO;

    private float volume = 1f;

    private void Awake() {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, 1f);
    }

    private void Start () {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipsRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e) {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(audioClipsRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e) {
        PlaySound(audioClipsRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        PlaySound(audioClipsRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        PlaySound(audioClipsRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(audioClipsRefsSO.chop, cuttingCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArr, Vector3 position, float volumeMultiplier = 1f) {
        PlaySound(audioClipArr[Random.Range(0, audioClipArr.Length)], position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootStepSound(Vector3 position, float volume) {
        PlaySound(audioClipsRefsSO.footstep, position, volume);
    }

    public void PlayCountdownSound() {
        PlaySound(audioClipsRefsSO.warning, Vector3.zero);
    }

    public void ChangeVolume() {
        volume += .1f;
        if(volume > 1f) {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
