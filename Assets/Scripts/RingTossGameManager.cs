using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class RingTossManager : MonoBehaviour
{
    public Transform ringStartPoint;
    public GameObject ringPrefab;
    public int ringCount = 3;
    public Canvas uiCanvas;

    private GameObject[] rings;

    void Start()
    {
        SpawnRings();
        uiCanvas.gameObject.SetActive(false);
    }

    void SpawnRings()
    {
        rings = new GameObject[ringCount];
        for (int i = 0; i < ringCount; i++)
        {
            Vector3 offset = new(i * 0.25f, 0, 0); // Simplified 'new' expression
            rings[i] = Instantiate(ringPrefab, ringStartPoint.position + offset, ringStartPoint.rotation);
        }
    }

    void Update()
    {
        if (AllRingsThrown())
        {
            StartCoroutine(ShowRestartUI());
        }
    }

    bool AllRingsThrown()
    {
        foreach (var ring in rings)
        {
            if (ring != null && ring.transform.position.y > 0.2f)
                return false;
        }
        return true;
    }

    IEnumerator ShowRestartUI()
    {
        yield return new WaitForSeconds(3f);
        uiCanvas.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        foreach (var ring in rings)
            if (ring != null) Destroy(ring);
        uiCanvas.gameObject.SetActive(false);
        SpawnRings();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
