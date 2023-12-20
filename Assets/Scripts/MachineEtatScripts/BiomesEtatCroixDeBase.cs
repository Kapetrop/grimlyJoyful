// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état d'une croix de base dans un biome, héritant de la classe de base BiomesEtatsBase
public class BiomesEtatCroixDeBase : BiomesEtatsBase
{

    // Matériaux associés à l'état de la croix de base dans le biome
    private Material _matBlocMort;
    private Material _matMorph;

    // Objet représentant la croix de base dans le biome
    private GameObject _propBloc;


    // Objet de particules associé à la croix de base
    private GameObject _particuleObject;

    /// <summary>
    /// Initialise l'état de la croix de base dans le biome.
    /// Charge les ressources nécessaires (particules, matériaux, modèle de croix de base),
    /// instancie le modèle de croix de base à la position du biome,
    /// l'assigne comme enfant de la hiérarchie des spawners du biome,
    /// puis modifie le matériau du biome.
    /// </summary>
    public override void InitEtat(BiomesEtatsManager biome)
    {
        // Charge l'objet de particules
        _particuleObject = (GameObject)Resources.Load("Particule/maParticule");

        // Charge le matériau de morphing
        _matMorph = (Material)Resources.Load("Materials_biome/morph");

        // Charge le modèle de croix de base
        GameObject blocMort = (GameObject)Resources.Load("Props/Spawner");

        // Vérifie si le modèle de croix de base est chargé
        if (blocMort != null)
        {
            // Instancie le modèle de croix de base à la position du biome
            _propBloc = GameObject.Instantiate(blocMort,biome._propSpot.position + new Vector3(0,.5f,0), Quaternion.identity);
            _propBloc.transform.localScale = new Vector3(Random.Range(0.6f, 1.1f), Random.Range(0.6f, 1.1f), Random.Range(0.6f, 1.1f));
            _propBloc.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            // Assigne le modèle de croix de base comme enfant de la hiérarchie des spawners du biome
            _propBloc.transform.parent = biome.Spawners.transform;

            // Instancie l'effet de particules à la position du biome
            GameObject part = GameObject.Instantiate(_particuleObject, biome.transform.position, Quaternion.identity);
            part.transform.parent = biome.Particules.transform;
        }

        // Charge le matériau de la croix de base
        _matBlocMort = (Material)Resources.Load("Materials_biome/beton");

        // Modifie le matériau du biome
        biome.GetComponent<Renderer>().material = _matBlocMort;
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de la croix de base dans le biome.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec la croix de base dans le biome.
    /// Laissée vide car rien n'est spécifié pour les collisions dans cet état.
    /// </summary>
    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider col)
    {
        // Laissée vide car rien n'est spécifié pour les collisions dans cet état
    }
}
