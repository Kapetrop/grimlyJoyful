using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AffichageUI : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] private SOPerso _donnees;  // Référence aux données du joueur.
    [SerializeField] private GenerateurDiles _donneesIles;

    [Header("UI Coeur")]
    [SerializeField] private GameObject[] _iconVie;  // Tableau d'icônes de vies visuelles.
    private int _vies;  // Nombre actuel de vies.

    [Header("UI Récoltable")]
    [SerializeField] private TextMeshProUGUI _texteTete;
    [SerializeField] private TextMeshProUGUI _texteMain;
    [SerializeField] private TextMeshProUGUI _textePied;
    [SerializeField] private TextMeshProUGUI _texteCerveau;
    [SerializeField] private TextMeshProUGUI _texteOs;

    [Header("UI potion")]
    [SerializeField] private TextMeshProUGUI _textePotionMauve;
    [SerializeField] private TextMeshProUGUI _textePotionRouge;

    [Header("UI Arme")]
    [SerializeField] private Image _imgFaucilleNormal;
    [SerializeField] private Image _imgFaucilleMauve;

    [Header("SpawnerRestants")]
    [SerializeField] private TextMeshProUGUI _texteSpawnerRestant;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // S'abonne à l'événement de mise à jour des données du joueur.
        _donnees.evenementMiseAJour.AddListener(Actualisation);
        _texteSpawnerRestant.text = "";
    }

    private void Update(){
        // ActuSpawner();
        InvokeRepeating("ActuSpawner", 1f, 5f);
    }

    public void ActuSpawner(){
        _texteSpawnerRestant.text = _donneesIles.textNbSpawner;
    }

    /// <summary>
    /// Méthode appelée lors de la mise à jour des données du joueur.
    /// </summary>
    private void Actualisation()
    {
        // Met à jour les textes des ressources récoltées.
        // _texteTete.text = _donnees.nbTete.ToString();
        // _texteMain.text = _donnees.nbMain.ToString();
        // _textePied.text = _donnees.nbPieds.ToString();
        // _texteCerveau.text = _donnees.nbCerveau.ToString();
        // _texteOs.text = _donnees.nbOs.ToString();
        // _textePotionMauve.text = _donnees.nbPotionMauve.ToString();
        // _textePotionRouge.text = _donnees.nbPotionRouge.ToString();

        //Vérifie si le nombre de points de vie a changé.
        if (_donnees.nbPointsDeVie != _vies)
        {
            // Désactive toutes les icônes de vie.
            for (int i = 0; i < _iconVie.Length; i++)
            {
                _iconVie[i].SetActive(false);
            }

            // Active les premières icônes en fonction du nombre de vies actuel.
            for (int i = 0; i < _donnees.nbPointsDeVie; i++)
            {
                if (i >= _iconVie.Length) break;  // Évite de dépasser la longueur du tableau.
                _iconVie[i].SetActive(true);
            }

            // Met à jour le nombre actuel de vies.
            _vies = _donnees.nbPointsDeVie;
        }
    }
}
