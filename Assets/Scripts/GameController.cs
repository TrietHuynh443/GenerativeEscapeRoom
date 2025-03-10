﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public BtnPressedInteraction[] btns;
    public List<int> RandomList = new List<int>();

    public bool listFull;
    public bool turnOnPC;
    public bool userShift;

    public int counter;
    public int contadorUsusario;
    public int nivel;

    [Range(0.1f, 2f)]
    public float Velocidad;

    public Text gameOver;

    public AudioClip incorrect;
    public AudioClip correct;
    public AudioClip nb;

    public GameObject tUsuario;
    public GameObject tPC;

    public GameObject buttons;
    public GameObject btnPlay;

    public Submission submision0;
    public Submission submision1;
    public Submission submision2;

    public GameObject misionRequisitoJugar;

    public GameObject premio;

    public bool completedSound = false;

    public bool playable = true;

    bool ins6 = false;
    bool ins7 = false;

    bool firstTime = true;

    private SoundManager soundManager;
    private bool _canPlay;

    void Awake()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        if (!_canPlay)
            StartCoroutine(CheckRequirementPlay());
        if (nivel == 5 && turnOnPC)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        soundManager.PlaySound("Ins5L1");
        userShift = false;
        turnOnPC = false;
        playable = false;
        btnPlay.SetActive(false);
        buttons.SetActive(false);
        premio.SetActive(true);
    }

    IEnumerator CheckSphereShift()
    {
        yield return new WaitForSeconds(1.5f);
        if (userShift)
        {
            tUsuario.SetActive(true);
            tPC.SetActive(false);
        }
        else if (turnOnPC)
        {
            tPC.SetActive(true);
            tUsuario.SetActive(false);
        }
        else
        {
            tPC.SetActive(false);
            tUsuario.SetActive(false);
        }
    }

    IEnumerator CheckRequirementPlay()
    {
        if ((submision0.completed) && (submision1.completed) && (submision2.completed))
        {
            if (!completedSound)
            {
                completedSound = true;
                AudioSource.PlayClipAtPoint(correct, Vector3.zero, 1.0f);
                yield return new WaitForSeconds(3);
                completedSound = false;
            }
            misionRequisitoJugar.SetActive(false);
            _canPlay = true;
            Debug.LogWarning($"CheckRequirementPlay");
            Invoke("ButtonPlay", 1.0f);
        }
    }

    void ButtonPlay()
    {
        Debug.LogWarning($"Button Play");

        playable = true;
        buttons.SetActive(false);
        btnPlay.SetActive(true);
        submision0.completed = false;
        if (!ins6)
        {
            soundManager.PlaySound("Ins6L1");
            StartCoroutine(soundManager.ChangeScreenInstruction("Ins6L1", "6Ins", "", 0, 4, 0));
            ins6 = true;
        }
        else if (!firstTime)
        {
            soundManager.PlaySound("Ins7L1");
            StartCoroutine(soundManager.ChangeScreenInstruction("Ins6L1", "7Ins", "", 0, 2, 0));
            soundManager.arrow.SetActive(false);
        }
    }

    void FillRandomList()
    {
        for (int i = 0; i <= 1000; i++)
        {
            RandomList.Add(Random.Range(0, 4));
        }
        listFull = true;
    }

    void TurnOnPC()
    {
        Debug.Log("TurnOnPC");  
        if (listFull && turnOnPC)
        {
            //print("NIVEL "+ nivel +" entra, i= "+ contador + " === BTN " +ListaAleatoria[contador]);

            btns[RandomList[counter]].Activar();
            if (counter >= nivel)
            {
                nivel++;
                ChangeShift();
            }
            else
            {
                counter++;
            }
            Invoke("TurnOnPC", Velocidad); //Velocidad
        }
    }

    public void ChangeShift()
    {
        Debug.Log("ChangeShift");
        AudioSource.PlayClipAtPoint(correct, Vector3.zero, 1.0f);
        if (turnOnPC)
        {
            Debug.Log("ChangeShift TurnOnPC");
            turnOnPC = false;
            userShift = true;
        }
        else
        {
            Debug.Log("ChangeShift UserShift");
            turnOnPC = true;
            userShift = false;
            counter = 0;
            contadorUsusario = 0;
            Invoke("TurnOnPC", 3.0f);
        }
        StartCoroutine(CheckSphereShift());
    }

    public void PlayUser(int idBtn)
    {
        // print(" entra, j= "+ contadorUsusario + " === BTN " +ListaAleatoria[contador]);
        if (idBtn != RandomList[contadorUsusario])
        {
            AudioSource.PlayClipAtPoint(incorrect, Vector3.zero, 2.0f);
            userShift = false;
            turnOnPC = false;
            playable = false;
            Invoke("ButtonPlay", 2.0f);
            return;
        }
        if (contadorUsusario == counter)
        {
            print("Nivel actual" + nivel);
            ChangeShift();
        }
        else
        {
            contadorUsusario++;
        }
    }

    public IEnumerator Play()
    {
        playable = true;

        if (firstTime)
        {
            soundManager.PlaySound("Ins8L1");
            yield return new WaitForSeconds(3);
            tPC.SetActive(true);
            tUsuario.SetActive(false);
            soundManager.activateAnimation("Ins8L1");

            yield return new WaitForSeconds(11);
            soundManager.PlaySound("Ins9L1");
            yield return new WaitForSeconds(3);
            tPC.SetActive(false);
            tUsuario.SetActive(true);
            soundManager.activateAnimation("Ins9L1");

            firstTime = false;

            yield return new WaitForSeconds(9);
            soundManager.arrow.SetActive(false);
            tPC.SetActive(false);
            tUsuario.SetActive(false);
        }

        userShift = false;
        turnOnPC = true;
        counter = 0;
        contadorUsusario = 0;
        nivel = 0;
        RandomList.Clear();
        FillRandomList();
        Invoke("ShowElementsPlay", 2.0f);
        Invoke("TurnOnPC", 4.0f);
    }

    void ShowElementsPlay()
    {
        buttons.SetActive(true);
        btnPlay.SetActive(false);
        turnOnPC = true;
        userShift = false;
    }
}
