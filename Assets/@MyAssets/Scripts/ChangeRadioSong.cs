using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadioSong : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;
    private AudioSource audioSource;
    private bool portalSongSounding;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = songs[0];
        audioSource.Play();
        portalSongSounding = true;
    }

    public void ChangeSong()
    {
        if(portalSongSounding)
        {
            audioSource.Stop();
            audioSource.clip = songs[1];
            audioSource.Play();
            portalSongSounding = false;
        }
        else
        {
            audioSource.Stop();
            audioSource.clip = songs[0];
            audioSource.Play();
            portalSongSounding = true;
        }
    }
}
