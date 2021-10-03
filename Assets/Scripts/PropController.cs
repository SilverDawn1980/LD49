using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PropController : MonoBehaviour, IDestroyable
{
    private Rigidbody2D _rb;
    private bool _isDestroyed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Destroys the Object no matter the amount of Damage
    /// </summary>
    /// <param name="damageAmount"></param>
    public void AddDamage(int damageAmount)
    {
        if (_isDestroyed) return;
        _isDestroyed = true;
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


    /// <summary>
    /// Temporary for TestPurposes
    /// Calculates the amount of Damage the Object will get depending on the Speed of the Object and
    /// the other Object
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_isDestroyed)
        {
            AddDamage(0);
        }
    }
}