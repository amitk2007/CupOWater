using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundNames
{
    clickButton,
    Hit,
    CoinsThrow,
    CoinCollect
}
public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip currentSound;
    static AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlaySound(SoundNames soundName)
    {
        currentSound = Resources.Load<AudioClip>("Sounds/" + soundName.ToString());
        audioSource.PlayOneShot(currentSound);
    }
}
