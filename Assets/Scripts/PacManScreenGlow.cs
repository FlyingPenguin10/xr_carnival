using UnityEngine;

public class PacManScreenGlow : MonoBehaviour
{
    [Header("Screen Glow Settings")]
    public Material screenMaterial;
    public Color glowColor = Color.cyan;
    public float glowIntensity = 2f;
    public bool pulseGlow = true;
    public float pulseSpeed = 2f;
    
    private float baseBrightness;
    
    private void Start()
    {
        if (screenMaterial == null)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (renderer != null && renderer.materials.Length > 0)
            {
                screenMaterial = renderer.material;
            }
        }
        
        if (screenMaterial != null)
        {
            screenMaterial.EnableKeyword("_EMISSION");
            baseBrightness = glowIntensity;
        }
    }
    
    private void Update()
    {
        if (screenMaterial == null || !pulseGlow) return;
        
        float pulse = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        float brightness = baseBrightness * (0.7f + pulse * 0.3f);
        
        screenMaterial.SetColor("_EmissionColor", glowColor * brightness);
    }
}
