using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class Stick : MonoBehaviour
{
    public string stickColor; // Assign in Inspector ("Red", "Blue", "Gold", "Green")
    private XRGrabInteractable grabInteractable;
    private bool hasBeenPulled = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectExited.AddListener(OnStickPulled);
    }

    private void OnDisable()
    {
        grabInteractable.selectExited.RemoveListener(OnStickPulled);
    }

    private void OnStickPulled(SelectExitEventArgs args)
    {
        if (hasBeenPulled) return;

        hasBeenPulled = true;

        // Notify the game manager
        if (StickPullGame.Instance != null)
            StickPullGame.Instance.OnStickPulled(stickColor);

        // Optional: destroy or disable the stick after pulling
        Destroy(gameObject, 1.0f);
    }
}
