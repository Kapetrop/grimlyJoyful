// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état de repos d'un ennemi, héritant de la classe de base EnnemiEtatBase
public class EnnemiEtatRepos : EnnemiEtatBase
{
    /// <summary>
    /// Initialise l'état de repos de l'ennemi.
    /// Lance une coroutine pour gérer le passage à l'état de marche après une pause.
    /// </summary>
    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        ennemi.JouerSon(ennemi.sonSpawn);
        // Lance une coroutine pour gérer le passage à l'état de marche après une pause
        Coroutine corout = ennemi.StartCoroutine(CoroutineMarcher(ennemi));
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de repos de l'ennemi.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'ennemi pendant son état de repos.
    /// Vérifie si l'objet en collision a le tag "Arme" et change l'état de l'ennemi vers l'état de mort.
    /// </summary>
     public override void TriggerEnterEtat(EnnemiEtatManager ennemi, Collider col)
    {
        // Vérifie si l'objet en collision a le tag "Arme"
        if (col.gameObject.tag == "Arme" || col.gameObject.tag == "ArmeAmeliorer")
        {
            Debug.Log("Toucher lors du repos");
            // Change l'état de l'ennemi vers l'état de mort
            ennemi.StopCoroutine(CoroutineMarcher(ennemi));
            ennemi.ChangerEtat(ennemi.etatMort);
        }
    }

    /// <summary>
    /// Coroutine gérant le passage de l'ennemi à l'état de marche après une pause.
    /// Attend une durée définie, puis change l'état de l'ennemi vers l'état de chasse.
    /// </summary>
    IEnumerator CoroutineMarcher(EnnemiEtatManager ennemi)
    {
        // Attend une durée de 5 secondes
        yield return new WaitForSeconds(5);

        // Change l'état de l'ennemi vers l'état de chasse
        ennemi.ChangerEtat(ennemi.etatChasse);
    }
}
