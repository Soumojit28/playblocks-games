using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void WebGLGameOver(string userName, int score);

    public static GameManager Instance;

    private float initialTimeScale;

    void Awake()
    {
        Instance = this;

        initialTimeScale = Time.timeScale;
    }

    public static void GameOver()
    {
        int score = ScoreManager.GetScore();

        Time.timeScale = 0f;

#if UNITY_WEBGL == true && UNITY_EDITOR == false
        WebGLGameOver ("PixelBit", score);
#endif
    }

    public void Restart()
    {
        // reset scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = initialTimeScale;
    }

    void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
