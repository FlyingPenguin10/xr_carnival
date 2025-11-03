using UnityEngine;
using System.Collections.Generic;

public class BallSpawner : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public int maxBalls = 5; // how many can exist at once
    public float respawnHeight = -2f; // below this, the ball respawns
    public float respawnDelay = 2f;

    [Header("Spawn Location")]
    public Transform spawnPoint;

    private List<GameObject> activeBalls = new List<GameObject>();

    void Start()
    {
        // Spawn the initial set of balls
        for (int i = 0; i < maxBalls; i++)
        {
            SpawnBall();
        }
    }

    void Update()
    {
        // Check if any balls fell too low
        for (int i = activeBalls.Count - 1; i >= 0; i--)
        {
            GameObject ball = activeBalls[i];
            if (ball == null)
            {
                activeBalls.RemoveAt(i);
                continue;
            }

            if (ball.transform.position.y < respawnHeight)
            {
                Destroy(ball);
                activeBalls.RemoveAt(i);
                StartCoroutine(RespawnBallAfterDelay(respawnDelay));
            }
        }
    }

    private void SpawnBall()
    {
        if (ballPrefab == null || spawnPoint == null) return;

        GameObject newBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        activeBalls.Add(newBall);
    }

    private System.Collections.IEnumerator RespawnBallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnBall();
    }
}

