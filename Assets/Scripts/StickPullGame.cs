using UnityEngine;

public class StickPullGame : MonoBehaviour
{
    public static StickPullGame Instance;

    [Header("Prize Prefabs")]
    public GameObject commonPrize;
    public GameObject rarePrize;
    public GameObject legendaryPrize;
    public GameObject bonusPrize; // 🟩 Green stick prize

    [Header("Spawn Settings")]
    public Transform prizeSpawnPoint;

    private void Awake()
    {
        Instance = this;
    }

    public void OnStickPulled(string color)
    {
        Debug.Log($"Stick pulled! Color: {color}");
        GameObject prizePrefab = null;

        switch (color)
        {
            case "Red":
                prizePrefab = commonPrize;
                break;
            case "Blue":
                prizePrefab = rarePrize;
                break;
            case "Gold":
                prizePrefab = legendaryPrize;
                break;
            case "Green": // 🟩 New color
                prizePrefab = bonusPrize;
                break;
            default:
                Debug.LogWarning("Unknown color stick pulled!");
                break;
        }

        if (prizePrefab != null && prizeSpawnPoint != null)
        {
            Instantiate(prizePrefab, prizeSpawnPoint.position, Quaternion.identity);
        }
    }
}
