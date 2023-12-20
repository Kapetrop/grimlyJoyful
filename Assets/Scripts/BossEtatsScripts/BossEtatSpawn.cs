using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEtatSpawn : BossEtatBase
{
    Coroutine _spawn;
  
    public override void InitEtat(BossEtatManager boss)
    {

       _spawn = boss.StartCoroutine(CoroutineSpawn(boss));
       boss.transform.localScale = new Vector3(0, 0, 0);
    }

    public override void UpdateEtat(BossEtatManager boss)
    {
        
    }

    public override void TriggerEnterEtat(BossEtatManager boss, Collider col)
    {
       
    }
    IEnumerator CoroutineSpawn(BossEtatManager boss)
    {
        while(boss.transform.localScale.x < 2)
        {
            boss.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            boss.transform.Rotate(0, 10, 0);
            yield return new WaitForSeconds(0.1f);
        }
 
        yield return new WaitForSeconds(1f);
        boss.ChangerEtat(boss.etatMarche);
    }
}
