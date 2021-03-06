using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatManager : MonoBehaviour
{
    private static readonly String StartTag = "CatStart";
    
    [SerializeField] private GameObject startingPoint;
    [SerializeField] private List<GameObject> activeCats;

    [SerializeField] private int numCats;

    private CatFactory connectedFactory;
    
    private void Awake()
    {
        connectedFactory = GetComponent<CatFactory>();
        startingPoint = GameObject.FindGameObjectWithTag(StartTag);
        StartCoroutine("spawnCats");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator spawnCats()
    {
        while (numCats > 0)
        {
            yield return new WaitForSeconds(Random.Range(0, 5f));
            GameObject nextCat = connectedFactory.getRandomCat();
            activeCats.Add(nextCat);
            numCats--;
        }

        yield return null;
    }
}
