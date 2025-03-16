using UnityEngine;

public class WebGLEvents : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.StartGame();
    }

    public void PauseGame()
    {
        GameManager.PauseGame();
    }

    public void ResumeGame()
    {
        GameManager.ResumeGame();
    }

    public void Restart()
    {
        GameManager.Restart();
    }
}
