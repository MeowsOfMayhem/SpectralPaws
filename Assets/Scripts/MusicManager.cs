using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource musicBg;
    public AudioClip[] musicTrack;
    private int currentTrack;

    private void Start()
    {
        currentTrack = -1;
        PlayMusic(0);
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public void PlayMusic(int sfxToPlay)
    {
        if (sfxToPlay != currentTrack)
        {
            musicBg.clip = musicTrack[sfxToPlay];
            currentTrack = sfxToPlay;
            musicBg.Stop();
            musicBg.Play();
        }
    }

    public void StopMusic(int sfxToPlay)
    {
        musicBg.Stop();
    }


}
