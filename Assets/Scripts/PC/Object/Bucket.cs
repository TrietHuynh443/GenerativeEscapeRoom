using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    // ECategoryType eCategoryType;
    // public ECategoryType ECategoryType;

    [SerializeField] private List<string> _bucketObjects;
    public Dictionary<ECategoryType, string> _bucketTagDictionary;

    // Start is called before the first frame update
    void Start()
    {
        _bucketTagDictionary = new Dictionary<ECategoryType, string>();
        _bucketTagDictionary.Add(ECategoryType.Class, _bucketObjects[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
