// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant l'état de mort avec blocage d'un biome, héritant de la classe de base BiomesEtatsBase
public class BiomesEtatMortBloc : BiomesEtatsBase
{
    // Matériaux associés à l'état de mort avec blocage d'un biome
    private Material _matBlocMort;
    private Material _matMorph;

    // Objet de bloc de mort associé à l'état de mort avec blocage
    private GameObject _propBloc;

    // Variables de contrôle pour la rotation et le changement de grosseur
    private bool _isRotating = false;
    private bool _changeDeGrosseur = false;

    // Objet de particules associé à l'état de mort avec blocage
    private GameObject _particuleObject;

    /// <summary>
    /// Initialise l'état de mort avec blocage d'un biome.
    /// Charge les ressources nécessaires (particules, matériaux, bloc de mort),
    /// puis lance la coroutine de changement d'état.
    /// </summary>
    public override void InitEtat(BiomesEtatsManager biome)
    {
        // Charge l'objet de particules
        _particuleObject = (GameObject)Resources.Load("Particule/maParticule");

        // Charge le matériau de morphing
        _matMorph = (Material)Resources.Load("Materials_biome/morph");

        // Charge le bloc de mort aléatoire
        GameObject blocMort = (GameObject)Resources.Load("Props/mb" + Random.Range(1, 4));
        if (blocMort != null)
        {
            // Instancie le bloc de mort à la position du biome
            _propBloc = Object.Instantiate(blocMort,biome._propSpot.position, Quaternion.identity);
        }

        // Charge le matériau de bloc de mort
        _matBlocMort = (Material)Resources.Load("Materials_biome/b2_v1");

        // Lance la coroutine de changement d'état
        Coroutine corout = biome.StartCoroutine(CoroutineChangeEtat(biome));

        // Applique le matériau de bloc de mort au renderer du biome
        biome.GetComponent<Renderer>().material = _matBlocMort;
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de mort avec blocage d'un biome.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'état de mort avec blocage d'un biome.
    /// Laissée vide car rien n'est spécifié pour les collisions avec cet état.
    /// </summary>
    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider col)
    {
        // Laissée vide car rien n'est spécifié pour les collisions avec cet état
    }

    /// <summary>
    /// Coroutine de changement d'état de mort avec blocage d'un biome.
    /// Réalise une séquence de changements visuels, incluant la rotation, le changement de matériau,
    /// l'effet de particules, puis rétablit l'état initial du biome.
    /// </summary>
    IEnumerator CoroutineChangeEtat(BiomesEtatsManager biome)
    {
        // Enregistre les valeurs initiales de la grosseur et de la rotation
        Vector3 grosseurOriginale = biome.transform.localScale;
        Vector3 rotationOriginale = biome.transform.rotation.eulerAngles;

        // Active la rotation et le changement de grosseur
        _isRotating = true;
        _changeDeGrosseur = true;

        // Lance la coroutine de rotation
        Coroutine coroutRotate = biome.StartCoroutine(CoroutineRotate(biome));
        yield return new WaitForSeconds(.5f);

        // Applique le matériau de morphing
        Coroutine coroutGrosseur = biome.StartCoroutine(CoroutineGrosseur(biome));
        biome.GetComponent<Renderer>().material = _matMorph;

        // Change la position de la valeur du y pendant la rotation
        biome.transform.position = new Vector3(biome.transform.position.x, biome.transform.position.y + 0.5f, biome.transform.position.z);

        // Attend 1 seconde
        yield return new WaitForSeconds(0.3f);

        // Désactive le changement de grosseur
        _changeDeGrosseur = false;

        // Instancie l'effet de particules à la position du biome
        GameObject part = GameObject.Instantiate(_particuleObject, biome.transform.position, Quaternion.identity);
        part.transform.parent = biome.Particules.transform;

        // Rétablit la grosseur initiale et le matériau de bloc de mort
        biome.transform.localScale = grosseurOriginale;
        biome.GetComponent<Renderer>().material = _matBlocMort;

        // Attend 1 seconde
        yield return new WaitForSeconds(1f);

        // Désactive la rotation
        _isRotating = false;

        // Réinitialise la rotation, la grosseur et la position initiale
        biome.transform.rotation = Quaternion.Euler(rotationOriginale);
        biome.transform.localScale = grosseurOriginale;
        biome.transform.position = new Vector3(biome.transform.position.x, biome.transform.position.y - 0.5f, biome.transform.position.z);
    }

    /// <summary>
    /// Coroutine de rotation progressive pendant l'état de mort avec blocage d'un biome.
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
    /// Coroutine de changement progressif de grosseur pendant l'état de mort avec blocage d'un biome.
    /// Instancie également l'effet de particules pendant le changement de grosseur.
    /// </summary>
    IEnumerator CoroutineGrosseur(BiomesEtatsManager biome)
    {

        // Applique le changement progressif de grosseur tant que la variable _changeDeGrosseur est vraie
        while (_changeDeGrosseur)
        {
            biome.transform.localScale = biome.transform.localScale * 0.5f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
