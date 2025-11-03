using UnityEngine;
using TMPro; // Optional if using TextMeshPro for VR UI

public class HoopGameManager : MonoBehaviour
{
    public static HoopGameManager Instance;

    [Header("Score Settings")]
    public int score = 0;
    public int pointsPerBasket = 1;

    [Header("Timer Settings")]
    public float roundTime = 120f; // 2 minutes
    private float timeRemaining;
    private bool roundActive = false;

    [Header("Prize Prefabs")]
    public GameObject commonPrize;
    public GameObject rarePrize;
    public GameObject legendaryPrize;
    public GameObject bonusPrize;

    [Header("Prize Spawn Point")]
    public Transform prizeSpawnPoint;

    [Header("UI (Optional)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        if (!roundActive) return;

        // Countdown timer
        timeRemaining -= Time.deltaTime;

        if (timerText != null)
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();

        if (timeRemaining <= 0f)
            EndRound();
    }

    public void AddScore()
    {
        if (!roundActive) return;

        score += pointsPerBasket;
        Debug.Log("Current Score: " + score);

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void StartRound()
    {
        score = 0;
        timeRemaining = roundTime;
        roundActive = true;

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (timerText != null)
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }

    private void EndRound()
    {
        roundActive = false;
        AwardPrize();
    }

    private void AwardPrize()
    {
        GameObject prizeToGive = null;

        // Assign prize based on total score
        if (score <= 2)
            prizeToGive = commonPrize;
        else if (score <= 4)
            prizeToGive = rarePrize;
        else if (score <= 6)
            prizeToGive = legendaryPrize;
        else
            prizeToGive = bonusPrize;

        if (prizeToGive != null && prizeSpawnPoint != null)
            Instantiate(prizeToGive, prizeSpawnPoint.position, Quaternion.identity);

        Debug.Log("Round ended! Prize awarded: " + prizeToGive.name);
    }
}

