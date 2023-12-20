// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état de chasse d'un ennemi, héritant de la classe de base EnnemiEtatBase
public class EnnemiEtatChasse : EnnemiEtatBase
{
    /// <summary>
    /// Initialise l'état de chasse de l'ennemi.
    /// Active l'animation de chasse dans le composant Animator de l'ennemi
    /// et lance une coroutine pour gérer le comportement de chasse.
    /// </summary>
    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        ennemi.animator.SetBool("enChasse", true);
        Coroutine corout =ennemi.StartCoroutine(CoroutineEnChasse(ennemi));
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de chasse de l'ennemi.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'ennemi pendant son état de chasse.
    /// Vérifie si l'objet en collision a le tag "Arme" et change l'état de l'ennemi vers l'état de mort.
    /// </summary>
     public override void TriggerEnterEtat(EnnemiEtatManager ennemi, Collider col)
    {

        // Vérifie si l'objet en collision a le tag "Arme"
        if (col.gameObject.CompareTag("Arme") || col.gameObject.tag == "ArmeAmeliorer")
        {
            Debug.Log("Toucher lors de la chasse");
            // Change l'état de l'ennemi vers l'état de mort
            ennemi.StopCoroutine(CoroutineEnChasse(ennemi));
            ennemi.ChangerEtat(ennemi.etatMort);
        }
    }

    /// <summary>
    /// Coroutine gérant le comportement de chasse de l'ennemi.
    /// Définit la destination de l'ennemi comme étant la position de sa cible,
    /// modifie la vitesse de l'agent et met à jour sa destination en continu,
    /// désactive l'animation de chasse après un certain temps,
    /// et change l'état de l'ennemi vers l'état d'attaque.
    /// </summary>
    IEnumerator CoroutineEnChasse(EnnemiEtatManager ennemi)
    {
        ennemi.agent.destination = ennemi.cible.transform.position;
       

        while (ennemi.agent.remainingDistance < 2.5f && ennemi.agent.pathPending == true)
        {
            
            ennemi.agent.speed = 18f;
            ennemi.agent.destination = ennemi.cible.transform.position;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        ennemi.animator.SetBool("enChasse", false);
        ennemi.ChangerEtat(ennemi.etatAttaque);
    }
}
