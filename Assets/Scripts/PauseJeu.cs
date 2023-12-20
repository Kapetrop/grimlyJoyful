using UnityEngine;

public class PauseJeu : MonoBehaviour
{
    public Canvas pauseCanvas;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PeserBouton()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }

    }

    public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0f;
        
        // Activate the pause canvas
        if (pauseCanvas != null)
        {
            pauseCanvas.gameObject.SetActive(true);
        }

        isPaused = true;
    }

    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1f;
        
        // Deactivate the pause canvas
        pauseCanvas.gameObject.SetActive(false);
        isPaused = false;
    }
}
