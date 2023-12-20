using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEtatTouche : BossEtatBase
{
  
    public override void InitEtat(BossEtatManager boss)
    {
        boss.StartCoroutine(CoroutineToucher(boss));
        
    }

    public override void UpdateEtat(BossEtatManager boss)
    {
       
    }

    public override void TriggerEnterEtat(BossEtatManager boss, Collider col)
    {
    
    }
    IEnumerator CoroutineToucher(BossEtatManager boss)
    {
        boss.animator.Play("HitBoss");
        yield return new WaitForSeconds(1f);
        boss.ChangerEtat(boss.etatMarche);
    }

}