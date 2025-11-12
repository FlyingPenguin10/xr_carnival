using UnityEngine;

public class RingTossGameManager : MonoBehaviour
{
    [Header("References")]
    public Transform[] ringSpawnPoints; // Where rings start from
    public GameObject ringPrefab;       // The ring prefab to spawn
    public Transform playerStartPoint;  // Where the player stands or teleports
    public Transform pegArea;           // The peg target area

    [Header("Settings")]
    public int ringsPerRound = 3;
    public float resetDelay = 3f;

    private GameObject[] activeRings;
    private bool gameActive = false;

    void Start()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        ClearRings();

        activeRings = new GameObject[ringsPerRound];
        for (int i = 0; i < ringsPerRound; i++)
        {
            Transform spawnPoint = ringSpawnPoints[i % ringSpawnPoints.Length];
            activeRings[i] = Instantiate(ringPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        gameActive = true;
        Debug.Log("Ring Toss round started.");
    }

    public void ResetRingsAfterDelay()
    {
        Invoke(nameof(StartNewRound), resetDelay);
    }

    private void ClearRings()
    {
        if (activeRings == null) return;
        foreach (var ring in activeRings)
        {
            if (ring != null)
                Destroy(ring);
        }
    }

    public void EndRound()
    {
        gameActive = false;
        ResetRingsAfterDelay();
    }
}

public class RingTarget : MonoBehaviour
{
    public RingTossGameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            // Replace or remove the call to AddScore
            // For example, you could call EndRound if you want to end the round when a ring hits the target:
            // gameManager.EndRound();

            // Or simply remove the line if no action is needed:
            // (No code here)
        }
    }
}


