using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Game> games;
    public int currentGame = 0;

    public bool isEnd = false;

    [SerializeField] private Canvas _canvas;

    // Start is called before the first frame update
    void Start()
    {
        games[currentGame].StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(games[currentGame].IsDone)
        {
            if(currentGame < games.Count - 1)
            {
                currentGame++;
                games[currentGame].StartGame();
            }
            else
            {
                if (isEnd == true)
                {
                    _canvas.gameObject.SetActive(true);
                }
            }
        }
    }
}
