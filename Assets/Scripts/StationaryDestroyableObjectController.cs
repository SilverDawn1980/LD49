using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StationaryDestroyableObjectController : MonoBehaviour, IDestroyable
{
    [SerializeField] private int stability = 10;
    [SerializeField] private Sprite destroyedVariant;
    
    private bool _isDestroyed;

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
    /// Changes the Sprite/Animation to a destroyed Variant
    /// </summary>
    public void DestroyObject()
    {
        //Temporary, later change Animation to destroyed Variant
        GetComponent<SpriteRenderer>().sprite = destroyedVariant;
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
        int incomingDamage = (int)(((otherRb != null) ? otherRb.velocity : Vector2.zero)).magnitude;
        AddDamage(incomingDamage);
    }
}