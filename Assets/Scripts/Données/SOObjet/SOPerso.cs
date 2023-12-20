using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Perso", menuName = "Perso")]
public class SOPerso : ScriptableObject
{
    [Header("Valeurs initiales")]
    [SerializeField] int _nbPointsDeVieIni = 6;
    [SerializeField] int _nbCerveauIni = 0;
    [SerializeField] int _nbMainini = 0;
    [SerializeField] int _nbPiedsIni = 0;
    [SerializeField] int _nbTeteIni = 0;
    [SerializeField] int _nbOsIni = 0;
    [SerializeField] int _nbPotionMauveIni = 0;
    [SerializeField] int _nbPotionRougeIni = 0;



    [Header("Valeurs actuelles")]
    [SerializeField] int _nbPointsDeVie;
    [SerializeField] int _nbCerveau;
    [SerializeField] int _nbMain;
    [SerializeField] int _nbPieds;
    [SerializeField] int _nbTete;
    [SerializeField] int _nbOs;
    [SerializeField] int _nbPotionMauve;
    [SerializeField] int _nbPotionRouge;

    public int nbPointsDeVie
    {
        get => _nbPointsDeVie;
        set 
        {
            _nbPointsDeVie = value;
           _evenementMiseAJour.Invoke();
        }
    }
    public int nbCerveau
    {
        get => _nbCerveau;
        set 
        {
            _nbCerveau = value;
           _evenementMiseAJour.Invoke();
        }
    }
    public int nbMain
    {
        get => _nbMain;
        set 
        {
            _nbMain = value;
           _evenementMiseAJour.Invoke();
        }
    }
    public int nbPieds
    {
        get => _nbPieds;
        set 
        {
            _nbPieds = value;
           _evenementMiseAJour.Invoke();
        }
    }
    public int nbTete
    {
        get => _nbTete;
        set 
        {
            _nbTete = value;
           _evenementMiseAJour.Invoke();
        }
    }
    public int nbPotionMauve
    {
        get => _nbPotionMauve;
        set 
        {
            _nbPotionMauve = value;
           _evenementMiseAJour.Invoke();
        }
    }
    public int nbPotionRouge
    {
        get => _nbPotionRouge;
        set 
        {
            _nbPotionRouge = value;
           _evenementMiseAJour.Invoke();
        }
    }
    public int nbOs
    {
        get => _nbOs;
        set 
        {
            _nbOs = value;
           _evenementMiseAJour.Invoke();
        }
    }

    private UnityEvent _evenementMiseAJour = new UnityEvent();
    public UnityEvent evenementMiseAJour => _evenementMiseAJour;

    public void Initialiser()
    {
        _nbPointsDeVie = _nbPointsDeVieIni;
        _nbCerveau = _nbCerveauIni;
        _nbMain = _nbMainini;
        _nbPieds = _nbPiedsIni;
        _nbTete = _nbTeteIni;
        _nbOs = _nbOsIni;
        _nbPotionMauve = _nbPotionMauveIni;
        _nbPotionRouge = _nbPotionRougeIni;
    }

    void OnValidate()
    {
        _evenementMiseAJour.Invoke();
    }
}
