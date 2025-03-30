using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreUI;

    [SerializeField]
    private TextMeshProUGUI destroyedUI;

    [SerializeField]
    private TextMeshProUGUI healthUI;

    [SerializeField]
    private TextMeshProUGUI bossHealth;

    [SerializeField]
    private Image bossHealthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreUI.text = (0).ToString();
        destroyedUI.text = (0).ToString();
        healthUI.text = "x" + ScoreManager.GetHealth();

        int bossHealthValue = FindFirstObjectByType<ShipBoss>().GetHealthPercentage();
        bossHealth.text = bossHealthValue + "%";
        bossHealthBar.rectTransform.sizeDelta = new Vector2(
            bossHealthValue,
            bossHealthBar.rectTransform.sizeDelta.y
        );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreUI.text = ScoreManager.GetScore().ToString();
        destroyedUI.text = (
            ScoreManager.GetObstaclesDestroyed() + ScoreManager.GetShipsDestroyed()
        ).ToString();
        healthUI.text = ("x" + ScoreManager.GetHealth());

        int bossHealthValue = FindFirstObjectByType<ShipBoss>().GetHealthPercentage();
        bossHealth.text = bossHealthValue + "%";
        bossHealthBar.rectTransform.sizeDelta = new Vector2(
            bossHealthValue,
            bossHealthBar.rectTransform.sizeDelta.y
        );
    }
}
