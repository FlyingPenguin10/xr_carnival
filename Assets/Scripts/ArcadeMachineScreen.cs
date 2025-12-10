using UnityEngine;

public class ArcadeMachineScreen : MonoBehaviour
{
    [Header("Screen Settings")]
    public Material screenMaterial;
    public Color emissiveColor = Color.cyan;
    public float emissiveIntensity = 2f;
    
    [Header("Optional Animator")]
    public Animator arcadeAnimator;
    public string playAnimationTrigger = "Play";
    
    private void Start()
    {
        SetupEmissiveScreen();
    }
    
    private void SetupEmissiveScreen()
    {
        if (screenMaterial != null)
        {
            screenMaterial.EnableKeyword("_EMISSION");
            screenMaterial.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
        }
    }
    
    public void OnScreenPressed()
    {
        if (arcadeAnimator != null && !string.IsNullOrEmpty(playAnimationTrigger))
        {
            arcadeAnimator.SetTrigger(playAnimationTrigger);
            Debug.Log("Arcade machine activated!");
        }
        
        FlashScreen();
    }
    
    private void FlashScreen()
    {
        if (screenMaterial != null)
        {
            StartCoroutine(FlashCoroutine());
        }
    }
    
    private System.Collections.IEnumerator FlashCoroutine()
    {
        Color originalColor = screenMaterial.GetColor("_EmissionColor");
        
        screenMaterial.SetColor("_EmissionColor", Color.white * emissiveIntensity * 2f);
        yield return new WaitForSeconds(0.1f);
        
        screenMaterial.SetColor("_EmissionColor", originalColor);
    }
}
