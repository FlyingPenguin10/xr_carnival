using UnityEngine;
using TMPro;

public class HoopGameManager : MonoBehaviour
{
    public static HoopGameManager Instance;

    [Header("Score Settings")]
    public int score = 0;
    public int pointsPerBasket = 1;

    [Header("Timer Settings")]
    public float roundTime = 120f;
    private float timeRemaining;
    private bool roundActive = false;

    [Header("Prize Prefabs")]
    public GameObject commonPrize;
    public GameObject rarePrize;
    public GameObject legendaryPrize;
    public GameObject bonusPrize;

    [Header("Prize Spawn Point")]
    public Transform prizeSpawnPoint;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    
    [Header("Basketball Reset")]
    public Transform[] basketballStartPositions;
    private Rigidbody[] basketballs;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        FindBasketballs();
        StartRound();
    }
    
    private void FindBasketballs()
    {
        GameObject[] basketballObjects = GameObject.FindGameObjectsWithTag("Basketball");
        basketballs = new Rigidbody[basketballObjects.Length];
        
        for (int i = 0; i < basketballObjects.Length; i++)
        {
            basketballs[i] = basketballObjects[i].GetComponent<Rigidbody>();
        }
        
        if (basketballStartPositions.Length != basketballs.Length)
        {
            Debug.LogWarning($"Basketball start positions ({basketballStartPositions.Length}) don't match number of basketballs ({basketballs.Length})!");
        }
    }

    private void Update()
    {
        if (!roundActive) return;

        timeRemaining -= Time.deltaTime;

        if (timerText != null)
            timerText.text = $"Time: {Mathf.CeilToInt(timeRemaining)}";

        if (timeRemaining <= 0f)
            EndRound();
    }

    public void AddScore()
    {
        if (!roundActive) return;

        score += pointsPerBasket;
        Debug.Log($"Basket scored! Current Score: {score}");

        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    public void StartRound()
    {
        score = 0;
        timeRemaining = roundTime;
        roundActive = true;
        
        ResetBasketballs();

        if (scoreText != null)
            scoreText.text = $"Score: {score}";

        if (timerText != null)
            timerText.text = $"Time: {Mathf.CeilToInt(timeRemaining)}";

        Debug.Log("Basketball round started!");
    }
    
    private void ResetBasketballs()
    {
        if (basketballs == null || basketballStartPositions == null) return;
        
        for (int i = 0; i < basketballs.Length && i < basketballStartPositions.Length; i++)
        {
            if (basketballs[i] != null && basketballStartPositions[i] != null)
            {
                basketballs[i].linearVelocity = Vector3.zero;
                basketballs[i].angularVelocity = Vector3.zero;
                basketballs[i].transform.position = basketballStartPositions[i].position;
                basketballs[i].transform.rotation = basketballStartPositions[i].rotation;
            }
        }
        
        Debug.Log("Basketballs reset to starting positions!");
    }

    private void EndRound()
    {
        roundActive = false;
        AwardPrize();
    }

    private void AwardPrize()
    {
        GameObject prizeToGive = null;

        if (score <= 2)
            prizeToGive = commonPrize;
        else if (score <= 4)
            prizeToGive = rarePrize;
        else if (score <= 6)
            prizeToGive = legendaryPrize;
        else
            prizeToGive = bonusPrize;

        if (prizeToGive != null && prizeSpawnPoint != null)
        {
            Instantiate(prizeToGive, prizeSpawnPoint.position, Quaternion.identity);
            Debug.Log($"Round ended! Score: {score}, Prize: {prizeToGive.name}");
        }
        else
        {
            Debug.LogWarning("No prize awarded - check prize prefab assignments!");
        }
    }
}

