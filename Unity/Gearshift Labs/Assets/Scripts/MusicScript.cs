using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
  

    public AudioSource audioSource;
    public AudioClip[] songs;

    private void Start()
    {
        PlayRandomSong();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!audioSource.isPlaying)
        {
            // Play a new random song
            PlayRandomSong();
        }
    }
    private void PlayRandomSong()
    {
        // Check if there are songs in the array
        if (songs.Length == 0)
        {
            Debug.LogError("No songs found in the array!");
            return;
        }

        // Select a random song from the array
        int randomIndex = Random.Range(0, songs.Length);
        AudioClip randomSong = songs[randomIndex];

        // Set the selected song as the audio clip to play
        audioSource.clip = randomSong;

        // Play the selected song
        audioSource.Play();
    }
}
