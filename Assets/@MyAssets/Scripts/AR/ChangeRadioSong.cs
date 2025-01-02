using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadioSong : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;
    private AudioSource audioSource;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = songs[0];
        audioSource.Play();
    }

    public void ChangeSong()
    {
        counter++;
        audioSource.Stop();
        audioSource.clip = songs[counter % songs.Length];
        audioSource.Play();
    }
}
