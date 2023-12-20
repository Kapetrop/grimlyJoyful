// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état de mort d'un ennemi, héritant de la classe de base EnnemiEtatBase
public class EnnemiEtatMort : EnnemiEtatBase
{
    private GameObject _particuleObject;
    bool _enMort = false;

    /// <summary>
    /// Initialise l'état de mort de l'ennemi.
    /// Charge un butin aléatoire, lance une coroutine pour gérer l'apparition du butin,
    /// et une autre coroutine pour gérer la disparition visuelle de l'ennemi.
    /// Active l'animation de mort dans le composant Animator de l'ennemi.
    /// </summary>
    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        _particuleObject = (GameObject)Resources.Load("Particule/lootParticule");
        // AudioClip sonMort = Resources.Load<AudioClip>("Audios/ZombieMort");
        // ennemi.JouerSon(sonMort);
        // Charge un butin aléatoire
        
        GameObject butin = (GameObject)Resources.Load("Loots/l" + Random.Range(1, 4));

        // Vérifie si le butin n'est pas nul
        if (butin != null)
        {
            // Lance une coroutine pour gérer l'apparition du butin
            Coroutine coroutMort = ennemi.StartCoroutine(CoroutineMort(butin,ennemi));
        }

       ennemi.JouerSon(ennemi.sonMort);
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de mort de l'ennemi.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'ennemi pendant son état de mort.
    /// Laissée vide car rien n'est spécifié pour les collisions dans cet état.
    /// </summary>
      public override void TriggerEnterEtat(EnnemiEtatManager ennemi, Collider col)
    {
    }

  
    /// <summary>
    /// Coroutine gérant la disparition visuelle de l'ennemi.
    /// </summary>
    IEnumerator CoroutineMort(GameObject butin, EnnemiEtatManager ennemi)
    {
        Rigidbody rb = butin.GetComponent<Rigidbody>();
        ennemi.agent.speed = 0f;
        yield return null;
        if(!_enMort){
            ennemi.agent.enabled = false;
            ennemi.animator.SetTrigger("EnMort");
            butin = Object.Instantiate(butin, ennemi.transform.position, Quaternion.identity);
            Object.Instantiate(_particuleObject, ennemi.transform.position, Quaternion.identity);
            _enMort = true;
            yield return new WaitForFixedUpdate();
            rb.AddForce(Vector3.up * 20f);
        }
        yield return new WaitForSeconds(1f);
        Object.Instantiate(_particuleObject, ennemi.transform.position, Quaternion.identity);
        ennemi.gameObject.SetActive(false);
    }
}
