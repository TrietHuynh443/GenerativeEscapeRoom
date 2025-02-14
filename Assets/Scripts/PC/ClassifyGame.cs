using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassifyGame : Game
{
    [SerializeField] private List<ClassifyObject> _needToClassifyObjects;
    [SerializeField] private List<GameObject> _bucketObjects;

    private bool _isPLaying = true;

    public AudioClip correct;
    public bool completedSound = false;

    public GameObject Game1;

    private int checkWin = 0;

    public void Update()
    {
        if (!_isPLaying)
            return;

        GameControl();
    }

    public override void GameControl()
    {
        // Debug.Log("GameControl");
        _needToClassifyObjects.ForEach(obj =>
        {
            if (!obj.completed){
                checkWin++;
            }
        });

        if (checkWin == 0)
            StartCoroutine(WinGame());
        else
            checkWin = 0;
    }

    public override void StartGame()
    {
        _soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public override IEnumerator WinGame()
    {
        Debug.Log("WinGame");
        _isPLaying = false;
        if (!completedSound)
            {
                completedSound = true;
                AudioSource.PlayClipAtPoint(correct, Vector3.zero, 1.0f);
                Game1.SetActive(false);
                yield return new WaitForSeconds(3);
                completedSound = false;
            }
            // _canPlay = true;
            // Debug.LogWarning($"CheckRequirementPlay");
            // Invoke("ButtonPlay", 1.0f);
    }
}
