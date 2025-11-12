using UnityEngine;

public class Balloon : MonoBehaviour
{
    public int points = 10;
    public GameObject popEffect;   // optional particle or sound
    public AudioClip popSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // When hit by a dart or collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dart"))
        {
            Pop();
        }
    }

    public void Pop()
    {
        // Play FX
        if (popEffect) Instantiate(popEffect, transform.position, Quaternion.identity);
        if (popSound)
        {
            audioSource.PlayOneShot(popSound);
        }

        // Add score
        BalloonGameManager.Instance.AddScore(points);

        // Destroy after FX plays
        Destroy(gameObject, 0.05f);
    }
}
