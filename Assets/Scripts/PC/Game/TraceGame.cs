using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraceGame : Game
{
    public BtnPressedInteraction[] btns;
    public List<int> RandomList = new List<int>();

    public bool listFull;
    public bool turnOnPC;
    public bool userShift;

    public int counter;
    public int userCounter;
    public int level;

    [Range(0.1f, 2f)]
    public float Velocidad;

    public Text gameOver;

    public AudioClip incorrect;
    public AudioClip correct;
    public AudioClip nb;

    public GameObject tUser;
    public GameObject tPC;

    public GameObject buttons;
    public GameObject btnPlay;

    public GameObject award;

    public bool completedSound = false;

    public bool playable = true;

    bool ins6 = false;
    bool ins7 = false;

    bool firstTime = true;

    void Update()
    {
        if (level == 5 && turnOnPC)
        {
            StartCoroutine(WinGame());
        }
    }

    public override IEnumerator GameControl()
    {
        playable = true;

        Debug.Log("GameControlHere");
        if (firstTime)
        {
            _soundManager.PlaySound("Ins8L1");
            yield return new WaitForSeconds(3);
            tPC.SetActive(true);
            tUser.SetActive(false);
            _soundManager.activateAnimation("Ins8L1");

            yield return new WaitForSeconds(9);
            _soundManager.PlaySound("Ins9L1");
            yield return new WaitForSeconds(3);
            tPC.SetActive(false);
            tUser.SetActive(true);
            _soundManager.activateAnimation("Ins9L1");

            firstTime = false;

            yield return new WaitForSeconds(8);
            _soundManager.arrow.SetActive(false);
            tPC.SetActive(false);
            tUser.SetActive(false);
        }

        userShift = false;
        turnOnPC = true;
        counter = 0;
        userCounter = 0;
        level = 0;
        RandomList.Clear();
        FillRandomList();
        Invoke("ShowElementsPlay", 2.0f);
        Invoke("TurnOnPC", 4.0f);

        yield return new WaitForSeconds(0);
    }

    public override void StartGame()
    {
        // _soundManager = GameObject.FindObjectOfType<SoundManager>();

        playable = true;
        buttons.SetActive(false);
        btnPlay.SetActive(true);
        // submision0.completed = false;
        if (!ins6)
        {
            _soundManager.PlaySound("Ins6L1");
            StartCoroutine(_soundManager.ChangeScreenInstruction("Ins6L1", "6Ins", "", 0, 4, 0));
            ins6 = true;
        }
        else if (!firstTime)
        {
            _soundManager.PlaySound("Ins7L1");
            StartCoroutine(_soundManager.ChangeScreenInstruction("Ins6L1", "7Ins", "", 0, 2, 0));
            _soundManager.arrow.SetActive(false);
        }
    }

    public override IEnumerator WinGame()
    {
        _soundManager.PlaySound("Ins5L1");
        userShift = false;
        turnOnPC = false;
        playable = false;
        btnPlay.SetActive(false);
        buttons.SetActive(false);
        award.SetActive(true);
        yield return new WaitForSeconds(3);
    }

       IEnumerator CheckSphereShift()
    {
        yield return new WaitForSeconds(1.5f);
        if (userShift)
        {
            tUser.SetActive(true);
            tPC.SetActive(false);
        }
        else if (turnOnPC)
        {
            tPC.SetActive(true);
            tUser.SetActive(false);
        }
        else
        {
            tPC.SetActive(false);
            tUser.SetActive(false);
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

    void ButtonPlay()
    {
        Debug.LogWarning($"Button Play");

        playable = true;
        buttons.SetActive(false);
        btnPlay.SetActive(true);
        if (!ins6)
        {
            _soundManager.PlaySound("Ins6L1");
            StartCoroutine(_soundManager.ChangeScreenInstruction("Ins6L1", "6Ins", "", 0, 4, 0));
            ins6 = true;
        }
        else if (!firstTime)
        {
            _soundManager.PlaySound("Ins7L1");
            StartCoroutine(_soundManager.ChangeScreenInstruction("Ins6L1", "7Ins", "", 0, 2, 0));
            _soundManager.arrow.SetActive(false);
        }
    }

    void TurnOnPC()
    {
        // Debug.Log("TurnOnPC");  
        if (listFull && turnOnPC)
        {
            //print("level "+ level +" entra, i= "+ contador + " === BTN " +ListaAleatoria[contador]);

            btns[RandomList[counter]].Activar();
            if (counter >= level)
            {
                level++;
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
        // Debug.Log("ChangeShift");
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
            userCounter = 0;
            Invoke("TurnOnPC", 3.0f);
        }
        StartCoroutine(CheckSphereShift());
    }

    void ShowElementsPlay()
    {
        buttons.SetActive(true);
        btnPlay.SetActive(false);
        turnOnPC = true;
        userShift = false;
    }

    public void PlayUser(int idBtn)
    {
        // print(" entra, j= "+ userCounter + " === BTN " +ListaAleatoria[contador]);
        if (idBtn != RandomList[userCounter])
        {
            AudioSource.PlayClipAtPoint(incorrect, Vector3.zero, 2.0f);
            userShift = false;
            turnOnPC = false;
            playable = false;
            Invoke("ButtonPlay", 2.0f);
            return;
        }
        if (userCounter == counter)
        {
            // print("level actual" + level);
            ChangeShift();
        }
        else
        {
            userCounter++;
        }
    }
}
