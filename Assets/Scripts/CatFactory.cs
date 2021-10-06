using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatFactory : MonoBehaviour
{
    [SerializeField] private GameObject catPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getRandomCat()
    {
        return getCat((CatTypes)Random.Range(0, 7));
    }
    
    private void randomCatColor(GameObject cat)
    {
        cat.GetComponent<SpriteRenderer>().color =
            new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
    
    public GameObject getCat(CatTypes catType)
    {
        GameObject newCatOnTheBlock = GameObject.Instantiate(catPrefab,transform.position,Quaternion.identity);
        newCatOnTheBlock.GetComponent<CatController>().catType = catType;
        randomCatColor(newCatOnTheBlock);
        switch (catType)
        {
            case CatTypes.Bonechonk:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(3,7,2);
                break;
            case CatTypes.Chonkzero:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(4,9,3);
                break;
            case CatTypes.Leanandmean:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(5,11,3);
                break;
            case CatTypes.Fineboi:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(5,10,4);
                break;
            case CatTypes.Chonker:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(3,6,4);
                break;
            case CatTypes.Heftychonk:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(2,3,3);
                break;
            case CatTypes.Ohlawdhecomin:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(1,1,2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(catType), catType, null);
        }

        return newCatOnTheBlock;
    }
}
