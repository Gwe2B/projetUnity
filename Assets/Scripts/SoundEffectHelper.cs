using System.Collections;
using UnityEngine;

public class SoundEffectHelper : MonoBehaviour
{
    public static SoundEffectHelper Instance;

    public AudioClip playerShotSound;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    public void MakePlayerShotSound()
    {
        MakeSound(playerShotSound);
    }

    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
