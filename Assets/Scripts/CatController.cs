using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// Main Class for controlling a cats movement and behavior
public class CatController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("is_walking");
    
    /// Control Signals
    /// Each signal defined triggers a specific action the cat will be when colliding with a trigger, tagged with
    /// the corresponding Tag
    private static readonly String jumpSignal = "CatJump";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator.SetBool(IsWalking,true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision : " + other.tag.ToString());
        if (other.CompareTag(jumpSignal))
        {
            Vector3 target = calculateTarget(other.GetComponent<JumpNavigation>());
            if (target != Vector3.zero)
            {
                float diffy = target.y - transform.position.y;
                _rigidbody2D.AddForce(new Vector2(0f,Mathf.Sqrt(-2.0f * Physics2D.gravity.y * (diffy + 1))),ForceMode2D.Impulse);
            }
        }
    }

    /// <summary>
    /// Calculates a jump target based on the target list and the weightings of a passed JumpNavigation
    /// element.
    /// </summary>
    /// <param name="navigation">Navigation Element attached to a CatNav : Jump Point</param>
    /// <returns>Target as Vector 3 <br/> returns Vector3.Zero if no valid target is found.</returns>
    private Vector3 calculateTarget(JumpNavigation navigation)
    {
        if (navigation == null)
        {
            Debug.Log("No Targets");
            return Vector3.zero;
        }
        GameObject[] navigationTargets = navigation.GetTargets();
        int[] chances = navigation.GetChance();
        Debug.Log(chances.Length);
        if (chances.Length > 1)
        {
            int roll = Random.Range(0,chances[chances.Length - 1]);
            for (int i = 0; i < chances.Length; i++)
            {
                if (roll < chances[i])
                {
                    if (navigationTargets[i] == null)
                    {
                        Debug.Log("Dont wanna jump !");
                        return Vector3.zero;
                    }
                    Debug.Log(navigationTargets[i].transform.position);
                    return navigationTargets[i].transform.position;
                }
            }
        }
        else
        {
            if (navigationTargets[0] == null)
            {
                Debug.Log("Dont wanna jump !");
                return Vector3.zero;
            }
            Debug.Log(navigationTargets[0].transform.position);
            return navigationTargets[0].transform.position;
        }
        Debug.Log("Nope");
        return Vector3.zero;
    }
}
