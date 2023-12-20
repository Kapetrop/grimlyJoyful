using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePerso : MonoBehaviour
{
    [SerializeField] private float vitesseMouvement = 15.0f;
    [SerializeField] private float vitesseRotation = 3.0f;
    [SerializeField] private float impulsionSaut = 30.0f;
    [SerializeField] private float gravite = 0.2f;
    [SerializeField] private GameObject champsDeForce;
    [SerializeField] private AudioClip _sonAttaque;
    [SerializeField] private AudioClip _sonSaut;
    [SerializeField] private AudioClip _sonChute;
    [SerializeField]  GameObject _arme;
    [SerializeField]  GameObject _armeAmelioree;
    [SerializeField] private float _tempsArmeAmelioree = 5.0f;
    [SerializeField] private SOPerso _donnees;
    private Coroutine attaque;
    private bool _enAttaque = false;
    private AudioSource _audioSource;
    private Vector3 _posOrigine;
    public GenerateurDiles generateurDiles { get; set; }
    [SerializeField] FootstepController _footstepController;
    [SerializeField] private GameObject _skinRenderer;
    private bool _enChute = false;
    public float lerpSpeed = 0.1f; // Ajustez la vitesse de l'interpolation
    [SerializeField]private bool _invulnerable = false;
    [SerializeField] private ChangeScene _changeScene;
    Vector3 _grosseurSPhere = new Vector3(3,3,3);
    bool _peutDasher = true;



    private float vitesseSaut;
    private Vector3 directionsMouvement = Vector3.zero;

    Animator animator;
    CharacterController controller;

    void Awake()
    {
        _donnees.Initialiser();
        _changeScene = GetComponent<ChangeScene>();
        _enChute = false;
        _posOrigine = transform.position;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();   
        _audioSource = GetComponent<AudioSource>();
        _arme.GetComponent<Collider>().enabled = false;
        _armeAmelioree.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Permet d'applique une rotation sur le Y avec la variable vitesseRotation en fonction des touche "a" ou "d" appuyer
        transform.Rotate(0, Input.GetAxisRaw("Horizontal")*vitesseRotation, 0);
        // Permet de modifier la vitesse du personnage en fonction de la variable vitesseMouvement en fonction de la touche "w" ou "s" appuyer
        float vitesse = Input.GetAxisRaw("Vertical")*vitesseMouvement;
        // Permet de lancer l'animation de course en fonction du paramètre enCourse, si la vitesse est plus grande que 0 sinon idle
        animator.SetBool("enCourse", vitesse>0); 
        animator.SetBool("enArriere", vitesse<0); 
        // Permet de mettre la valeur de vitesse dans la variable directionMouvement en ne modifiant que la valeur z du vector3
        directionsMouvement = new Vector3(0,0, vitesse); 
        // Permet de transforme la valeur local de directionMouvement en valeur globale avec transform.TransformDirection
        directionsMouvement = transform.TransformDirection(directionsMouvement); 
        // Permet de faire sauter le personnage à l'aide impulsionSaut en la manipulant avec vitesseSaut si la touche sau en appuyer et que le personnage est au sol
        if(  Input.GetButton("Jump") && _peutDasher) 
        {
            
            _peutDasher = false;
            directionsMouvement.z = vitesse * (impulsionSaut) ;
            Coroutine dash = StartCoroutine(CoroutineDash());
            // vitesseSaut = impulsionSaut; 
            _audioSource.PlayOneShot(_sonSaut);
        }
        // Permet de lancer l'animation du saut à l'aide du boolean "enSaut" en la manipulant si la valeur de impulsionSaut est plus grand que moins vitesseSaut 
        //et que le personnage n'est plus au en contact avec le sol
        animator.SetBool("enSaut", !controller.isGrounded&&vitesseSaut>-impulsionSaut); 
            directionsMouvement.y += vitesseSaut; 
        //Permet d'insérer la valeur vitesseSaut dans le Vector3 à la position "y" en fonction de vitesseSaut pour continuer le déplacement même dans les airs
        // Permet de décroitre la valeur de vitesseSaut en fonction de la valeur de gravite si le personnage n'est pas au sol 
        if(!controller.isGrounded) vitesseSaut -= gravite; 
        // Permet de faire bouger le personnage en modifiant le vecteur de déplacement avec la variable directionMovement en fonction du temps entre les frames
        controller.Move(directionsMouvement * Time.deltaTime); 
        // Permet de lancer l'animation d'attaque à l'aide du trigger "attaque" si la touche "Fire1" est appuyer
        if(Input.GetButton("Fire1")){
            if(attaque!=null)
            {
                StopCoroutine(attaque);
            }
            animator.Play("PJ_attack", 0, 0.1f); 
            _audioSource.PlayOneShot(_sonAttaque);
           _enAttaque = true;
           attaque = StartCoroutine(CoroutineAttaque());        
        }
        
        if(transform.position.y < -5 && transform.position.y > -30){            
            Coroutine chute = StartCoroutine(CoroutineChuter());
        }else _enChute= false;
        
       
        champsDeForce.transform.localScale = _grosseurSPhere *(1 + generateurDiles.pourcentage); // Permet de modifier la taille du champs de force en fonction de la vitesse du personnage
        
        if(transform.rotation.z > 0.1f || transform.rotation.z < -0.1f) // Permet de vérifier si la rotation du personnage est plus grande que 0.1 ou plus petite que -0.1
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,0), lerpSpeed); // Permet de faire une interpolation entre la rotation actuelle et la rotation voulu
        }
        if(vitesse > 0.1f || vitesse < -0.1f && controller.isGrounded) // Permet de vérifier si la vitesse du personnage est plus grande que 0.1 ou plus petite que -0.1
        {
            _footstepController.StartWalking(); // Permet de lancer la méthode StartWalking de la classe FootstepController
        }
        else if(vitesse < 0.1f || vitesse > -0.1f || !controller.isGrounded)
        {
            _footstepController.StopWalking(); // Permet de lancer la méthode StopWalking de la classe FootstepController
        }
    }
    
    public void AjouterVie()
    {
        _donnees.nbPointsDeVie++;
    }
    public void PerdreVie()
    {
        _donnees.nbPointsDeVie--;
       
        if(_donnees.nbPointsDeVie <= 0)
        {
            _changeScene.VersSceneFin();
            
        }
    }
    public void AmeliorerArme()
    {
        Coroutine ChangeArme = StartCoroutine(CoroutineChangeArme());
    }
    IEnumerator CoroutineChangeArme()
        {
            _arme.SetActive(false);
            _armeAmelioree.SetActive(true);
            yield return new WaitForSeconds(_tempsArmeAmelioree);
            _arme.SetActive(true);
            _armeAmelioree.SetActive(false);

        }
    IEnumerator CoroutineChuter()
    {
        _audioSource.PlayOneShot(_sonChute);
        _enChute = true;
        Coroutine corout = StartCoroutine(CoroutineBlink(_enChute));
        yield return new WaitForSeconds(2f);
        StopCoroutine(corout);
        _enChute= false;
        transform.position = _posOrigine;
         _skinRenderer.SetActive(true);
    }
    IEnumerator CoroutineBlink(bool variable)
    {
        while (variable)
        {
            // Toggle the visibility of the character by adjusting material color alpha
           _skinRenderer.SetActive(!_skinRenderer.activeSelf);

            // Wait for the specified blink rate
            yield return new WaitForSeconds(0.1f);
        }
       
    }

        IEnumerator CoroutineAttaque()
    {
        while(_enAttaque)
        {
            _arme.GetComponent<Collider>().enabled = true;
            _armeAmelioree.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(1f);
            _enAttaque = false;
        }
        _arme.GetComponent<Collider>().enabled = false;
        _armeAmelioree.GetComponent<Collider>().enabled = false;
    }
    IEnumerator CoroutineInvulnerable(float tempsInvulnerable)
    {
        while(_invulnerable)
        {
            bool vulnerable = true;
            Debug.Log(vulnerable);
            Coroutine corout = StartCoroutine(CoroutineBlink(vulnerable));
            yield return new WaitForSeconds(tempsInvulnerable);
            vulnerable = false;
            StopCoroutine(corout);
            _skinRenderer.SetActive(true);
            _invulnerable = false;
        }
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        
    
        if(other.gameObject.CompareTag("mainsZombie"))
        {

            if(!_invulnerable)
            {
                _invulnerable = true;
                PerdreVie();
                Coroutine invulnerable = StartCoroutine(CoroutineInvulnerable(1.5f));
            }
        }
    }
    public void GrossirSphere(float ajout)
    {
        Debug.Log("GrossirSphere");
        _grosseurSPhere += new Vector3(ajout*.1f,ajout*.1f,ajout*.1f);
    }
    IEnumerator CoroutineDash()
    {
        yield return new WaitForSeconds(4f);
        _peutDasher = true;
    }
    public void AgrandirArme(float ajout)
    {
        _arme.transform.localScale += new Vector3(ajout*0.1f,ajout*0.1f,ajout*0.1f);
        _armeAmelioree.transform.localScale += new Vector3(ajout*0.1f,ajout*0.1f,ajout*0.1f);
    }
}