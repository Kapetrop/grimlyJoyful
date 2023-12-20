// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état d'attaque d'un ennemi, héritant de la classe de base EnnemiEtatBase
public class EnnemiEtatAttaque : EnnemiEtatBase
{
    // Méthode pour initialiser l'état d'attaque de l'ennemi
    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        // Active l'animation d'attaque dans le composant Animator de l'ennemi
        ennemi.animator.SetBool("enAttack", true);

        // Génère un temps aléatoire de retour après l'attaque
        float tempsDeRetour = Random.Range(1f, 3f);

        // Lance une coroutine pour gérer le retour à l'état de promenade après l'attaque
        Coroutine corout = ennemi.StartCoroutine(CoroutineAttaque(ennemi, tempsDeRetour));
    }

    // Méthode appelée à chaque mise à jour de l'état d'attaque de l'ennemi
    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    // Méthode appelée lorsqu'un objet entre en collision avec l'ennemi pendant son état d'attaque
    public override void TriggerEnterEtat(EnnemiEtatManager ennemi, Collider col)
    {
        // Vérifie si l'objet en collision a le tag "Arme"
        if (col.gameObject.tag == "Arme" || col.gameObject.tag == "ArmeAmeliorer")
        {
            Debug.Log("Toucher lors de l'attaque");
            // Change l'état de l'ennemi vers l'état de mort
            ennemi.StopCoroutine(CoroutineAttaque(ennemi, 0));
            ennemi.ChangerEtat(ennemi.etatMort);
        }
    }

    // Coroutine gérant le temps d'attaque et le retour à l'état de promenade
    IEnumerator CoroutineAttaque(EnnemiEtatManager ennemi, float tempsDeRetour)
    {
        // Attend le temps spécifié avant de désactiver l'animation d'attaque
        yield return new WaitForSeconds(tempsDeRetour);

        // Désactive l'animation d'attaque dans le composant Animator de l'ennemi
        ennemi.animator.SetBool("enAttack", false);

        // Change l'état de l'ennemi vers l'état de promenade
        ennemi.ChangerEtat(ennemi.etatPromenade);
    }
}
