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
    
    private GameObject currentPrize;

    private void Awake()
    {
        Instance = this;
    }

    public void OnStickPulled(string color)
    {
        Debug.Log($"Stick pulled! Color: {color}");
        
        DespawnCurrentPrize();
        
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
            case "Green":
                prizePrefab = bonusPrize;
                break;
            default:
                Debug.LogWarning("Unknown color stick pulled!");
                break;
        }

        if (prizePrefab != null && prizeSpawnPoint != null)
        {
            currentPrize = Instantiate(prizePrefab, prizeSpawnPoint.position, Quaternion.identity);
            Debug.Log($"{color} stick prize spawned!");
        }
    }
    
    private void DespawnCurrentPrize()
    {
        if (currentPrize != null)
        {
            Destroy(currentPrize);
            Debug.Log("Previous prize despawned!");
        }
    }
}
