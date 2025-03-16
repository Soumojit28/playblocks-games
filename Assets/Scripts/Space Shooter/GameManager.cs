using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void WebGLGameOver(int shipsDestroyed, int obstaclesDestroyed);

    public static GameManager Instance;

    public static bool isGameOver = false;

    private static float initialTimeScale;

    void Awake()
    {
        Instance = this;

        initialTimeScale = Time.timeScale;
    }

    public static void GameOver()
    {
        int ships = ScoreManager.GetShipsDestroyed();
        int obstacles = ScoreManager.GetObstaclesDestroyed();

        Time.timeScale = 0f;
        isGameOver = true;

        AudioManager.PlayAudio(AudioManager.GameAudio.GameOver);

#if UNITY_WEBGL == true && UNITY_EDITOR == false
        WebGLGameOver (ships, obstacles);
#endif
    }

    public static void StartGame()
    {
        SceneManager.LoadScene(1);

        isGameOver = false;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0f;
        FindFirstObjectByType<SpaceShipController>().enabled = false;
    }

    public static void ResumeGame()
    {
        Time.timeScale = initialTimeScale;
        FindFirstObjectByType<SpaceShipController>().enabled = true;
    }

    public static void Restart()
    {
        // reset scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = initialTimeScale;
        isGameOver = false;
    }

    void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
