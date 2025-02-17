using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassifyGame : Game
{
    [SerializeField] private List<ClassifyObject> _needToClassifyObjects;
    [SerializeField] private List<Bucket> _bucketObjects;

    private bool _isPLaying = true;

    public AudioClip correct;
    public bool completedSound = false;

    public bool isLoadObject = false;

    // public GameObject Game1;

    private int checkWin = 0;

    public void Update()
    {
        if (!_isPLaying)
            return;

        StartCoroutine(GameControl());
    }

    public override IEnumerator GameControl()
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
        
        yield return new WaitForSeconds(0);
    }

    public override void StartGame()
    {
        _soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public override IEnumerator WinGame()
    {
        Debug.Log("WinGame");
        _isPLaying = false;
        _isDone = true;
        if (!completedSound)
            {
                completedSound = true;
                AudioSource.PlayClipAtPoint(correct, Vector3.zero, 1.0f);
                if (isEnd == false)
                {
                    gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(3);
                completedSound = false;
            }
    }

    public void SetObjectList(List<ClassifyObject> objects)
    {
        _needToClassifyObjects = objects;
    }
}
