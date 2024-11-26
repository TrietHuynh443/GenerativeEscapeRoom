using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    public List<string> words;

    public string ChangeWord(int next)
    {
        return words[next];
    }
}
