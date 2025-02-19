using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour
{
    protected SoundManager _soundManager;
    public bool IsDone = false;

    public bool IsEnd = false;
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    void Awake()
    {
        _soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public abstract void StartGame();

    public abstract IEnumerator WinGame();

    public virtual void LoseGame()
    {
        Debug.Log("Game Lose");
    }

    public abstract IEnumerator GameControl();
}
