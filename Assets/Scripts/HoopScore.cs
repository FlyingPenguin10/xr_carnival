using UnityEngine;

public class HoopScore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))
        {
            if (HoopGameManager.Instance != null)
                HoopGameManager.Instance.AddScore();
        }
    }
}

