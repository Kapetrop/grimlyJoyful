using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoeursLoading : MonoBehaviour
{
    public GameObject[] coeurs;

    [SerializeField] private AudioClip soundBreaking;

    private AudioSource audioSource;

    private int currentIndex = 0;
    private float destructionTimer = 0f;
    public float timer = 7f;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {

        destructionTimer += Time.deltaTime;

        // Check if it's time to destroy the next object
        if (destructionTimer >= 1f && currentIndex < coeurs.Length)
        {
            Destroy(coeurs[currentIndex]);
            currentIndex++;
            destructionTimer = 0f; // Reset the timer for the next object

            //Joue son coeur qui brise
            if (audioSource != null && soundBreaking != null)
            {
                audioSource.PlayOneShot(soundBreaking);
            }
        }

        // Change la scene a la fin du temps
        if (destructionTimer >= timer)
        {
            SceneManager.LoadScene("SceneJeu");
        }
    }
}


