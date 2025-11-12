using UnityEngine;
using UnityEngine.Events;

public class TryAgainButton : MonoBehaviour
{
    public UnityEvent OnPressed;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") || other.CompareTag("Ball"))
        {
            OnPressed.Invoke();
        }
    }
}
