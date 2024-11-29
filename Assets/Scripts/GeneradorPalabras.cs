using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    private List<string> words = new List<string> { "Mystic", "Blossom", "Echo", "Harmony", "Nimbus" };

    public string ChangeWord(int next)
    {   
        if (next >= 0 && next < words.Count)
        {
            return words[next];
        }
        else
        {
            Debug.LogWarning("Index out of bounds! Returning default value.");
            return ""; // Or return a default word
        }
    }

    public string GetRandomWord()
    {
        if (words.Count > 0)
        {
            int randomIndex = Random.Range(0, words.Count);
            return words[randomIndex];
        }
        else
        {
            Debug.LogWarning("Word list is empty!");
            return "";
        }
    }
}
