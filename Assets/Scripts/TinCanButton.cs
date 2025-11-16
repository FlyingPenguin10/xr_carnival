using UnityEngine;

public class VRButton : MonoBehaviour
{
    public TinCanGameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // Any object touches button resets game
        gameManager.ResetGame();
    }
}
