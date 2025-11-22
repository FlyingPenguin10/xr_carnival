using UnityEngine;

public class VRResetButtonRingToss : MonoBehaviour
{
    public RingTossGameManager gameManager;
    
    public void PressButton()
    {
        if (gameManager != null)
        {
            gameManager.ResetGame();
            Debug.Log("Ring Toss game reset!");
        }
    }
}
