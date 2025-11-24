using UnityEngine;

public class TinCanGameManager : MonoBehaviour
{
    [Header("Cans & Balls")]
    public Transform[] canStartPositions;
    public Transform[] ballStartPositions;

    private Rigidbody[] cans;
    private Rigidbody[] balls;

    [Header("Prize")]
    public GameObject prizePrefab;
    public Transform prizeSpawnPoint;
    private bool prizeGiven = false;

    [Header("Win Condition Settings")]
    public float knockedAngleThreshold = 45f;
    public float minKnockedHeight = 0.1f;

    private void Start()
    {
        GameObject[] canObjects = GameObject.FindGameObjectsWithTag("Can");
        GameObject[] ballObjects = GameObject.FindGameObjectsWithTag("Ball");

        cans = new Rigidbody[canObjects.Length];
        balls = new Rigidbody[ballObjects.Length];

        for (int i = 0; i < canObjects.Length; i++)
            cans[i] = canObjects[i].GetComponent<Rigidbody>();

        for (int i = 0; i < ballObjects.Length; i++)
            balls[i] = ballObjects[i].GetComponent<Rigidbody>();

        if (canStartPositions.Length != cans.Length)
        {
            Debug.LogError($"Can start positions ({canStartPositions.Length}) don't match number of cans ({cans.Length})!");
        }

        if (ballStartPositions.Length != balls.Length)
        {
            Debug.LogError($"Ball start positions ({ballStartPositions.Length}) don't match number of balls ({balls.Length})!");
        }
    }

    private void Update()
    {
        CheckWinCondition();
    }

    public void ResetGame()
    {
        ResetCans();
        ResetBalls();
        prizeGiven = false;
    }

    private void ResetCans()
    {
        for (int i = 0; i < cans.Length && i < canStartPositions.Length; i++)
        {
            cans[i].linearVelocity = Vector3.zero;
            cans[i].angularVelocity = Vector3.zero;
            cans[i].transform.position = canStartPositions[i].position;
            cans[i].transform.rotation = canStartPositions[i].rotation;
        }
    }

    private void ResetBalls()
    {
        for (int i = 0; i < balls.Length && i < ballStartPositions.Length; i++)
        {
            balls[i].linearVelocity = Vector3.zero;
            balls[i].angularVelocity = Vector3.zero;
            balls[i].transform.position = ballStartPositions[i].position;
            balls[i].transform.rotation = ballStartPositions[i].rotation;
        }
    }

    private void CheckWinCondition()
    {
        if (prizeGiven) return;

        int knockedCount = 0;

        foreach (Rigidbody can in cans)
        {
            float angle = Vector3.Angle(can.transform.up, Vector3.up);
            bool isTipped = angle > knockedAngleThreshold;
            bool isFallen = can.transform.position.y < (canStartPositions[0].position.y - minKnockedHeight);

            if (isTipped || isFallen)
            {
                knockedCount++;
            }
        }

        if (knockedCount >= cans.Length)
        {
            Debug.Log($"All {cans.Length} cans knocked down! Giving prize...");
            GivePrize();
        }
    }

    private void GivePrize()
    {
        prizeGiven = true;
        
        if (prizePrefab != null && prizeSpawnPoint != null)
        {
            Instantiate(prizePrefab, prizeSpawnPoint.position, prizeSpawnPoint.rotation);
            Debug.Log("All cans knocked down! Prize awarded!");
        }
        else
        {
            Debug.LogWarning("Prize prefab or spawn point not assigned!");
        }
        
        if (confettiFX != null)
        {
            confettiFX.Play();
            Debug.Log("Confetti FX played!");
        }
        else
        {
            Debug.LogWarning("Confetti FX not assigned!");
        }
    }
    
    [Header("Visual Effects")]
    public ParticleSystem confettiFX;
}
