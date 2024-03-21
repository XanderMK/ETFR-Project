using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] private int maxTargets;
    [Space(10f)]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnPosition;
    [Space(10f)]
    [SerializeField] private TMP_Text scoreText;
    [Space(10f)]
    [SerializeField] private float playTime;

    private List<GameObject> targets = new();
    public int Score = 0;

    private void Start()
    {
        Score = 0;
    }

    float playTimeTimer = 0f;
    float targetSpawnTimer = 0f;
    private void Update()
    {
        playTimeTimer += Time.deltaTime;
        if (playTimeTimer > playTime) {
            StopGame();
            return;
        }

        for (int i = 0; i < targets.Count; i++) {
            if (targets[i] == null) {
                targets.RemoveAt(i);
                i--;
            }
        }

        if (targets.Count > maxTargets) return;

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

        targets.Add(instantiatedTarget);
    }

    private void StopGame() {
        
    }

    public void UpdateScoreText() {
        if (scoreText == null) return;
        scoreText.text = "Score: " + Score;
    }
}
