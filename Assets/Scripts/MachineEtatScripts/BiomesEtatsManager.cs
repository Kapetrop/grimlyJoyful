// Inclut les bibliothèques nécessaires
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe définissant le gestionnaire d'états d'un biome
public class BiomesEtatsManager : MonoBehaviour
{
    // État actuel du biome
    private BiomesEtatsBase _etatActuel;



    // Déclaration des différents états du biome
    public BiomesEtatVivant etatVivant = new BiomesEtatVivant();
    public BiomesEtatArbreVivant etatArbreVivant = new BiomesEtatArbreVivant();
    public BiomesEtatMort etatMort = new BiomesEtatMort();
    public BiomesEtatGenerateur etatGenerateur = new BiomesEtatGenerateur();
    public BiomesEtatMortBloc etatMortBloc = new BiomesEtatMortBloc();
    public BiomesEtatCroixDeBase etatCroixDeBase = new BiomesEtatCroixDeBase();
    public BiomesEtatArbreMort etatArbreMort = new BiomesEtatArbreMort();
    public BiomesEtatRecoltable etatRecoltable = new BiomesEtatRecoltable();
    public BiomesEtatTrappe etatTrappe = new BiomesEtatTrappe();
    [SerializeField] public Transform _propSpot;



    // Liste de matériaux associée au biome
    private List<Material> _listeMateriels = new List<Material>();
    public List<Material> listeMateriels { set { _listeMateriels = value; } get { return _listeMateriels; } }

    // Générateur de dilemmes associé au biome
    public GenerateurDiles generateurDiles { get; set; }

    // Matériau du biome
    public Material biomeMateriel { get; set; }

    // Objets du biome (Arbres, Herbes, Spawner)
    public GameObject Arbres { get; set; }
    public GameObject Herbes { get; set; }
    public GameObject Spawners { get; set; }
    public GameObject Particules { get; set; }

    // Méthode appelée au démarrage
    void Start()
    {
        // Initialise l'état du biome au début
        ChangerEtat(etatVivant);
    }

    // Méthode pour changer l'état du biome
    public void ChangerEtat(BiomesEtatsBase etat)
    {
        // Met à jour l'état actuel et initialise le nouvel état
        _etatActuel = etat;
        etat.InitEtat(this);
    }

    // Méthode appelée à chaque frame
    void Update()
    {
        // Laissée commentée car l'update de l'état n'est pas utilisé ici
    }

    /// <summary>
    /// Méthode appelée lorsque le collider d'un autre objet entre dans la zone de déclenchement.
    /// Appelle la méthode TriggerEnterEtat de l'état actuel.
    /// </summary>
    /// <param name="other">Le collider de l'objet entrant en collision.</param>
    void OnTriggerEnter(Collider other)
    {
        // Appelle la méthode TriggerEnterEtat de l'état actuel
        _etatActuel.TriggerEnterEtat(this, other);
    }
}
