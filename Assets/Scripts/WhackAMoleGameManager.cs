using UnityEngine;
using System.Collections;

public class WhackAMoleGameManager : MonoBehaviour
{
    [Header("References")]
    public Mole[] moles; // Assign in Inspector
    public float molePopInterval = 1.5f; // How often moles pop up
    public float gameDuration = 30f; // Total game length

    private bool gameRunning = false;
    void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        gameRunning = true;
        float timer = 0f;

        while (timer < gameDuration)
        {
            PopRandomMole();
            yield return new WaitForSeconds(molePopInterval);
            timer += molePopInterval;
        }

        gameRunning = false;
        ResetAllMoles();
        Debug.Log("Whack-a-Mole game ended.");
    }

    private void PopRandomMole()
    {
        if (moles.Length == 0) return;

        int index = Random.Range(0, moles.Length);
        moles[index].PopUp();
    }

    public void ResetAllMoles()
    {
        foreach (Mole mole in moles)
        {
            mole.ResetPosition();
        }
    }
}
