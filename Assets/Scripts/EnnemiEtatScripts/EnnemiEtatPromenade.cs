// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état de promenade d'un ennemi, héritant de la classe de base EnnemiEtatBase
public class EnnemiEtatPromenade : EnnemiEtatBase
{
    /// <summary>
    /// Initialise l'état de promenade de l'ennemi.
    /// Active l'animation de marche dans le composant Animator de l'ennemi
    /// et lance une coroutine pour gérer le retour à la position de départ.
    /// </summary>
    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        // Active l'animation de marche dans le composant Animator de l'ennemi
        ennemi.animator.SetBool("enMarche", true);

        // Lance une coroutine pour gérer le retour à la position de départ
        Coroutine corout = ennemi.StartCoroutine(CoroutineRetour(ennemi));
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de promenade de l'ennemi.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'ennemi pendant son état de promenade.
    /// Vérifie si l'objet en collision a le tag "Arme" et change l'état de l'ennemi vers l'état de mort.
    /// </summary>
      public override void TriggerEnterEtat(EnnemiEtatManager ennemi, Collider col)
    {

        // Vérifie si l'objet en collision a le tag "Arme"
        if (col.gameObject.tag == "Arme" || col.gameObject.tag == "ArmeAmeliorer")
        {
            Debug.Log("Toucher lors de la promenade");
            // Change l'état de l'ennemi vers l'état de mort
            ennemi.StopCoroutine(CoroutineRetour(ennemi));
            ennemi.ChangerEtat(ennemi.etatMort);
        }
    }

    /// <summary>
    /// Coroutine gérant le retour de l'ennemi à sa position de départ.
    /// Ajuste la vitesse de l'agent, défini la destination comme la position de départ,
    /// attend que l'agent atteigne sa destination, désactive l'animation de marche,
    /// attend un court laps de temps, puis change l'état de l'ennemi vers l'état de repos.
    /// </summary>
    IEnumerator CoroutineRetour(EnnemiEtatManager ennemi)
    {
        // Ajuste la vitesse de l'agent pour la promenade
        ennemi.agent.speed = 5f;

        // Défini la destination de l'agent comme la position de départ (home) de l'ennemi
        ennemi.agent.destination = ennemi.home.transform.position;

        // Attend que l'agent atteigne sa destination et que le chemin ne soit pas en cours de calcul
        while (ennemi.agent.remainingDistance < 2.5f && ennemi.agent.pathPending == false)
        {
            // Attend une courte période avant de vérifier à nouveau
            yield return new WaitForSeconds(0.2f);
        }

        // Désactive l'animation de marche
        ennemi.animator.SetBool("enMarche", false);

        // Attend un court laps de temps
        yield return new WaitForSeconds(1f);

        // Change l'état de l'ennemi vers l'état de repos
        ennemi.ChangerEtat(ennemi.etatRepos);
    }
}
