using UnityEngine;

public class AnimationDebugger : MonoBehaviour
{
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogError($"No Animator found on {gameObject.name}!");
            return;
        }
        
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError($"Animator on {gameObject.name} has no controller assigned!");
            return;
        }
        
        Debug.Log($"Animator Status for {gameObject.name}:");
        Debug.Log($"- Controller: {animator.runtimeAnimatorController.name}");
        Debug.Log($"- Enabled: {animator.enabled}");
        Debug.Log($"- Update Mode: {animator.updateMode}");
        Debug.Log($"- Culling Mode: {animator.cullingMode}");
    }
    
    private void Update()
    {
        if (animator == null) return;
        
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        
        if (clipInfo.Length > 0)
        {
            AnimationClip currentClip = clipInfo[0].clip;
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            
            Debug.Log($"Playing: {currentClip.name} | Time: {stateInfo.normalizedTime:F2} | Speed: {animator.speed}");
        }
        else
        {
            Debug.LogWarning($"No animation clip is currently playing on {gameObject.name}!");
        }
    }
}
