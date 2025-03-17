using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreUI;

    [SerializeField]
    private TextMeshProUGUI destroyedUI;

    [SerializeField]
    private TextMeshProUGUI healthUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreUI.text = (0).ToString();
        destroyedUI.text = (0).ToString();
        healthUI.text = ("x" + ScoreManager.GetHealth());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreUI.text = ScoreManager.GetScore().ToString();
        destroyedUI.text = (
            ScoreManager.GetObstaclesDestroyed() + ScoreManager.GetShipsDestroyed()
        ).ToString();
        healthUI.text = ("x" + ScoreManager.GetHealth());
    }

    // public void OnPause(InputAction.CallbackContext context) {
    //     if (context.performed) {
    //         if (GameManager.isPaused) GameManager.ResumeGame();
    //         else GameManager.PauseGame();
    //     }
    // }

    // public void OnRestart(InputAction.CallbackContext context) {
    //     if (context.performed) {
    //         if (GameManager.isGameOver) GameManager.Restart();
    //     }
    // }
}
