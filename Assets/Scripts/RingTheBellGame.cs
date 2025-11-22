using UnityEngine;

public class RingTheBellGame : MonoBehaviour
{
    [Header("Game Objects")]
    public Transform marker;
    public Transform bell;
    
    [Header("Settings")]
    public float towerHeight = 5f;
    public float minHitVelocity = 3f;
    public float velocityMultiplier = 1.5f;
    public float gravity = 9.8f;
    public float resetDelay = 3f;
    
    [Header("Audio")]
    public AudioSource bellSound;
    
    private Vector3 markerStartPosition;
    private bool isAnimating = false;
    private bool hasRungBell = false;
    
    private void Start()
    {
        if (marker != null)
        {
            markerStartPosition = marker.localPosition;
        }
        else
        {
            Debug.LogError("Marker not assigned in RingTheBellGame!");
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (isAnimating) return;
        
        Rigidbody rb = collision.rigidbody;
        if (rb == null) return;
        
        float hitVelocity = rb.linearVelocity.magnitude;
        
        if (hitVelocity >= minHitVelocity)
        {
            float strength = hitVelocity * velocityMultiplier;
            StartCoroutine(AnimateMarker(strength));
        }
    }
    
    private System.Collections.IEnumerator AnimateMarker(float strength)
    {
        isAnimating = true;
        hasRungBell = false;
        float currentHeight = 0f;
        
        float velocity = strength;
        float deltaTime = Time.fixedDeltaTime;
        
        while (velocity > 0 || currentHeight > 0.01f)
        {
            velocity -= gravity * deltaTime;
            currentHeight += velocity * deltaTime;
            
            currentHeight = Mathf.Max(0, currentHeight);
            currentHeight = Mathf.Min(currentHeight, towerHeight);
            
            if (marker != null)
            {
                Vector3 newPos = markerStartPosition;
                newPos.y += currentHeight;
                marker.localPosition = newPos;
            }
            
            if (currentHeight >= towerHeight * 0.95f && !hasRungBell)
            {
                RingBell();
                hasRungBell = true;
            }
            
            if (currentHeight <= 0.01f && velocity <= 0)
            {
                break;
            }
            
            yield return new WaitForFixedUpdate();
        }
        
        if (marker != null)
        {
            marker.localPosition = markerStartPosition;
        }
        
        yield return new WaitForSeconds(resetDelay);
        
        isAnimating = false;
    }
    
    private void RingBell()
    {
        if (bellSound != null)
        {
            bellSound.Play();
        }
        
        if (bell != null)
        {
            StartCoroutine(ShakeBell());
        }
        
        Debug.Log("Bell rung!");
    }
    
    private System.Collections.IEnumerator ShakeBell()
    {
        Vector3 originalPos = bell.localPosition;
        float shakeDuration = 0.5f;
        float shakeAmount = 0.1f;
        float elapsed = 0f;
        
        while (elapsed < shakeDuration)
        {
            float x = originalPos.x + Random.Range(-shakeAmount, shakeAmount);
            float y = originalPos.y + Random.Range(-shakeAmount, shakeAmount);
            float z = originalPos.z + Random.Range(-shakeAmount, shakeAmount);
            
            bell.localPosition = new Vector3(x, y, z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        bell.localPosition = originalPos;
    }
}
