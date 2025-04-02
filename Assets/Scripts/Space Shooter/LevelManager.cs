using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelConfigs
    {
        public LevelConfig levelConfig;
        public float thresholdPoints;
        public bool autoProgress;
    }

    public static ScoreManager Instance;

    [SerializeField]
    private LevelConfigs[] levelConfigs;

    private static int score = 0;

    private static int shipsDestroyed;
    private static int obstaclesDestroyed;

    private static int activeLevelConfig;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Reset();
    }

    internal void Reset()
    {
        score = 0;

        shipsDestroyed = 0;
        obstaclesDestroyed = 0;

        activeLevelConfig = 0;
    }

    public static int ProgressLevel()
    {
        if (activeLevelConfig < Instance.levelConfigs.Length - 1)
        {
            activeLevelConfig++;
            SpawnBoss();
        }
        return activeLevelConfig;
    }

    private static void SpawnBoss()
    {
        LevelConfig.BossSpawnerConfig bossSpawnerConfig = Instance.levelConfigs[activeLevelConfig]
            .levelConfig
            .bossSpawnerConfig;

        if (bossSpawnerConfig.spawnBoss)
        {
            Transform spaceShipTransform = FindFirstObjectByType<SpaceShipController>()
                .GetComponent<Transform>();

            Vector3 spawnPosition = new Vector3(
                0,
                spaceShipTransform.position.y + bossSpawnerConfig.spawnDistance,
                0
            );

            Instantiate(bossSpawnerConfig.bossPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public static LevelConfig GetLevelConfig()
    {
        return Instance.levelConfigs[activeLevelConfig].levelConfig;
    }

    public static void ShipDestroyed()
    {
        shipsDestroyed++;
    }

    public static void ObstacleDestroyed()
    {
        obstaclesDestroyed++;
    }

    public static int GetShipsDestroyed()
    {
        return shipsDestroyed;
    }

    public static int GetObstaclesDestroyed()
    {
        return obstaclesDestroyed;
    }

    public static void IncrementScore(int amount)
    {
        score += amount;

        if (
            Instance.levelConfigs[activeLevelConfig].autoProgress
            && (score >= Instance.levelConfigs[activeLevelConfig].thresholdPoints)
        )
        {
            ProgressLevel();
        }
    }

    public static int GetScore()
    {
        return score;
    }

    void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
