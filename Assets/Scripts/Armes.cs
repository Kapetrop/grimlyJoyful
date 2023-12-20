using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armes : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        SpawnChecker spawnChecker = other.GetComponent<SpawnChecker>();
        if(spawnChecker != null)
        {
            if(!spawnChecker.estToucher)
            {
                spawnChecker.estToucher = true;
                GameObject.Destroy(other.gameObject);

            GetComponentInParent<MovePerso>().generateurDiles.EnleverSpawner();
            }
        }
        if(other.CompareTag("props"))
        {
            GameObject.Destroy(other.gameObject);
        }
    }
}
