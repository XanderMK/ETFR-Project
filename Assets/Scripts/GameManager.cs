using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [Space(20f)]
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float targetSpawnDistance;
    [SerializeField] private Vector2 targetSpawnVariance;
    [SerializeField] private Vector3 targetSpawnOffset;
    [Tooltip("Frequency of target spawns, 1/x seconds")]
    [SerializeField] private float targetSpawnFrequency;
    [Space(10f)]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPosition;
    [Space(10f)]
    [SerializeField] private TMP_Text scoreText;


    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;

            if (scoreText != null )
            {
                scoreText.text = "Score: " + value;
            }
        }
    }

    private void Start()
    {
        score = 0;
    }

    float targetSpawnTimer = 0f;
    private void Update()
    {
        if (targetSpawnTimer >= (1f / targetSpawnFrequency))
        {
            SpawnTarget();

            targetSpawnTimer = 0f;
        }

        targetSpawnTimer += Time.deltaTime;
    }

    private void SpawnTarget()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-targetSpawnVariance.x, targetSpawnVariance.x), Random.Range(-targetSpawnVariance.y, targetSpawnVariance.y), (targetSpawnVariance.x + targetSpawnVariance.y * 2)).normalized * targetSpawnDistance;

        GameObject instantiatedTarget = Instantiate(targetPrefab, randomPosition + targetSpawnOffset, Quaternion.identity);
        instantiatedTarget.transform.LookAt(player);
    }
}
