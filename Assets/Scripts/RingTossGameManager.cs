using UnityEngine;
using System.Collections.Generic;

public class RingTossGameManager : MonoBehaviour
{
    [Header("Rings")]
    public Transform[] ringStartPositions;
    private Rigidbody[] rings;

    [Header("Prize")]
    public GameObject prizePrefab;
    public Transform prizeSpawnPoint;

    private HashSet<GameObject> ringsScored = new HashSet<GameObject>();
    private bool prizeGiven = false;

    private void Start()
    {
        GameObject[] ringObjects = GameObject.FindGameObjectsWithTag("Ring");
        rings = new Rigidbody[ringObjects.Length];

        for (int i = 0; i < ringObjects.Length; i++)
        {
            rings[i] = ringObjects[i].GetComponent<Rigidbody>();
        }
    }

    public void ResetGame()
    {
        ResetRings();
        ringsScored.Clear();
        prizeGiven = false;
    }

    private void ResetRings()
    {
        for (int i = 0; i < rings.Length; i++)
        {
            rings[i].linearVelocity = Vector3.zero; // Updated from velocity to linearVelocity
            rings[i].angularVelocity = Vector3.zero;
            rings[i].transform.position = ringStartPositions[i].position;
            rings[i].transform.rotation = ringStartPositions[i].rotation;
        }
    }

    // Called by PegRingDetector
    public void RingScored(GameObject ring)
    {
        if (prizeGiven) return;

        ringsScored.Add(ring);

        // All 3 rings scored?
        if (ringsScored.Count >= rings.Length)
        {
            GivePrize();
        }
    }

    private void GivePrize()
    {
        Instantiate(prizePrefab, prizeSpawnPoint.position, prizeSpawnPoint.rotation);
        prizeGiven = true;
    }
}
