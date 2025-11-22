using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Hammer : MonoBehaviour
{
    private Rigidbody rb;
    private const float MIN_HIT_VELOCITY = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.linearVelocity.magnitude < MIN_HIT_VELOCITY) return;
        
        Mole mole = collision.gameObject.GetComponent<Mole>();
        if (mole != null)
        {
            mole.OnHit();
        }
    }
}
