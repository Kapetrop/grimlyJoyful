// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état d'un générateur dans un biome, héritant de la classe de base BiomesEtatsBase
public class BiomesEtatGenerateur : BiomesEtatsBase
{
    // Matériau associé à l'état du générateur dans le biome
    private Material _generateurMat;

    /// <summary>
    /// Initialise l'état du générateur dans le biome.
    /// Charge le matériau spécifique au générateur, modifie le matériau du biome,
    /// et utilise le générateur de créatures du biome pour placer une nouvelle créature.
    /// </summary>
    public override void InitEtat(BiomesEtatsManager biome)
    {
        // Charge le matériau spécifique au générateur
        _generateurMat = (Material)Resources.Load("Materials_biome/b3_v1");

        // Modifie le matériau du biome avec celui du générateur
        biome.GetComponent<Renderer>().material = _generateurMat;

        Coroutine corout = biome.StartCoroutine(PlacerEnnemi(biome, Random.Range(1f, 3.5f)));
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état du générateur dans le biome.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec le générateur dans le biome.
    /// Laissée vide car rien n'est spécifié pour les collisions dans cet état.
    /// </summary>
    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider col)
    {
        // Laissée vide car rien n'est spécifié pour les collisions dans cet état
    }
    IEnumerator PlacerEnnemi(BiomesEtatsManager biome, float tempsAvantSpawn)
    {
        yield return new WaitForSeconds(tempsAvantSpawn);
        biome.generateurDiles.PlacerUnEnnemi(biome.transform);
    }
}
