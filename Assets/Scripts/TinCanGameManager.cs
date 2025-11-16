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

    private void Start()
    {
        // Collect all cans and balls by tag
        GameObject[] canObjects = GameObject.FindGameObjectsWithTag("Can");
        GameObject[] ballObjects = GameObject.FindGameObjectsWithTag("Ball");

        cans = new Rigidbody[canObjects.Length];
        balls = new Rigidbody[ballObjects.Length];

        for (int i = 0; i < canObjects.Length; i++)
            cans[i] = canObjects[i].GetComponent<Rigidbody>();

        for (int i = 0; i < ballObjects.Length; i++)
            balls[i] = ballObjects[i].GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckWinCondition();
    }

    // Called by the physical VR button
    public void ResetGame()
    {
        resetCans();
        resetBalls();
        prizeGiven = false;
    }

    private void resetCans()
    {
        for (int i = 0; i < cans.Length; i++)
        {
            cans[i].linearVelocity = Vector3.zero;
            cans[i].angularVelocity = Vector3.zero;
            cans[i].transform.position = canStartPositions[i].position;
            cans[i].transform.rotation = canStartPositions[i].rotation;
        }
    }

    private void resetBalls()
    {
        for (int i = 0; i < balls.Length; i++)
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

        bool allDown = true;

        foreach (Rigidbody can in cans)
        {
            // Can is considered knocked down if it's tilted more than 45 degrees
            if (Vector3.Dot(can.transform.up, Vector3.up) > 0.7f)
            {
                allDown = false;
                break;
            }
        }

        if (allDown)
            GivePrize();
    }

    private void GivePrize()
    {
        Instantiate(prizePrefab, prizeSpawnPoint.position, prizeSpawnPoint.rotation);
        prizeGiven = true;
    }
}
