// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Classe définissant l'état de mort d'un biome, héritant de la classe de base BiomesEtatsBase
public class BiomesEtatMort : BiomesEtatsBase
{
    // Objet de particules associé à l'état de mort
    private GameObject _particuleObject;

    // Matériaux associés à l'état de mort d'un biome
    private Material _matMort;
    private Material _matMorph;

    // Variable de contrôle pour la rotation
    private bool _isRotating = false;

    /// <summary>
    /// Initialise l'état de mort d'un biome.
    /// Charge les ressources nécessaires (particules, matériaux),
    /// détermine aléatoirement si le biome doit être bloqué, puis lance la coroutine de changement d'état.
    /// </summary>
    public override void InitEtat(BiomesEtatsManager biome)
    {
        biome.generateurDiles.EnleverBiomeVivant();
        // Charge l'objet de particules
        _particuleObject = (GameObject)Resources.Load("Particule/maParticule");

        // Charge le matériau de mort
        _matMort = (Material)Resources.Load("Materials_biome/mort");

        // Charge le matériau de morphing
        _matMorph = (Material)Resources.Load("Materials_biome/morph");

        // Détermine aléatoirement si le biome doit être bloqué (5% de chance)
        int chanceBlocquer = Random.Range(0, 100);

        // Vérifie si le biome doit être bloqué
        if (chanceBlocquer < 5)
        {
        
            Debug.Log("Bloc");
            // Change l'état du biome vers l'état de mort avec blocage
            biome.ChangerEtat(biome.etatMortBloc);
        }
        else
        {
            // Lance la coroutine de changement d'état
            Coroutine corout = biome.StartCoroutine(CoroutineChangeEtat(biome));
        }
    }

    /// <summary>
    /// Coroutine de changement d'état d'un biome.
    /// Réalise une séquence de changements visuels, incluant la rotation, le changement de matériau,
    /// l'effet de particules, puis rétablit l'état initial du biome.
    /// </summary>
    IEnumerator CoroutineChangeEtat(BiomesEtatsManager biome)
    {
        // Enregistre les valeurs initiales de la grosseur et de la rotation
        Vector3 grosseurOriginale = biome.transform.localScale;
        Vector3 rotationOriginale = biome.transform.rotation.eulerAngles;

        // Active la rotation
        _isRotating = true;
       

         // Applique le matériau de morphing
        biome.GetComponent<Renderer>().material = _matMorph;
        yield return new WaitForSeconds(.3f);
        // Lance la coroutine de rotation
        Coroutine coroutRotate = biome.StartCoroutine(CoroutineRotate(biome));
        
        yield return new WaitForSeconds(1f);
          // Désactive la rotation
        _isRotating = false;

        // Instancie l'effet de particules à la position du biome
        GameObject part = Object.Instantiate(_particuleObject, biome.transform.position, Quaternion.identity);
        part.name = "Particule";
        part.transform.parent = biome.Particules.transform;
        // Rétablit la grosseur initiale
        biome.transform.localScale = grosseurOriginale;

        // Rétablit le matériau de mort
        biome.GetComponent<Renderer>().material = _matMort;
        // Réinitialise la rotation, la grosseur et la position initiale
        biome.transform.rotation = Quaternion.Euler(rotationOriginale);
        biome.transform.localScale = grosseurOriginale;



    }

    /// <summary>
    /// Coroutine de rotation d'un biome.
    /// Effectue une rotation continue de 30 degrés autour de l'axe y.
    /// </summary>
    IEnumerator CoroutineRotate(BiomesEtatsManager biome)
    {
         while(_isRotating)
        {
            // Effectue une rotation de 1 degré sur l'axe y
            biome.transform.Rotate(new Vector3(-1,0,1) * 4.5f, Space.World);

            // Attend 0.01 seconde
            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    /// Appelée à chaque mise à jour de l'état de mort d'un biome.
    /// Laissée vide car rien n'est spécifié pour la mise à jour de cet état.
    /// </summary>
    public override void UpdateEtat(BiomesEtatsManager biome)
    {
        // Laissée vide car rien n'est spécifié pour la mise à jour de cet état
    }

    /// <summary>
    /// Appelée lorsqu'un objet entre en collision avec l'état de mort d'un biome.
    /// Change l'état du biome vers l'état du générateur si l'objet a le tag "Generateur".
    /// </summary>
    public override void TriggerEnterEtat(BiomesEtatsManager biome, Collider col)
    {
        // Vérifie si l'objet qui entre en collision a le tag "Generateur"
        if (col.CompareTag("Generateur"))
        {
            // Change l'état du biome vers l'état du générateur
            biome.ChangerEtat(biome.etatGenerateur);
        }
    }
}
