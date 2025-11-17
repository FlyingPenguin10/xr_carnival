using UnityEngine;

public class JackBoxTrigger : MonoBehaviour
{
    public Animator jackAnimator;

    public void TriggerLaunch()
    {
        jackAnimator.SetTrigger("LaunchTrigger");
    }
}