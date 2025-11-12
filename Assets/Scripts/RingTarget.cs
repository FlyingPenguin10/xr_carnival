using UnityEngine;

public class RingTarget : MonoBehaviour
{
    public RingTossGameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            
        }
    }
}
