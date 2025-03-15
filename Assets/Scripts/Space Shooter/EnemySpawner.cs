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

    [SerializeField]
    private int spawnDelay = 5;

    [SerializeField]
    private int maxEnemies = 2;

    private Transform spaceShipTransform;

    private int activeEnemies;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();

        InvokeRepeating("SpawnEnemy", 0, spawnDelay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        activeEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void SpawnEnemy()
    {
        if (activeEnemies >= maxEnemies)
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
