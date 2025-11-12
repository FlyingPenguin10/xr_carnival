using UnityEngine;

public class PrizeBehavior : MonoBehaviour
{
    public float lifeTime = 5f; // how long before despawn

    private void Start()
    {
        Destroy(gameObject, lifeTime); // automatically removes it
    }
}

