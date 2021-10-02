using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpNavigation : MonoBehaviour
{
    /// <summary>
    /// targets represents the possible targets to make a jump. Only works if a
    /// corresponding chance is set.
    /// </summary>
    [SerializeField] private GameObject[] targets;
    /// <summary>
    /// Chance is intended to work as follows :
    /// When chosing a target a die with a number of sides equal to the sum of all chances
    /// is rolled. The result is compared to the nearest chance value.<br/>
    /// If you want 3 Points with a 33% chance each, you would define chance as [33,66,99].<br/>
    /// A roll of 0-33 would be path 1<br/>
    /// A roll of 34-66 would be path 2<br/>
    /// A roll of 67-99 would be path 3
    /// </summary>
    [SerializeField] private int[] chance;

    public GameObject[] GetTargets()
    {
        return targets;
    }

    public int[] GetChance()
    {
        return chance;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
