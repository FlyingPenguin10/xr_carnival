using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeManager : MonoBehaviour
{
    public GameObject prizePrefab;
    public Transform prizeSpawnPoint;
    public float uprightThreshold = 0.6f;
    public float checkInterval = 0.5f;

    private GameObject[] cans;
    private bool prizeGiven = false;

    void Start()
    {
        cans = GameObject.FindGameObjectsWithTag("Can");
        StartCoroutine(CheckCansRoutine());
    }

    IEnumerator CheckCansRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            if (!prizeGiven && AllCansKnockedDown())
            {
                GivePrize();
            }
        }
    }

    bool AllCansKnockedDown()
    {
        foreach (GameObject can in cans)
        {
            if (can == null) continue;

            if (Vector3.Dot(can.transform.up, Vector3.up) > uprightThreshold)
            {
                return false;
            }
        }
        return true;
    }

    void GivePrize()
    {
        prizeGiven = true;

        if (prizePrefab && prizeSpawnPoint)
        {
            Instantiate(prizePrefab, prizeSpawnPoint.position, prizeSpawnPoint.rotation);
        }

        Debug.Log("You Win! Prize awarded.");

        StartCoroutine(EnableTryAgainButtonAfterDelay(2f));
    }

    IEnumerator EnableTryAgainButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        prizeGiven = false;
    }
}
