using UnityEngine;

public class PegRingDetector : MonoBehaviour
{
    public RingTossGameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            gameManager.RingScored(other.gameObject);
        }
    }
}

