using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEtatManager : MonoBehaviour
{
    private BossEtatBase _etatActuel;

    public BossEtatSpawn etatSpawn = new BossEtatSpawn();
    public BossEtatMarche etatMarche = new BossEtatMarche();
    public BossEtatAttaque etatAttaque = new BossEtatAttaque();
    public BossEtatTouche etatTouche = new BossEtatTouche();
    public BossEtatMort etatMort = new BossEtatMort();

      // Propriétés de l'ennemi
    [SerializeField] public AudioClip sonMort;
    private AudioSource _audioSource;
    public GameObject cible { get; set; }
    public GameObject home { get; set; }
    public NavMeshAgent agent { get; set; }
    public Animator animator { get; set; }
    public Transform goal { get; set; }
    public float range = 10000f;
    public GenerateurDiles generateur { get; set; }
    public int vieBoss=2;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();

        ChangerEtat(etatSpawn);

        agent.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
    }
    public void ChangerEtat(BossEtatBase etat)
    {
        _etatActuel = etat;
        etat.InitEtat(this);
    }

    public void JoueurSon(AudioClip son)
    {
        _audioSource.PlayOneShot(son);
    }
    void OnTriggerEnter(Collider other)
    {
        _etatActuel.TriggerEnterEtat(this, other);
    }
    public void EnleverVie(int degat, BossEtatManager boss)
    {
        vieBoss -= degat;
        if(vieBoss > 0)
        {
            boss.ChangerEtat(boss.etatTouche);
        }
        if (vieBoss <= 0)
        {
            boss.ChangerEtat(boss.etatMort);
        }
    }
    IEnumerator CoroutineMarche(BossEtatManager boss)
    {
        boss.agent.destination = boss.cible.transform.position;
       

        while (boss.agent.remainingDistance < 2.5f && boss.agent.pathPending == true)
        {
            
            boss.agent.speed = 18f;
            boss.agent.destination = boss.cible.transform.position;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        boss.animator.SetBool("enChasse", false);
        boss.ChangerEtat(boss.etatAttaque);
    }
}
