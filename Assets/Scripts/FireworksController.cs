using UnityEngine;
using System.Collections;

public class FireworksController : MonoBehaviour
{
    [Header("Fireworks Settings")]
    public GameObject[] fireworkPrefabs;
    public Transform[] spawnPoints;
    public int fireworksPerShow = 5;
    public float delayBetweenFireworks = 0.5f;
    public float showDuration = 10f;
    
    [Header("Audio")]
    public AudioClip launchSound;
    public AudioClip explosionSound;
    public float explosionDelay = 1.5f;
    
    private AudioSource audioSource;
    private bool isShowRunning = false;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f;
            audioSource.minDistance = 10f;
            audioSource.maxDistance = 100f;
        }
    }
    
    public void StartFireworksShow()
    {
        if (isShowRunning)
        {
            Debug.Log("Fireworks show already running!");
            return;
        }
        
        StartCoroutine(FireworksShow());
    }
    
    private IEnumerator FireworksShow()
    {
        isShowRunning = true;
        Debug.Log("Starting fireworks show!");
        
        float showEndTime = Time.time + showDuration;
        
        while (Time.time < showEndTime)
        {
            for (int i = 0; i < fireworksPerShow; i++)
            {
                LaunchFirework();
                yield return new WaitForSeconds(delayBetweenFireworks);
            }
            
            yield return new WaitForSeconds(1f);
        }
        
        Debug.Log("Fireworks show complete!");
        isShowRunning = false;
    }
    
    private void LaunchFirework()
    {
        if (fireworkPrefabs == null || fireworkPrefabs.Length == 0)
        {
            Debug.LogWarning("No firework prefabs assigned!");
            return;
        }
        
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }
        
        GameObject fireworkPrefab = fireworkPrefabs[Random.Range(0, fireworkPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        GameObject firework = Instantiate(fireworkPrefab, spawnPoint.position, spawnPoint.rotation);
        
        if (launchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(launchSound, 0.5f);
        }
        
        if (explosionSound != null && audioSource != null)
        {
            StartCoroutine(PlayExplosionSound(spawnPoint.position));
        }
        
        Destroy(firework, 5f);
    }
    
    private IEnumerator PlayExplosionSound(Vector3 position)
    {
        yield return new WaitForSeconds(explosionDelay);
        
        AudioSource.PlayClipAtPoint(explosionSound, position, 0.8f);
    }
}
