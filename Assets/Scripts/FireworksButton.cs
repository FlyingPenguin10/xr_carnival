using UnityEngine;
using System.Collections;

public class FireworksButton : MonoBehaviour
{
    public FireworksController fireworksController;
    
    [Header("Visual Feedback")]
    public float pressDepth = 0.05f;
    public float pressSpeed = 10f;
    
    private Vector3 startPosition;
    private bool isPressed = false;
    
    private void Start()
    {
        startPosition = transform.localPosition;
    }
    
    public void PressButton()
    {
        if (fireworksController != null)
        {
            fireworksController.StartFireworksShow();
            Debug.Log("Fireworks button pressed!");
            
            if (!isPressed)
            {
                StartCoroutine(PressAnimation());
            }
        }
        else
        {
            Debug.LogWarning("FireworksController not assigned to button!");
        }
    }
    
    private IEnumerator PressAnimation()
    {
        isPressed = true;
        
        Vector3 pressedPosition = startPosition - transform.forward * pressDepth;
        
        while (Vector3.Distance(transform.localPosition, pressedPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, pressedPosition, Time.deltaTime * pressSpeed);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.2f);
        
        while (Vector3.Distance(transform.localPosition, startPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * pressSpeed);
            yield return null;
        }
        
        transform.localPosition = startPosition;
        isPressed = false;
    }
}

