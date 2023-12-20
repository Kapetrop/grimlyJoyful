using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollector : MonoBehaviour
{
    [SerializeField] private MovePerso perso;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "fioleVie")
        {
            Destroy(other.gameObject);
            Debug.Log("fioleVie");
            perso.AjouterVie();
        }
        if (other.gameObject.tag == "fioleArme")
        {
            Destroy(other.gameObject);
            Debug.Log("fioleArme");
            perso.AmeliorerArme();
        }
        if(other.gameObject.tag == "fiolePower")
        {
            Debug.Log("fiolePower");
            perso.AgrandirArme(.50f);
            Destroy(other.gameObject);
            perso.GrossirSphere(10);
        }
    }
}
