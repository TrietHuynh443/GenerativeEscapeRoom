using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
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
