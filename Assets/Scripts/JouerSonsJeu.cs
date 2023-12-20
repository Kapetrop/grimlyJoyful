using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JouerSonsJeu : MonoBehaviour
{
    
    [SerializeField]
    private AudioClip[] randomSounds;

    [SerializeField]
    private AudioClip backgroundSound;

    private AudioSource audioSource;
    private float minInterval = 2f;
    private float maxInterval = 5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Start playing the background sound on a loop
        audioSource.clip = backgroundSound;
        audioSource.loop = true;
        audioSource.Play();

        // Start playing random sounds with random intervals
        InvokeRepeating("PlayRandomSound", Random.Range(minInterval, maxInterval), Random.Range(minInterval, maxInterval));
    }

    private void PlayRandomSound()
    {
        if (randomSounds.Length == 0)
        {
            Debug.LogWarning("No random sounds assigned.");
            return;
        }

        // Choose a random sound from the array
        AudioClip randomSound = randomSounds[Random.Range(0, randomSounds.Length)];

        // Play the selected sound
        audioSource.PlayOneShot(randomSound);
    }
}
