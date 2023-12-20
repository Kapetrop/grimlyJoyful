// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Classe gérant les états d'un ennemi
public class EnnemiEtatManager : MonoBehaviour
{
    // État actuel de l'ennemi
    private EnnemiEtatBase _etatActuel;

    // Définition des différents états de l'ennemi
    public EnnemiEtatRepos etatRepos = new EnnemiEtatRepos();
    public EnnemiEtatChasse etatChasse = new EnnemiEtatChasse();
    public EnnemiEtatAttaque etatAttaque = new EnnemiEtatAttaque();
    public EnnemiEtatPromenade etatPromenade = new EnnemiEtatPromenade();
    public EnnemiEtatMort etatMort = new EnnemiEtatMort();

    // Propriétés de l'ennemi
    [SerializeField] public AudioClip sonMort;
    [SerializeField] public AudioClip sonSpawn;
    private AudioSource _audioSource;
    public GameObject cible { get; set; }
    public GameObject home { get; set; }
    public NavMeshAgent agent { get; set; }
    public Animator animator { get; set; }
    public Transform goal { get; set; }
    public float range = 10000f;
    public GenerateurDiles generateur { get; set; }

    // Méthode appelée au démarrage du script
    void Start()
    {
        // Initialise l'agent de navigation et l'animateur
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Initialise l'audio source
        _audioSource = GetComponent<AudioSource>();

        // Change l'état initial de l'ennemi vers l'état de repos
        ChangerEtat(etatRepos);

        // Fait tourner l'ennemi avec une rotation aléatoire autour de l'axe Y
        agent.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
    }

    /// <summary>
    /// Change l'état actuel de l'ennemi vers un nouvel état spécifié.
    /// Initialise le nouvel état.
    /// </summary>
    /// <param name="etat">Nouvel état de l'ennemi.</param>
    public void ChangerEtat(EnnemiEtatBase etat)
    {
        _etatActuel = etat;
        etat.InitEtat(this);
    }
    public void JouerSon(AudioClip son)
    {
        _audioSource.PlayOneShot(son);
    }
    void OnTriggerEnter(Collider other)
    {
        // Appelle la méthode TriggerEnterEtat de l'état actuel
        _etatActuel.TriggerEnterEtat(this, other);
    }
}
