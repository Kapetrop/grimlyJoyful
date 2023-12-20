using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEtatMort : BossEtatBase
{
    private GameObject _particuleObject;
    private bool _enMort = false;

    public override void InitEtat(BossEtatManager boss)
    {
        _particuleObject = (GameObject)Resources.Load("Particule/lootParticule");
       boss.animator.Play("DyingBoss");
       GameObject butin = (GameObject)Resources.Load("Loots/lb1");
       // Vérifie si le butin n'est pas nul
        if (butin != null)
        {
            // Lance une coroutine pour gérer l'apparition du butin
            Coroutine coroutMort = boss.StartCoroutine(CoroutineMort(butin,boss));
        }
        boss.JoueurSon(boss.sonMort);
    }

    public override void UpdateEtat(BossEtatManager boss)
    {
        
    }

    public override void TriggerEnterEtat(BossEtatManager boss, Collider col)
    {
       
    }
    /// <summary>
    /// Coroutine gérant la disparition visuelle de l'boss.
    /// </summary>
    IEnumerator CoroutineMort(GameObject butin, BossEtatManager boss)
    {
        Rigidbody rb = butin.GetComponent<Rigidbody>();
        boss.agent.speed = 0f;
        yield return null;
        if(!_enMort){
            boss.agent.enabled = false;
            butin = Object.Instantiate(butin, boss.transform.position, Quaternion.identity);
            butin.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            Object.Instantiate(_particuleObject, boss.transform.position, Quaternion.identity);
            _enMort = true;
            yield return new WaitForFixedUpdate();
            rb.AddForce(Vector3.up * 20f);
        }
        yield return new WaitForSeconds(1f);
        Object.Instantiate(_particuleObject, boss.transform.position, Quaternion.identity);
        boss.gameObject.SetActive(false);
    }
}