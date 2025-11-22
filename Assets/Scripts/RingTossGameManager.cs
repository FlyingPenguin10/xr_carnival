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

    [Header("Pegs")]
    public PegRingDetector[] pegs;

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

        if (ringStartPositions.Length != rings.Length)
        {
            Debug.LogError($"Ring start positions ({ringStartPositions.Length}) don't match number of rings ({rings.Length})!");
        }
    }

    public void ResetGame()
    {
        ResetRings();
        ringsScored.Clear();
        prizeGiven = false;

        foreach (PegRingDetector peg in pegs)
        {
            if (peg != null)
            {
                peg.ResetDetector();
            }
        }

        Debug.Log("Ring Toss game reset!");
    }

    private void ResetRings()
    {
        for (int i = 0; i < rings.Length && i < ringStartPositions.Length; i++)
        {
            rings[i].linearVelocity = Vector3.zero;
            rings[i].angularVelocity = Vector3.zero;
            rings[i].transform.position = ringStartPositions[i].position;
            rings[i].transform.rotation = ringStartPositions[i].rotation;
        }
    }

    public void RingScored(GameObject ring)
    {
        if (prizeGiven) return;

        ringsScored.Add(ring);
        Debug.Log($"Rings scored: {ringsScored.Count}/{rings.Length}");

        if (ringsScored.Count >= rings.Length)
        {
            GivePrize();
        }
    }

    private void GivePrize()
    {
        if (prizePrefab != null && prizeSpawnPoint != null)
        {
            Instantiate(prizePrefab, prizeSpawnPoint.position, prizeSpawnPoint.rotation);
            prizeGiven = true;
            Debug.Log("All rings scored! Prize awarded!");
        }
    }
}
