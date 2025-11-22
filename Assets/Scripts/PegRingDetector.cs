using UnityEngine;
using System.Collections.Generic;

public class PegRingDetector : MonoBehaviour
{
    public RingTossGameManager gameManager;
    private HashSet<GameObject> scoredRings = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            if (!scoredRings.Contains(other.gameObject))
            {
                scoredRings.Add(other.gameObject);
                gameManager.RingScored(other.gameObject);
                Debug.Log($"Ring {other.gameObject.name} scored on {gameObject.name}!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            scoredRings.Remove(other.gameObject);
        }
    }

    public void ResetDetector()
    {
        scoredRings.Clear();
    }
}

