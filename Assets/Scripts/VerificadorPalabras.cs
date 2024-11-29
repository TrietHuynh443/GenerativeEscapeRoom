using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordChecker : MonoBehaviour
{

    private List<GameObject> blocks;
    public GameObject gOPrefab;
    public float xI;
    public float yI;
    public float zI;
    public float espacio = 0.3f;

    public int completed = 0;
    private string palabra;
    public int palabraActual = 0;
    public WordGenerator wordsGenerator;

    bool primeraVez = true;
    private SoundManager soundManager;

    void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        //print(completada+" = "+palabra.Length);
        if(completed == palabra.Length)
        {
            palabraActual++;
            destruirBloquesPalAnterior();
            StartGame();
            Invoke("Jugar", 2.0f);
        }
    }

    public void StartGame()
    {
        completed = 0;
        // palabra = wordsGenerator.ChangeWord(palabraActual);
        palabra = wordsGenerator.ChangeWord(1);
        blocks = new List<GameObject>();
        LlenarGameObjectsVacios();
    }


    public void Jugar()
    {
        float xtemp = xI;
        char[] arr = palabra.ToCharArray(0, palabra.Length);
        for (int i = 0; i < arr.Length; i++)
        {
            blocks[i] = Instantiate(gOPrefab, new Vector3((xtemp += espacio), yI, zI), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            //gOModelo.transform.localScale += new Vector3(3f, 3f, 3f);
            blocks[i].name = "" + arr[i];
            blocks[i].AddComponent<VerificarCaracter>();
        }
        if(primeraVez)
        {
            StartCoroutine(comenzarInstruccionesPalabras());
        }
        else{
            soundManager.PlaySound("Ins6L2");
            StartCoroutine(soundManager.CambiarInstruccionPantalla2("Ins3L2", 0, palabra, 0));
        } 
    }

    IEnumerator comenzarInstruccionesPalabras()
    {
        soundManager.PlaySound("Ins4L2");
        StartCoroutine(soundManager.CambiarInstruccionPantalla2("Ins3L2", 0, "animal", 0));
        yield return new WaitForSeconds(18);
        soundManager.PlaySound("Ins5L2");
        StartCoroutine(soundManager.CambiarInstruccionPantalla2("Ins5L2", 0, "animal", 0));
        yield return new WaitForSeconds(10);
        primeraVez = false;
        yield return new WaitForSeconds(10);
        soundManager.arrow.SetActive(false);
    }


    public void LlenarGameObjectsVacios()
    {
        for (int i = 0; i < palabra.Length; i++)
        {
            blocks.Add(gOPrefab);
        }
    }

    public void destruirBloquesPalAnterior()
    {
        for (int i = 0; i < palabra.Length; i++)
        {
            Destroy(blocks[i]);
        }
    }



    public void verificarPalabraCompletada()
    {       
            for (int i = 0; i < palabra.Length; i++)
            {
                if(blocks[i].gameObject.GetComponent<VerificarCaracter>()!=null)
                {
                    if(blocks[i].gameObject.GetComponent<VerificarCaracter>().completed)
                    {
                        completed++;
                    }
                }
            }       
    }
}