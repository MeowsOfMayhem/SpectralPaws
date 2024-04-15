using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicTrigger : MonoBehaviour
{

    public int musicId;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // Increase score by 1
            print("Music Change!");

            MusicManager.instance.PlayMusic(musicId); // dash

        }
    }

}
