// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état d'un arbre vivant dans un biome, héritant de la classe de base BiomesEtatsBase
public class BiomesEtatArbreVivant : BiomesEtatsBase
{
    // Objet représentant l'arbre vivant dans le biome
    private GameObject _bloc;
    private Material _matArbreVivant;

    /// <summary>
    /// Initialise l'état de l'arbre vivant dans le biome.
    /// Charge un modèle d'arbre vivant aléatoire, l'instancie à la position du biome,
    /// l'assigne comme enfant de la hiérarchie des arbres du biome,
    /// puis modifie le matériau du biome.
    /// </summary>
    public override void InitEtat(BiomesEtatsManager biome)
    {
        // Charge un modèle d'arbre vivant aléatoire
        GameObject bloc = (GameObject)Resources.Load("Props/vb1");
        _matArbreVivant = (Material)Resources.Load("Props/avm"+ Random.Range(1, 5));

        // Vérifie si le modèle d'arbre vivant est chargé
        if (bloc != null)
        {

            // Instancie le modèle d'arbre vivant à la position du biome
            _bloc = Object.Instantiate(bloc,biome._propSpot.position, Quaternion.identity);
            _bloc.transform.localScale = new Vector3(_bloc.transform.localScale.x * Random.Range(0.6f, 1.1f), 
            _bloc.transform.localScale.y * Random.Range(0.6f, 1.1f),
            _bloc.transform.localScale.z * Random.Range(0.6f, 1.1f));
            _bloc.transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
            _bloc.GetComponent<Renderer>().material = _matArbreVivant;

            // Assigne le modèle d'arbre vivant comme enfant de la hiérarchie des arbres du biome
            _bloc.transform.parent = biome.Arbres.transform;
        }

        // Modifie le matériau du biome
        biome.GetComponent<Renderer>().material = biome.biomeMateriel;
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de l'arbre vivant dans le biome.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'arbre vivant dans le biome.
    /// Si l'objet en collision a le tag "champsDeForce", change l'état du biome vers l'état d'arbre mort
    /// et détruit le modèle d'arbre vivant.
    /// </summary>
    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider col)
    {
        // Vérifie si l'objet en collision a le tag "champsDeForce"
        if (col.CompareTag("champsDeForce"))
        {
            // Change l'état du biome vers l'état d'arbre mort
            biome.ChangerEtat(biome.etatArbreMort);

            // Détruit le modèle d'arbre vivant
            Object.Destroy(_bloc);
        }
    }
}
