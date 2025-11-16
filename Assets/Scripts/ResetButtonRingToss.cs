using UnityEngine;

public class VRResetButtonRingToss : MonoBehaviour
{
    public RingTossGameManager gameManager;
    private bool isPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPressed) return;
        isPressed = true;

        if (gameManager != null)
        {
            gameManager.ResetGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPressed = false;
    }
}
