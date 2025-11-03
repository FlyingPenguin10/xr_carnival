using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // For XR interactions

public class HoopGameStartButton : MonoBehaviour
{
    public HoopGameManager gameManager;

    // If using XR Grab/Interactable button
    public void PressButton()
    {
        if (gameManager != null)
        {
            gameManager.StartRound();
            Debug.Log("Hoop game started!");
        }
    }
}

