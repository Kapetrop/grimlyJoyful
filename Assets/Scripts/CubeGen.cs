using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class CubeGen : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private int _nbColone;
    [SerializeField] private int _nbRanger;
    [SerializeField] private int _nbEpaisseur;
    [SerializeField] private float _nbGap = 0.2f;
    private List<GameObject> _lCubes = new List<GameObject>();
    private bool _suppressionEnCours = false;
    private bool _rotationEnCours = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Je démarre");
        GenererCube(_nbColone, _nbRanger, _nbEpaisseur);
        _lCubes = MelangerListe(_lCubes);
        float offsetc = (float)_nbColone/2*(1f+_nbGap);
        float offsetr = (float)_nbRanger/2*(1f+_nbGap);
        float offsete = (float)_nbEpaisseur/2*(1f+_nbGap);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (_lCubes.Count == 0 && _rotationEnCours)
        {   
            StopCoroutine(TournerLesCubes());
            _rotationEnCours = false;
        }
        // ransform.Rotate(0.1f,0,0.23f).Space.World;
    }
    private void GenererCube(int colone, int ranger, int epaisseur)
    {

        for (int x = 0; x <= colone; x++)
        {

            for (int y = 0; y <= ranger; y++)
            {

                for (int z = 0; z <= epaisseur; z++)
                {
                    GameObject cube = Instantiate(_cube, new Vector3(x, y, z) * (1f + _nbGap), Quaternion.identity);
                    //changer la transparance du cube
                    cube.GetComponent<Renderer>().material.color = new Color(1,1,1, 0.5f);
                    cube.transform.parent = gameObject.transform;
                    _lCubes.Add(cube);
                }

            }

        }
        StartCoroutine(CoroutineEnleverCube());
        StartCoroutine(TournerLesCubes());

    }
    private IEnumerator CoroutineEnleverCube()
    {
        _suppressionEnCours = true;
        while(_suppressionEnCours)
        {
            yield return new WaitForSeconds(0.2f);
            
            Destroy(_lCubes[0]);
            _lCubes.RemoveAt(0);
            if(_lCubes.Count == 0)
                {
                    _suppressionEnCours = false;
                }	
    }
    }

    private List<GameObject> MelangerListe(List<GameObject> liste)
    {
        List<GameObject> melangee = new List<GameObject>(liste); // Crée une copie de la liste originale pour ne pas modifier l'original

        for (int i = 0; i < melangee.Count; i++)
        {
            GameObject temp = melangee[i];
            int randomIndex = Random.Range(0, melangee.Count);
            melangee[i] = melangee[randomIndex];
            melangee[randomIndex] = temp;
        }

        return melangee;
    }
    private IEnumerator TournerLesCubes()
    {
        // Attendre que tous les cubes soient générés
        yield return new WaitForSeconds(.01f);

        _rotationEnCours = true;

        // Rotation continue
        float rotationSpeed = 45f;
        Vector3 centerPoint = CalculerCentreDuCube();
        
        while (_lCubes.Count > 0)
        {
            foreach (GameObject cube in _lCubes)
            {
                cube.transform.RotateAround(centerPoint, new Vector3(-1,0,-1), rotationSpeed * Time.deltaTime);
            }
            
            yield return null;
        }

        _rotationEnCours = false;
    }
        private Vector3 CalculerCentreDuCube()
    {
        Vector3 centerPoint = Vector3.zero;
        foreach (GameObject cube in _lCubes)
        {
            centerPoint += cube.transform.position;
        }
        centerPoint /= _lCubes.Count;
        return centerPoint;
    }

    
}