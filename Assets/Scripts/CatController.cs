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
    [SerializeField] private float naptimer;
    [SerializeField] private float maxNaptimer;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("is_walking");
    private static readonly int IsJumping = Animator.StringToHash("is_jumping");
    private static readonly int IsFalling = Animator.StringToHash("is_falling");
    
    /// Control Signals
    /// Each signal defined triggers a specific action the cat will be when colliding with a trigger, tagged with
    /// the corresponding Tag
    private static readonly String jumpSignal = "CatJump";
    private static readonly String killSignal = "CatGoal";

    [SerializeField]private bool is_waiting;

    public void setCatStats(float movespeed, float jumpforce, float napTimer)
    {
        this.moveSpeed = movespeed;
        this.jumpForce = jumpforce;
        this.maxNaptimer = napTimer;
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator.SetBool(IsWalking,true);
        naptimer = Random.Range(1, maxNaptimer);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_waiting)
        {
            _animator.SetBool(IsWalking,false);
            _animator.SetBool(IsJumping,false);
            _animator.SetBool(IsFalling,false);
        }
        else
        {
            _animator.SetBool(IsWalking,true);
            _animator.SetBool(IsJumping,false);
            _animator.SetBool(IsFalling,false);
        }
        if (_rigidbody2D.velocity.y > 0.2)
        {
            _animator.SetBool(IsWalking,false);
            _animator.SetBool(IsJumping,true);
            _animator.SetBool(IsFalling,false);
        }
        else if (_rigidbody2D.velocity.y < -0.2)
        {
            _animator.SetBool(IsWalking,false);
            _animator.SetBool(IsJumping,false);
            _animator.SetBool(IsFalling,true);
        }
        
        if (!is_waiting || _animator.GetBool(IsJumping) || _animator.GetBool(IsFalling))
        {
            transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime;
            naptimer -= Time.deltaTime;
        }

        if (naptimer <= 0)
        {
            is_waiting = true;
            StartCoroutine(waitforX(3));
        }
    }

    IEnumerator waitforX(float x)
    {
        yield return new WaitForSeconds(x);
        is_waiting = false;
        naptimer = maxNaptimer;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Needed to ignore other Cats
        if (other.gameObject.CompareTag("Cat"))
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        }
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
                float forceNeeded = Mathf.Sqrt(-2.0f * Physics2D.gravity.y * (diffy + 1));
                if (this.jumpForce >= forceNeeded)
                {
                    _rigidbody2D.AddForce(new Vector2(0f,forceNeeded),ForceMode2D.Impulse);
                }
                else
                {
                    _rigidbody2D.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
                }
            }
        }else if (other.CompareTag(killSignal))
        {
            Destroy(gameObject);
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
            return Vector3.zero;
        }
        GameObject[] navigationTargets = navigation.GetTargets();
        int[] chances = navigation.GetChance();
        if (chances.Length > 1)
        {
            int roll = Random.Range(0,chances[chances.Length - 1]);
            for (int i = 0; i < chances.Length; i++)
            {
                if (roll < chances[i])
                {
                    if (navigationTargets[i] == null)
                    {
                        return Vector3.zero;
                    }
                    return navigationTargets[i].transform.position;
                }
            }
        }
        else
        {
            if (navigationTargets[0] == null)
            {
                return Vector3.zero;
            }
            return navigationTargets[0].transform.position;
        }
        Debug.Log("Error in Navigation - Setting target Vector to Zero (This should never happen)");
        return Vector3.zero;
    }
}
