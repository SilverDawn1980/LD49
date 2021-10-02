using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryDestroyableObjects : MonoBehaviour, IDestroyable
{
    [SerializeField] private int stability = 10;

    public void AddDamage(int damageAmount)
    {
        stability -= damageAmount;
        if (stability <= 0)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D otherRb = other.rigidbody;
        int incomingDamage = (int)(((otherRb != null) ? otherRb.velocity : Vector2.zero)).magnitude;
        AddDamage(incomingDamage);
    }
}
