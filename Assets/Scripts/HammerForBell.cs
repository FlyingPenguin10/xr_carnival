using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HammerForBell : MonoBehaviour
{
    [Header("Settings")]
    public float swingForceMultiplier = 2f;
    
    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 2f;
            rb.angularDamping = 0.5f;
        }
    }
}
