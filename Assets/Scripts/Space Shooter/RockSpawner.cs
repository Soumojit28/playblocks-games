using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    private static RockSpawner Instance;

    [SerializeField]
    private GameObject[] RockPrefabs;

    [SerializeField]
    private float spawnDistance = 15.0f;

    [SerializeField]
    private float spawnRange = 10.0f;

    private Transform spaceShipTransform;

    private int activeRocks;

    private float totalSpawnChance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();

        SpawnRock();

        foreach (var prefab in RockPrefabs)
        {
            totalSpawnChance += prefab.GetComponent<Rock>().rockData.spawnChance;
        }
    }

    void FixedUpdate()
    {
        activeRocks = GameObject.FindGameObjectsWithTag("Rock").Length;
    }

    void SpawnRock()
    {
        LevelConfig.RockSpawnerConfig rockSpawnerConfig = ScoreManager
            .GetLevelConfig()
            .rockSpawnerConfig;

        Invoke(nameof(SpawnRock), rockSpawnerConfig.spawnDelay);

        if (activeRocks >= rockSpawnerConfig.maxRocks || !rockSpawnerConfig.spwanRocks)
            return;

        float randomX = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(
            randomX,
            spaceShipTransform.position.y + spawnDistance,
            0
        );

        float randomValue = Random.Range(0, totalSpawnChance);
        float cumulativeChance = 0f;
        int randomPrefab = 0;

        for (int i = 0; i < RockPrefabs.Length; i++)
        {
            cumulativeChance += RockPrefabs[i].GetComponent<Rock>().rockData.spawnChance;
            if (randomValue < cumulativeChance)
            {
                randomPrefab = i;
                break;
            }
        }

        Instantiate(RockPrefabs[randomPrefab], spawnPosition, Quaternion.identity);
    }

    void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
