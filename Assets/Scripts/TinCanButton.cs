using UnityEngine;

public class VRButton : MonoBehaviour
{
    public TinCanGameManager gameManager;
    
    public void PressButton()
    {
        if (gameManager != null)
        {
            gameManager.ResetGame();
            Debug.Log("Tin Can game reset!");
        }
    }
}
