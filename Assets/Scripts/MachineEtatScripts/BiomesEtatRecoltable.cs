// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état d'un biome récoltable, héritant de la classe de base BiomesEtatsBase
public class BiomesEtatRecoltable : BiomesEtatsBase
{
    /// <summary>
    /// Initialise l'état d'un biome récoltable.
    /// Affiche un message de débogage indiquant que le biome récoltable est apparu,
    /// puis applique le matériau du biome au renderer.
    /// </summary>
    public override void InitEtat(BiomesEtatsManager biome)
    {
        // Affiche un message de débogage indiquant que le biome récoltable est apparu

        // Applique le matériau du biome au renderer
        biome.GetComponent<Renderer>().material = biome.biomeMateriel;
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état d'un biome récoltable.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'état d'un biome récoltable.
    /// Laissée vide car rien n'est spécifié pour les collisions avec cet état.
    /// </summary>
    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider col)
    {
        // Laissée vide car rien n'est spécifié pour les collisions avec cet état
    }
}
