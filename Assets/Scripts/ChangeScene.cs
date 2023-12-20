using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    //Classe permettant de changer de scène
    public void VersSceneJeu()
    {

        SceneManager.LoadScene("SceneJeu");
    }

    public void VersSceneFin()
    {
        SceneManager.LoadScene("SceneFin");
    }
    public void VersSceneMenu()
    {
        SceneManager.LoadScene("SceneAccueil");
    }
     public void VersSceneInfo()
    {
        SceneManager.LoadScene("SceneInstructions");
    }
     public void VersSceneButJeu()
    {
        SceneManager.LoadScene("SceneButJeu");
    }
}