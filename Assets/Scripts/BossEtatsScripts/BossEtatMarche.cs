using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEtatMarche : BossEtatBase
{
    Coroutine marche;
    public override void InitEtat(BossEtatManager boss)
    {
        boss.animator.SetBool("enMarche", true);
       marche = boss.StartCoroutine(CoroutineMarche(boss));
    }

    public override void UpdateEtat(BossEtatManager boss)
    {
    
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
    IEnumerator CoroutineMarche(BossEtatManager boss)
    {
       boss.agent.destination = boss.cible.transform.position;
       

        while (boss.agent.remainingDistance < 2.5f && boss.agent.pathPending == true)
        {
            
            boss.agent.speed = 2f;
            boss.agent.destination = boss.cible.transform.position;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        boss.animator.SetBool("enMarche", false);
        boss.ChangerEtat(boss.etatAttaque);
    }
   
}
