using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BalloonGameManager : MonoBehaviour
{
    public static BalloonGameManager Instance;

    [Header("Spawn Settings")]
    public GameObject balloonPrefab;
    public int maxBalloons = 10;
    public Vector3 spawnArea = new Vector3(5f, 3f, 5f);
    public float spawnHeight = 1f;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public float gameTime = 60f; // 1 minute
    public TextMeshProUGUI timerText;

    private int score = 0;
    private float timer;
    private List<GameObject> balloons = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = gameTime;
        SpawnInitialBalloons();
        UpdateScoreText();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timerText)
        {
            int m = Mathf.FloorToInt(timer / 60);
            int s = Mathf.FloorToInt(timer % 60);
            timerText.text = $"Time: {m:00}:{s:00}";
        }

        if (timer <= 0)
        {
            EndGame();
        }

        // Maintain balloon count
        if (balloons.Count < maxBalloons)
            SpawnBalloon();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText)
            scoreText.text = "Score: " + score;
    }

    void SpawnInitialBalloons()
    {
        for (int i = 0; i < maxBalloons; i++)
            SpawnBalloon();
    }

    void SpawnBalloon()
    {
        Vector3 pos = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(spawnHeight, spawnArea.y + spawnHeight),
            Random.Range(-spawnArea.z, spawnArea.z)
        );
        GameObject b = Instantiate(balloonPrefab, pos, Quaternion.identity);
        balloons.Add(b);
    }

    void EndGame()
    {
        Debug.Log($"Game Over! Final Score: {score}");
        foreach (GameObject b in balloons)
        {
            if (b) Destroy(b);
        }
        balloons.Clear();
    }
}
