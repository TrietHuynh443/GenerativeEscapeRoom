using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour
{
    protected SoundManager _soundManager;
    public bool _isDone = false;
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public abstract void StartGame();

    public abstract IEnumerator WinGame();

    public virtual void LoseGame()
    {
        Debug.Log("Game Lose");
    }

    public abstract void GameControl();
}
