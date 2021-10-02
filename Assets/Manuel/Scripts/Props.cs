using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Props : MonoBehaviour, IDestroyable
{
    private Rigidbody2D _rb;
    private bool isDestroyed = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void AddDamage(int damageAmount)
    {
        isDestroyed = true;
        DestroyObject();
    }

    /// <summary>
    /// Drops the Prop to the Ground
    /// </summary>
    public void DestroyObject()
    {
        gameObject.layer = LayerMask.NameToLayer("DestroyedObject");
        _rb.AddForce(new Vector2(Random.Range(-1.0f,1.0f), Random.Range(1.0f,2.0f)).normalized * 250);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDestroyed)
        {
            AddDamage(0);
        }
    }
}