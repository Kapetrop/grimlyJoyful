using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEtatAttaque : BossEtatBase
{
    public override void InitEtat(BossEtatManager boss)
    {
       // Active l'animation d'attaque dans le composant Animator de l'boss
        boss.animator.SetBool("enAttack", true);

        // Génère un temps aléatoire de retour après l'attaque
        float tempsDeRetour = Random.Range(1f, 3f);

        // Lance une coroutine pour gérer le retour à l'état de promenade après l'attaque
        Coroutine corout = boss.StartCoroutine(CoroutineAttaque(boss, tempsDeRetour));
    }

    public override void UpdateEtat(BossEtatManager boss)
    {
        if(boss.vieBoss <= 0)
        {
            
            boss.ChangerEtat(boss.etatMort);
        }
    }

    public override void TriggerEnterEtat(BossEtatManager boss, Collider col)
    {
       if (col.gameObject.tag == "ArmeAmeliorer")
        {
            Debug.Log("Toucher lors de la chasse");
            // Change l'état de l'boss vers l'état de mort
            boss.EnleverVie(2, boss);
        }
        if(col.gameObject.tag == "Arme")
        {
            Debug.Log("Toucher lors de la chasse");
            boss.EnleverVie(1, boss);
        }
    }
    IEnumerator CoroutineAttaque(BossEtatManager boss, float tempsDeRetour)
    {
        // Attend le temps spécifié avant de désactiver l'animation d'attaque
        yield return new WaitForSeconds(tempsDeRetour);

        // Désactive l'animation d'attaque dans le composant Animator de l'boss
        boss.animator.SetBool("enAttack", false);

        // Change l'état de l'boss vers l'état de promenade
        boss.ChangerEtat(boss.etatMarche);
    }
}
