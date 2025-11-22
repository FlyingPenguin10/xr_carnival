using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class Stick : MonoBehaviour
{
    public string stickColor;
    public float pullDistanceThreshold = 0.5f;

    private XRGrabInteractable grabInteractable;
    private Vector3 startPosition;
    private bool hasBeenPulled = false;
    private bool isGrabbed = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    private void Update()
    {
        if (!isGrabbed || hasBeenPulled) return;

        float distancePulled = Vector3.Distance(transform.position, startPosition);
        
        if (distancePulled >= pullDistanceThreshold)
        {
            TriggerPrize();
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    private void TriggerPrize()
    {
        if (hasBeenPulled) return;
        hasBeenPulled = true;

        if (StickPullGame.Instance != null)
        {
            StickPullGame.Instance.OnStickPulled(stickColor);
        }

        Destroy(gameObject, 1.0f);
    }
}
