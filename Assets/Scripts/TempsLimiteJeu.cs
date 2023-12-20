using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempsLimiteJeu : MonoBehaviour
{

    [SerializeField] private AudioClip soundDefaite;

    private AudioSource audioSource;

    [SerializeField] private Text textTempsLimite;

    public float timerJeu = 300f; // 5 mins

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        timerJeu -= Time.deltaTime;
        int minuteJeu = Mathf.RoundToInt(timerJeu/60)-1;
        int secondesJeu = Mathf.RoundToInt(timerJeu%60);
        textTempsLimite.text = ""+minuteJeu+":"+secondesJeu;
        if(timerJeu <= 0){
            StopGame();
        }
    }

    private void StopGame()
    {
        SceneManager.LoadScene("SceneFin");
        audioSource.PlayOneShot(soundDefaite);
    }
}
