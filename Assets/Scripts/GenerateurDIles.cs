using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GenerateurDiles : MonoBehaviour
{
        // Section : Variables de Génération de l'Île
    [Header("Variables de Génération de l'Île")]
    [SerializeField] private GameObject _cubeBase; // Le cube qui sera dupliqué
    [SerializeField] private GameObject _perso; // Le personnage
    private GameObject perso; // Le personnage instancié
    [SerializeField] private GameObject _conteneurEnnemis;
    private int _npcInstancier;
    [SerializeField] private GameObject _conteneurArbres;
    [SerializeField] private GameObject _conteneurHerbes;
  
    [SerializeField] private GameObject _conteneurSpawners;
    [SerializeField] private GameObject _conteneurParticules;
    [SerializeField] private int _npcMax; // Le nombre de npc à placer
    [SerializeField] private int _ileLargeur; // La largeur de l'île
    [SerializeField] private int _ileProfondeur; // La profondeur de l'île
    [SerializeField] private float _epaisseurMax; // L'épaisseur maximale des cubes

    [Header("Variables de Texture et de Terrain")]
    [SerializeField] private Renderer _textureRenderer; // Le renderer de la texture
    [SerializeField, Range(2f, 30f)] private float _detailTerrain; // L'atténuation du bruit
    [SerializeField, Range(0.1f, 1f)] private float _pourcentageHorsDeLEau; // Le pourcentage de la surface hors de l'eau
    [SerializeField, Range(0.1f, 60f)] private float _erosionIle; // Le pourcentage de la surface dans l'eau
    public enum Options { Carre, Circulaire } // Les options de forme de l'île
    [SerializeField] private Options _formeIle; // La forme de l'île
    private List<List<Material>> _biomesMats = new List<List<Material>>(); // Liste des matériaux des biomes
    // [SerializeField] private GameObject _ocean; // Plane de l'océan
    private List<GameObject> _listePositionsBiomes = new List<GameObject>(); // Liste des positions des cubes

    int _nbBiomesVivantMax = 0; // Nombre de biomes vivants
    private int _nbBiomesVivant = 0; // Nombre de biomes vivants
    private int _nbBiomesMort =0; // Nombre de biomes morts
    List<string> _listeBossInsancies = new List<string>();
    int _bossSpawner = 0;
    
    public int _nbSpawnerMax=20;
    public int _nbSpawnerUtiliser=0;
    private string _textNbSpawner;
    public string textNbSpawner {
        get {return _textNbSpawner;}
    }
    private float _pourcentage = 0;
    public float pourcentage {
        get {return _pourcentage;}
    }

    void Start()
    {
        _nbBiomesVivant = 0; // Nombre de biomes vivants
        _nbBiomesMort =0; // Nombre de biomes morts
        _npcInstancier = 0;
        ListeRessourcesPopulate(); // Rempli la liste avec des liste biomes + variantes
        float[,] unTerrain = TerraForme(_ileLargeur, _ileProfondeur); // Crée un tableau de float avec les grandeurs de l'île
        float[,] unTerrainBiomes = TerraForme(_ileLargeur, _ileProfondeur); // Crée un tableau de float avec les grandeurs de l'île
        // Si la forme est circulaire 
        if(_formeIle == Options.Circulaire) unTerrain = AquaFormeCirculaire(unTerrain); 
        // Si la forme est carrée
        else unTerrain = AquaFormeCarre(unTerrain);
        AfficherIle(unTerrain, unTerrainBiomes); // Affiche l'île
        perso = PlacerPerso(_ileLargeur/2,_epaisseurMax,_ileProfondeur/2); // Place le personnage
        InstancierSpawner();// Instancie les générateurs
        
        GetComponent<NavMeshSurface>().BuildNavMesh();
        
    }
  

    // Update is called once per frame
    void Update()
    {
        _textNbSpawner = ""+(_nbSpawnerMax-_nbSpawnerUtiliser)+" pierres originelles a détruire";
    }
    private void ListeRessourcesPopulate()
    {
        
        int nbBiomes = 1; // Numéro du biome
        int nbVariants = 1; // Numéro de la variante
        bool resteDesMats = true; // Si il reste des matériaux à charger
        List<Material> tpBiome = new List<Material>(); // Liste temporaire des matériaux
        do // Tant qu'il reste des matériaux à charger
        {
            Object mats = Resources.Load("Materials_biome/b" + nbBiomes + "_v" + nbVariants); // Charge les matériaux
            if(mats != null) // Si le matériaux existe
            {
                tpBiome.Add((Material)mats); // Ajoute le matériaux à la liste temporaire
                nbVariants++; //    Incrémente le numéro de la variante
            }
            else
            {
                if(nbVariants == 1) // Si il n'y a pas de variante
                {
                    resteDesMats = false; // Il n'y a plus de matériaux à charger
                }
                else
                {
                    _biomesMats.Add(tpBiome); // Ajoute la liste temporaire à la liste des biomes
                    tpBiome = new List<Material>(); // Réinitialise la liste temporaire
                    nbBiomes++; // Incrémente le numéro du biome
                    nbVariants = 1; // Réinitialise le numéro de la variante
                }
            }
        }
        while(resteDesMats); // Tant qu'il reste des matériaux à charger
    }
    /// <summary>
    /// Méthode qui trouve la position des cubes à générer
    /// qui crée une texture et qui l'applique au renderer de la map des cubes
    /// qui instancie les cubes
    /// </summary>
    /// <param name="terrain"></param>
    private void AfficherIle(float[,] terrain,float[,] terrainBiome) // Génère une texture aléatoire
    {
        int largeur = terrain.GetLength(0); // Récupère la largeur du tableau
        int profondeur = terrain.GetLength(1); // Récupère la profondeur du tableau
        Texture2D texture = new Texture2D(largeur, profondeur); // Crée une texture
        // Parcours la texture
        for (int z = 0; z < profondeur; z++)
            {
                for (int x = 0; x < largeur; x++)
                {
                    Color coulPixel = new Color(1,1,1) * terrain[x,z]; // Couleur aléatoire
                    texture.SetPixel(x,z,coulPixel); // Applique la couleur au pixel
                    InstacierBiome(x,terrain[x,z],z,terrainBiome[x,z]); // Instancie le cube
                    
                }
            }
            //mélanger ma liste
          
            //instancier un npc
        texture.Apply(); // Applique les changements
        _textureRenderer.sharedMaterial.mainTexture = texture; // Applique la texture au renderer
    }
    /// <summary>
    /// Méthode qui instancie
    /// et mets la couleur des cubes
    /// </summary>
    /// <param name="x">position en x sur la map</param>
    /// <param name="y">hauteur des cubes</param>
    /// <param name="z">position en z sur la map</param>
    private void InstacierBiome(float x, float y, float z, float yV)
    {
        if(y > -.1f)
        {
            int quelBiome = 0; // Récupère le biome
            int quelVariant = (int)(Mathf.Clamp01(yV) * (_biomesMats[quelBiome].Count - 1)); // Récupère le biome            
            GameObject unBiome = Instantiate(_cubeBase, new Vector3(x,y* _epaisseurMax,z), Quaternion.identity); // Instancie un cube
            unBiome.name = "Cube " + x + " " + z; // Donne un nom au cube
            unBiome.transform.parent = this.transform; // Met le cube dans le parent
            unBiome.GetComponent<BiomesEtatsManager>().Arbres = _conteneurArbres; // Passe la map des cubes
            unBiome.GetComponent<BiomesEtatsManager>().Herbes = _conteneurHerbes; // Passe la map des cubes
            unBiome.GetComponent<BiomesEtatsManager>().Spawners = _conteneurSpawners; // Passe la map des cubes
            unBiome.GetComponent<BiomesEtatsManager>().Particules = _conteneurParticules; // Passe la map des cubes
            unBiome.GetComponent<BiomesEtatsManager>().generateurDiles = this; // Passe la map des cubes
            unBiome.GetComponent<BiomesEtatsManager>().biomeMateriel = _biomesMats[quelBiome][quelVariant];
            _listePositionsBiomes.Add(unBiome);
            _nbBiomesVivantMax++;
        } 
    }
    private void InstancierSpawner()
    {
        // Créez une copie de la liste originale avant de la mélanger
        List<GameObject> copieListe = new List<GameObject>(_listePositionsBiomes);

        // Appelez Shuffle sur la copie
        List<GameObject> listeMelangee = Shuffle(copieListe);
        
        GameObject biome = listeMelangee[0];
        biome.name = "Generateur";
        biome.transform.parent = _conteneurSpawners.transform;
        biome.GetComponent<BiomesEtatsManager>().ChangerEtat(biome.GetComponent<BiomesEtatsManager>().etatCroixDeBase);
        
    }
    private float[,] TerraForme(int largeur, int profondeur)
    {
        float[,] terrain = new float[largeur, profondeur]; // Crée un tableau de float
        float attenuation = _detailTerrain; // Atténuation du bruit
        int bruit = Random.Range(0,10000);  // Bruit aléatoire

        for (int z = 0; z < profondeur; z++)
        {
            for (int x = 0; x < largeur; x++)
            {
                // terrain[x,z] = Random.Range(0,1f); // Rempli le tableau de float
                terrain[x,z] = Mathf.PerlinNoise((x/attenuation)+bruit,(z/attenuation)+bruit); // Rempli le tableau de float
                
            }
        }
        return terrain; // Retourne le tableau  de float
    }
    /// <summary>
    /// Méthode qui calcule les portion émmergeant de l'eau en forme de carré
    /// </summary>
    /// <param name="terrain"> coordonés des différend cubes à générer</param>
    /// <returns></returns>
    private float[,] AquaFormeCarre(float[,] terrain)
    {
        float largeur = terrain.GetLength(0); // Récupère la largeur du tableau
        float profondeur = terrain.GetLength(1); // Récupère la profondeur du tableau
        

        for (int z = 0; z < profondeur; z++)
        {
            for (int x = 0; x < largeur; x++)
            {
                float vx = Mathf.Abs(x/(float)largeur * 2 - 1); // Calcul la distance entre le centre et le point en x
                float vz = Mathf.Abs(z/(float)profondeur * 2 - 1); // Calcul la distance entre le centre et le point en z
                float val = Mathf.Max(vx,vz); // Calcul la distance entre le centre et le point avec le théorème de Pythagore
                val = Sigmoide(val); // Calcul la sigmoide

                terrain[x,z] = terrain[x,z] - val; // Rempli le tableau de float

            }
        }
        // AfficherOcean(); // Affiche l'océan 
        return terrain; // Retourne le tableau  de float
    }
     /// <summary>
    /// Méthode qui calcule les portion émmergeant de l'eau en forme circulaire
    /// </summary>
    /// <param name="terrain"> coordonés des différend cubes à générer</param>
    /// <returns></returns>
        private float[,] AquaFormeCirculaire(float[,] terrain)
    {
        float largeur = terrain.GetLength(0); // Récupère la largeur du tableau
        float profondeur = terrain.GetLength(1); // Récupère la profondeur du tableau
        
        float rayon = Mathf.Min(largeur,profondeur)/2f; // Calcul le rayon du cercle
        float centreX = largeur/2f; // Calcul le centre en x du cercle
        float centreZ = profondeur/2f; // Calcul le centre en z du cercle

        for (int z = 0; z < profondeur; z++)
        {
            for (int x = 0; x < largeur; x++)
            {
                float cx = x-centreX; // Calcul la distance entre le centre et le point en x
                float cz = z-centreZ; // Calcul la distance entre le centre et le point en z
                float val = Mathf.Sqrt(cx*cx + cz*cz); // Calcul la distance entre le centre et le point avec le théorème de Pythagore
                float distantpourcent = val/rayon; // Calcul le pourcentage de la distance par rapport au rayon
                distantpourcent= Sigmoide(distantpourcent);

                terrain[x,z] = terrain[x,z] - distantpourcent; // Rempli le tableau de float
            }
        }
        // AfficherOcean(); // Affiche l'océan 
        return terrain; // Retourne le tableau  de float
    }
    /// <summary>
    /// Méthode qui calcule la sigmoide
    /// </summary>
    /// <param name="valeur"></param>
    /// <returns>les positions possible des cubes à générer</returns> <summary>
  
    private float Sigmoide(float valeur) 
    {
        float k = _erosionIle;
        float c = _pourcentageHorsDeLEau;   

        return 1/(1+Mathf.Exp(-k*(valeur-c)));
    }
    // private void AfficherOcean()
    // {
    //     GameObject unOcean = Instantiate(_ocean, new Vector3(22,0,22), Quaternion.identity); // Instancie un cube
    //     unOcean.name = "Ocean"; // Donne un nom au cube
    //     unOcean.transform.localScale = new Vector3(5.5f,1,5.5f); // Donne une taille au cube
    // }
    private GameObject PlacerPerso(float posX, float posY, float posZ)
    {
        GameObject perso = Instantiate(_perso, new Vector3(posX,posY,posZ), Quaternion.identity); // Instancie un cube
        perso.name = "Perso"; // Donne un nom au cube
        perso.GetComponent<MovePerso>().generateurDiles = this;
        return perso;
    }
    private List<T> Shuffle<T>(List<T> list)
    {
        List<T> shuffledList = new List<T>();
        while (list.Count > 0)
        {
            int index = Random.Range(0, list.Count);
            shuffledList.Add(list[index]);
            list.RemoveAt(index);
        }
        return shuffledList;
    } 
    /// <summary>
    /// Méthode qui place les ennemis
    /// et qui les ajoute à la liste des ennemis
    /// et qui passe des paramètres aux scripts des ennemis
    ///  en autre les positions de son cube maison
    /// et de la cible à attaquer
    /// </summary>
    /// <param name="perso"></param>
    public void PlacerUnEnnemi(Transform unCube)
    {
        
        if(unCube != null)
        {
        
            //boucle 1 à _npcMax
            if(_npcInstancier < _npcMax)
            {
            
                // Instancie un ennemi
                GameObject unEnnemi = Instantiate((GameObject)Resources.Load("NPC/Ennemi/Zombie"),
                new Vector3(unCube.position.x+1f,
                unCube.position.y+1f,
                unCube.position.z),
                Quaternion.identity);
                // Donne un nom à l'ennemi
                unEnnemi.name = "Ennemi"; // Donne un nom au cube
                // Met le cube dans un contenant dans la hiérarchie
                unEnnemi.transform.parent = _conteneurEnnemis.transform; // Met le cube dans le parent
                // Passe des paramètres aux scripts des ennemis
                unEnnemi.GetComponent<EnnemiEtatManager>().generateur = this; // Passe la map des cubes
                unEnnemi.GetComponent<EnnemiEtatManager>().cible = perso;   // Passe la cible à attaquer
                unEnnemi.GetComponent<EnnemiEtatManager>().home = unCube.gameObject;   // Passe la position de son cube maison
                _npcInstancier++;
            }
        }
    }
    public void EnleverSpawner()
{
    _nbSpawnerUtiliser++;
    if (_nbSpawnerUtiliser == 20)
    {
        Debug.Log("C'est la fin");
        GameOver();
    }
    else
    {
        InstancierSpawner();
    }
}
    private void GameOver()
    {
        SceneManager.LoadScene("SceneFin");
    }
    public void AjouterBiomeVivant()
    {
        _nbBiomesVivant++;
        if(_nbBiomesMort != 0) _nbBiomesMort--;
        
    }
    public void EnleverBiomeVivant()
    {
        _nbBiomesMort++;
        _nbBiomesVivant--;
        float pourcentage = (float)_nbBiomesMort / (float)_nbBiomesVivantMax * 100;
        if (_nbBiomesVivant == 0)
        {
            GameOver();
        }
        float pourcentageModuler = pourcentage % 10;
        if (pourcentageModuler > 4.98f && pourcentageModuler < 5)
        {
             
            if (_bossSpawner < 1) InstancierBoss(1);
                
            else InstancierBoss(_bossSpawner);
        }
        
    }
        private void InstancierBoss(int nbBoss)
    {
       _bossSpawner++;// Créez une copie de la liste originale avant de la mélanger
        List<GameObject> copieListe = new List<GameObject>(_listePositionsBiomes);

        // Appelez Shuffle sur la copie
        List<GameObject> listeMelangee = Shuffle(copieListe);
        for (int i = 0; i <= nbBoss; i++)
        {
            if (listeMelangee.Count > 0)
            {
                GameObject biome = listeMelangee[0];
                biome.name = "Boss" + i;
                biome.transform.parent = _conteneurSpawners.transform;
                GameObject unBoss = Instantiate((GameObject)Resources.Load("NPC/Ennemi/Boss"), biome.transform.position, Quaternion.identity);
                listeMelangee.RemoveAt(0);

                // Passe des paramètres aux scripts des boss
                unBoss.GetComponent<BossEtatManager>().generateur = this; // Passe la map des cubes
                unBoss.GetComponent<BossEtatManager>().cible = perso;   // Passe la cible à attaquer
                unBoss.GetComponent<BossEtatManager>().home = biome;   // Passe la position de son cube maison
            }
        }
    }

}

