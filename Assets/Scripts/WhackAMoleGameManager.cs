using UnityEngine;
using System.Collections;
using TMPro;

public class WhackAMoleGameManager : MonoBehaviour
{
    [Header("References")]
    public Mole[] moles;
    public float molePopInterval = 1.5f;
    public float gameDuration = 30f;

    [Header("Scoring")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private bool gameRunning = false;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        gameRunning = true;
        float timer = 0f;
        score = 0;
        UpdateScoreUI();

        while (timer < gameDuration)
        {
            if (gameRunning)
            {
                PopRandomMole();
            }
            yield return new WaitForSeconds(molePopInterval);
            timer += molePopInterval;
        }

        gameRunning = false;
        ResetAllMoles();
        Debug.Log($"Whack-a-Mole game ended. Final Score: {score}");
    }

    private void PopRandomMole()
    {
        if (moles.Length == 0) return;

        int index = Random.Range(0, moles.Length);
        moles[index].PopUp();
    }

    public void OnMoleHit()
    {
        if (!gameRunning) return;
        score++;
        UpdateScoreUI();
        Debug.Log($"Mole hit! Score: {score}");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public void ResetAllMoles()
    {
        foreach (Mole mole in moles)
        {
            mole.ResetPosition();
        }
    }
}
