using UnityEngine;
using System.Collections;

public class FireworksManager : MonoBehaviour
{
    [Header("Fireworks Settings")]
    public GameObject fireworkPrefab;
    public Transform spawnLocation;
    public int numberOfFireworks = 5;
    public float timeBetweenFireworks = 0.5f;
    public float fireworkLifetime = 5f;
    
    [Header("Spawn Area")]
    public float spawnRadius = 10f;
    public float minHeight = 20f;
    public float maxHeight = 30f;
    
    [Header("Audio")]
    public AudioClip[] explosionSounds;
    public AudioSource audioSource;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;
    
    private bool isLaunching = false;
    
    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.spatialBlend = 1f;
                audioSource.maxDistance = 100f;
            }
        }
    }
    
    public void LaunchFireworks()
    {
        if (isLaunching) return;
        
        StartCoroutine(FireworksSequence());
    }
    
    private IEnumerator FireworksSequence()
    {
        isLaunching = true;
        
        for (int i = 0; i < numberOfFireworks; i++)
        {
            SpawnFirework();
            yield return new WaitForSeconds(timeBetweenFireworks);
        }
        
        isLaunching = false;
    }
    
    private void SpawnFirework()
    {
        if (fireworkPrefab == null)
        {
            Debug.LogWarning("Firework prefab not assigned!");
            return;
        }
        
        Vector3 spawnPosition = GetRandomSpawnPosition();
        
        GameObject firework = Instantiate(fireworkPrefab, spawnPosition, Quaternion.identity);
        
        PlayExplosionSound();
        
        Destroy(firework, fireworkLifetime);
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 basePosition = spawnLocation != null ? spawnLocation.position : transform.position;
        
        float randomX = Random.Range(-spawnRadius, spawnRadius);
        float randomZ = Random.Range(-spawnRadius, spawnRadius);
        float randomY = Random.Range(minHeight, maxHeight);
        
        return basePosition + new Vector3(randomX, randomY, randomZ);
    }
    
    private void PlayExplosionSound()
    {
        if (audioSource != null && explosionSounds != null && explosionSounds.Length > 0)
        {
            AudioClip randomClip = explosionSounds[Random.Range(0, explosionSounds.Length)];
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(randomClip);
        }
    }
}
