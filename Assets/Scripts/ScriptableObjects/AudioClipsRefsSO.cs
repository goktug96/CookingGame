using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipsRefsSO : ScriptableObject {
    public AudioClip[] chop, deliverySuccess, deliveryFail, footstep, objectDrop, objectPickup, trash, warning;
    public AudioClip stoveSizzle;
}
