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

    [SerializeField]
    private Vector2 initialForce = new Vector2(1, 5);

    [SerializeField]
    private int spawnDelay = 3;

    [SerializeField]
    private int maxRocks = 3;

    private Transform spaceShipTransform;

    private int activeRocks;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();

        InvokeRepeating("SpawnRock", 0, spawnDelay);
    }

    void FixedUpdate()
    {
        activeRocks = GameObject.FindGameObjectsWithTag("Rock").Length;
    }

    void SpawnRock()
    {
        if (activeRocks >= maxRocks) return;

        float randomX = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(
            randomX,
            spaceShipTransform.position.y + spawnDistance,
            0
        );

        int randomPrefab = Random.Range(0, RockPrefabs.Length);
        GameObject rock = Instantiate(
            RockPrefabs[randomPrefab],
            spawnPosition,
            Quaternion.identity
        );

        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 forceDirection = (
                spaceShipTransform.position - rock.transform.position
            ).normalized;
            rb.AddForce(
                forceDirection * rb.mass * Random.Range(initialForce.x, initialForce.y),
                ForceMode2D.Impulse
            );
            rb.AddTorque(Random.Range(initialForce.x * 50, initialForce.y * 50));
        }
    }

    void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
