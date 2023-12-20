using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaccourciClavier : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Quit the aplication
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // Load first scene, quit the current game and progress
           SceneManager.LoadScene("SceneAccueil");
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Reload the same scene when playing the game
           if (SceneManager.GetActiveScene().name == "SceneJeu")
            {
                SceneManager.LoadScene("SceneJeu");
                Debug.Log("Reload scene jeu");
            }
        }
    }
}
