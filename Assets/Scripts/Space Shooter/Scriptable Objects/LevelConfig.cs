using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [System.Serializable]
    public class RockSpawnerConfig
    {
        public bool spwanRocks = true;
        public float spawnDelay = 4f;
        public int maxRocks = 2;
    }

    [System.Serializable]
    public class EnemySpawnerConfig
    {
        public bool spawnEnemies;
        public float spawnDelay = 4f;
        public int maxEnemies = 2;
        public int enemyHealth = 1;
        public float enemySpeed = 1f;
    }

    [System.Serializable]
    public class BossSpawnerConfig
    {
        public bool spawnBoss;
        public GameObject bossPrefab;
        public float spawnDistance = 25f;
    }

    public RockSpawnerConfig rockSpawnerConfig;

    public EnemySpawnerConfig enemySpawnerConfig;

    public BossSpawnerConfig bossSpawnerConfig;
}
