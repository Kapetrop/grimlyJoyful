using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    [SerializeField] private SOPerso _donnees;
    public void AjouterTete()
    {
        _donnees.nbTete++;
    }
    public void EnleverTete()
    {
        _donnees.nbTete--;
    }
    public void AjouterMain()
    {
        _donnees.nbMain++;
    }
    public void EnleverMain()
    {
        _donnees.nbMain--;
    }
    public void AjouterPied()
    {
        _donnees.nbPieds++;
    }
    public void EnleverPied()
    {
        _donnees.nbPieds--;
    }
    public void AjouterCerveau()
    {
        _donnees.nbCerveau++;
    }
    public void EnleverCerveau()
    {
        _donnees.nbCerveau--;
    }
    public void AjouterPotionMauve()
    {
        _donnees.nbPotionMauve++;
    }
    public void EnleverPotionMauve()
    {
        _donnees.nbPotionMauve--;
    }
    public void AjouterPotionRouge()
    {
        _donnees.nbPotionRouge++;
    }
    public void EnleverPotionRouge()
    {
        _donnees.nbPotionRouge--;
    }
    public void AjouterPointDeVie()
    {
        _donnees.nbPointsDeVie++;
    }
}
