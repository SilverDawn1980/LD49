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
        randomCatColor(newCatOnTheBlock);
        switch (catType)
        {
            case CatTypes.Bonechonk:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(3,5,2);
                break;
            case CatTypes.Chonkzero:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(4,5,3);
                break;
            case CatTypes.Leanandmean:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(5,7,3);
                break;
            case CatTypes.Fineboi:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(5,8,4);
                break;
            case CatTypes.Chonker:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(3,4,4);
                break;
            case CatTypes.Heftychonk:
                newCatOnTheBlock.GetComponent<CatController>().setCatStats(2,2,3);
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
