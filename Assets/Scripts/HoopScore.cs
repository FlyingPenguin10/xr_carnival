using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoopScore : MonoBehaviour
{
    private HashSet<GameObject> scoredBalls = new HashSet<GameObject>();
    private float resetDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))
        {
            if (!scoredBalls.Contains(other.gameObject))
            {
                scoredBalls.Add(other.gameObject);
                
                if (HoopGameManager.Instance != null)
                {
                    HoopGameManager.Instance.AddScore();
                }

                StartCoroutine(RemoveBallFromSet(other.gameObject));
            }
        }
    }

    private IEnumerator RemoveBallFromSet(GameObject ball)
    {
        yield return new WaitForSeconds(resetDelay);
        scoredBalls.Remove(ball);
    }
}

