// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état d'un arbre mort dans un biome, héritant de la classe de base BiomesEtatsBase
public class BiomesEtatArbreMort : BiomesEtatsBase
{
    // Matériaux utilisés pour l'état de l'arbre mort
    private Material _matBlocMort;
    private Material _matArbreMort;
    private Material _matMorph;
    private GameObject _blocMort;

    // Variables de contrôle pour la rotation et le changement de grosseur
    private bool _isRotating = false;
    private bool _changeDeGrosseur = false;


    /// <summary>
    /// Initialise l'état de l'arbre mort dans le biome.
    /// Charge les ressources nécessaires (particules, matériaux, modèle d'arbre mort),
    /// lance des coroutines pour gérer les transitions d'état, et modifie le matériau du biome.
    /// </summary>
    public override void InitEtat(BiomesEtatsManager biome)
    {
        // Charge le matériau de morphing
        _matMorph = (Material)Resources.Load("Materials_biome/morph");

        // Charge le matériau de l'arbre mort
        _matBlocMort = (Material)Resources.Load("Materials_biome/beton");

        // Lance une coroutine pour gérer les transitions d'état
        Coroutine corout = biome.StartCoroutine(CoroutineChangeEtat(biome));

        // Modifie le matériau du biome pour celui de l'arbre mort
        biome.GetComponent<Renderer>().material = _matBlocMort;
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de l'arbre mort dans le biome.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'arbre mort dans le biome.
    /// Laissée vide car rien n'est spécifié pour les collisions dans cet état.
    /// </summary>
    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider col)
    {
        // Laissée vide car rien n'est spécifié pour les collisions dans cet état
    }

    /// <summary>
    /// Coroutine gérant les transitions d'état de l'arbre mort dans le biome.
    /// Ajuste la rotation, la grosseur, le matériau, et déclenche l'effet de particules.
    /// </summary>
    IEnumerator CoroutineChangeEtat(BiomesEtatsManager biome)
    {
        // Sauvegarde la taille et la rotation d'origine du biome
        Vector3 grosseurOriginale = biome.transform.localScale;
        Vector3 rotationOriginale = biome.transform.rotation.eulerAngles;

        // Active la rotation et le changement de grosseur
        _isRotating = true;
        _changeDeGrosseur = true;

        // Lance une coroutine pour gérer la rotation
        Coroutine coroutRotate = biome.StartCoroutine(CoroutineRotate(biome));

        // Attend une courte période avant de continuer
        yield return new WaitForSeconds(.5f);

        // Applique un nouveau matériau au biome
        Coroutine coroutGrosseur = biome.StartCoroutine(CoroutineGrosseur(biome));
        biome.GetComponent<Renderer>().material = _matMorph;

        // Ajuste la position du biome pendant la rotation
        biome.transform.position = new Vector3(biome.transform.position.x, biome.transform.position.y + 0.5f, biome.transform.position.z);

        // Attend une courte période avant de continuer
        yield return new WaitForSeconds(0.3f);

        // Désactive le changement de grosseur
        _changeDeGrosseur = false;

        // Rétablit la taille d'origine du biome
        biome.transform.localScale = grosseurOriginale;

        // Rétablit le matériau d'origine du biome
        biome.GetComponent<Renderer>().material = _matBlocMort;

        // Attend une courte période avant de continuer
        yield return new WaitForSeconds(1f);

        // Désactive la rotation
        _isRotating = false;

        // Réinitialise la rotation, la taille et la position d'origine du biome
        biome.transform.rotation = Quaternion.Euler(rotationOriginale);
        biome.transform.localScale = grosseurOriginale;
        biome.transform.position = new Vector3(biome.transform.position.x, biome.transform.position.y - 0.5f, biome.transform.position.z);
        PlacerArbre(biome);
    }

    /// <summary>
    /// Coroutine gérant la rotation de l'arbre mort dans le biome.
    /// </summary>
    IEnumerator CoroutineRotate(BiomesEtatsManager biome)
    {
        while (_isRotating)
        {
            biome.transform.Rotate(30, 0, 30);
            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    /// Coroutine gérant le changement de taille de l'arbre mort dans le biome,
    /// et déclenche l'effet de particules à la fin.
    /// </summary>
    IEnumerator CoroutineGrosseur(BiomesEtatsManager biome)
    {
        while (_changeDeGrosseur)
        {
            // Réduit la taille du biome
            biome.transform.localScale = biome.transform.localScale * 0.5f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void PlacerArbre(BiomesEtatsManager biome)
    {
        
        _matArbreMort = (Material)Resources.Load("Props/amm"+Random.Range(1, 5));
        // Charge le modèle d'arbre mort
         GameObject blocMort = (GameObject)Resources.Load("Props/am1");
        if (blocMort != null)
        {
            // Instancie le modèle d'arbre mort à la position du biome
            _blocMort = Object.Instantiate(blocMort,biome._propSpot.position, Quaternion.identity);
            _blocMort.transform.localScale = new Vector3(_blocMort.transform.localScale.x * Random.Range(0.6f, 1.1f), 
            _blocMort.transform.localScale.y * Random.Range(0.6f, 1.1f),
            _blocMort.transform.localScale.z * Random.Range(0.6f, 1.1f));

            _blocMort.transform.rotation = Quaternion.Euler(-90, Random.Range(0, 360), 0);
            _blocMort.GetComponent<Renderer>().material = _matArbreMort;
            _blocMort.transform.parent = biome.Arbres.transform;


        }

    }
}
