using UnityEngine;

public class DestroyablePathObjectController : MonoBehaviour, IDestroyable
{
    [SerializeField] private GameObject destroyableAnchor;
    [SerializeField] private int stability = 10;

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
    /// Destroy the serialized Anchor and Changes CheckpointLocation
    /// </summary>
    public void DestroyObject()
    {
        Destroy(destroyableAnchor);
        //After that change CheckpointLocation
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
        int incomingDamage = (int)(otherRb != null ? otherRb.velocity : Vector2.zero).magnitude;
        AddDamage(incomingDamage);
    }
}