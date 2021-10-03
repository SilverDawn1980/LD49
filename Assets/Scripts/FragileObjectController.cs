using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class FragileObjectController : MonoBehaviour, IDestroyable
{
    [SerializeField] private int stability = 10;
    private bool _isDestroyed;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Deducts the incoming Damage from the Stability of the Object
    /// </summary>
    /// <param name="damageAmount"></param>
    public void AddDamage(int damageAmount)
    {
        stability -= damageAmount;
        if (stability > 0 || _isDestroyed) return;
        _isDestroyed = true;
        DestroyObject();
    }

    /// <summary>
    /// Shatters the Object into its smaller Parts 
    /// </summary>
    public void DestroyObject()
    {
        foreach (Transform tempChild in transform)
        {
            
            tempChild.gameObject.layer = LayerMask.NameToLayer("DestroyedObject");
            Rigidbody2D childRb = tempChild.gameObject.AddComponent<Rigidbody2D>();
            childRb.AddForce(new Vector2(Random.Range(-1.0f,1.0f), Random.Range(1.0f,2.0f)).normalized * 250);
            
        }
    }

    /// <summary>
    /// Temporary for TestPurposes
    /// Calculates the amount of Damage the Object will get depending on the Speed of the Object and
    /// the other Object
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D otherRb = other.rigidbody;
        int incomingDamage = (int)(((otherRb != null) ? otherRb.velocity : Vector2.zero) + _rb.velocity).magnitude;
        AddDamage(incomingDamage);
    }
}