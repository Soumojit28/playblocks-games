using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField]
    private static int maxLives = 3;

    private static int score = 0;

    private static int Lives = maxLives;

    private static int shipsDestroyed;
    private static int obstaclesDestroyed;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Lives = maxLives;
        score = 0;

        shipsDestroyed = 0;
        obstaclesDestroyed = 0;
    }

    public static void ShipDestroyed () {
        shipsDestroyed++;
    }

    public static void ObstacleDestroyed () {
        obstaclesDestroyed++;
    }

    public static int GetShipsDestroyed() {
        return shipsDestroyed;
    }

    public static int GetObstaclesDestroyed() {
        return obstaclesDestroyed;
    }

    public static void IncrementScore(int amount)
    {
        score += amount;
    }

    public static int GetScore()
    {
        return score;
    }

    public static void DecreaseLife()
    {
        Lives -= 1;

        if (Lives <= 0)
            GameManager.GameOver();
    }

    void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
