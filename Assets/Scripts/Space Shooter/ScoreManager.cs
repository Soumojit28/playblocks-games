using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField]
    private static int maxLives = 3;

    private static int score = 0;

    private static int Lives = maxLives;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Lives = maxLives;
        score = 0;
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
