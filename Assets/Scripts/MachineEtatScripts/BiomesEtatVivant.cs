using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class BiomesEtatVivant : BiomesEtatsBase
{
    GameObject _propVivant;

    /// <summary>
    /// Initialise l'état du biome.
    /// </summary>
    /// <param name="biome">L'instance BiomesEtatsManager représentant le biome.</param>
    public override void InitEtat(BiomesEtatsManager biome)
    {
        biome.generateurDiles.AjouterBiomeVivant();
        // Génère un nombre aléatoire entre 0 et 100
        int vaBloquer = Random.Range(0, 500);

        // Si le nombre aléatoire est inférieur à 5, change l'état du biome en état d'arbre vivant
        if (vaBloquer < 2)
        {
            Debug.Log("Arbre");
            biome.ChangerEtat(biome.etatArbreVivant);
        }

        // Charge un objet vivant aléatoire depuis la ressource "Props" avec un numéro entre 1 et 9
        GameObject propVivant = (GameObject)Resources.Load("Props/v" + Random.Range(1, 10));

        // Si l'objet vivant est chargé avec succès, l'instancie à la position du biome et le place sous l'objet Herbes du biome
        if (propVivant != null)
        {
            _propVivant = GameObject.Instantiate(propVivant,biome._propSpot.position, Quaternion.identity);
            _propVivant.transform.localScale = new Vector3(Random.Range(0.6f,1.1f), Random.Range(0.6f,1.1f), Random.Range(0.6f,1.1f));
            _propVivant.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            _propVivant.transform.parent = biome.Herbes.transform;
        }

        // Applique un matériau au Renderer du biome
        biome.GetComponent<Renderer>().material = biome.biomeMateriel;

        // Rotation du biome (commentée)
        biome.transform.rotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);
    }

    /// <summary>
    /// Met à jour l'état du biome.
    /// </summary>
    /// <param name="biome">L'instance BiomesEtatsManager représentant le biome.</param>
    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        // Cette méthode est actuellement vide
    }

    /// <summary>
    /// Appelée lorsqu'un autre objet entre en collision avec le biome.
    /// </summary>
    /// <param name="biome">L'instance BiomesEtatsManager représentant le biome.</param>
    /// <param name="col">Le Collider de l'objet entré en collision avec le biome.</param>
    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider col)
    {
        // Si l'objet en collision a le tag "champsDeForce", change l'état du biome en état de mort
        if (col.CompareTag("champsDeForce"))
        {
            biome.ChangerEtat(biome.etatMort);
        }

        // Si l'objet en collision a le tag "Generateur", change l'état du biome en état de générateur
        if (col.CompareTag("Generateur"))
        {
            biome.ChangerEtat(biome.etatGenerateur);
        }

        // Détruit l'objet vivant associé au biome
        GameObject.Destroy(_propVivant);
    }
}
