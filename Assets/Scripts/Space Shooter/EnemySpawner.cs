using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner Instance;

    [SerializeField]
    private GameObject[] EnemyPrefabs;

    [SerializeField]
    private float spawnDistance = 25.0f;

    [SerializeField]
    private float spawnRange = 2.5f;

    private Transform spaceShipTransform;

    private int activeEnemies;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();

        SpawnEnemy();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        activeEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void SpawnEnemy()
    {
        LevelConfig.EnemySpawnerConfig enemySpawnerConfig = ScoreManager
            .GetLevelConfig()
            .enemySpawnerConfig;

        Invoke(nameof(SpawnEnemy), enemySpawnerConfig.spawnDelay);

        if (activeEnemies >= enemySpawnerConfig.maxEnemies || !enemySpawnerConfig.spawnEnemies)
            return;

        float randomX = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPosition = new Vector3(
            randomX,
            spaceShipTransform.position.y + spawnDistance,
            0
        );

        int randomPrefab = Random.Range(0, EnemyPrefabs.Length);
        Instantiate(EnemyPrefabs[randomPrefab], spawnPosition, Quaternion.Euler(0, 0, 180));
    }

    void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
