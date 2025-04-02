using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreUI;

    [SerializeField]
    private TextMeshProUGUI destroyedText;

    [SerializeField]
    private TextMeshProUGUI healthUI;

    [SerializeField]
    private TextMeshProUGUI bossHealthText;

    [SerializeField]
    private Image bossHealthBar;

    [SerializeField]
    private GameObject destroyedUI;

    [SerializeField]
    private GameObject bossHealthUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreUI.text = (0).ToString();
        destroyedText.text = (0).ToString();
        healthUI.text = "x" + GameManager.GetPlayer().GetHealth();

        ShipBoss shipBoss = FindFirstObjectByType<ShipBoss>();
        if (shipBoss)
        {
            bossHealthText.text = shipBoss.GetHealthPercentage() + "%";
            bossHealthBar.rectTransform.sizeDelta = new Vector2(
                shipBoss.GetHealthPercentage(),
                bossHealthBar.rectTransform.sizeDelta.y
            );
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreUI.text = ScoreManager.GetScore().ToString();
        destroyedText.text = (
            ScoreManager.GetObstaclesDestroyed() + ScoreManager.GetShipsDestroyed()
        ).ToString();
        healthUI.text = "x" + GameManager.GetPlayer().GetHealth();

        ShipBoss shipBoss = FindFirstObjectByType<ShipBoss>();
        if (shipBoss)
        {
            destroyedUI.SetActive(false);
            bossHealthUI.SetActive(true);
            bossHealthText.text = shipBoss.GetHealthPercentage() + "%";
            bossHealthBar.rectTransform.sizeDelta = new Vector2(
                shipBoss.GetHealthPercentage(),
                bossHealthBar.rectTransform.sizeDelta.y
            );
        }
        else
        {
            destroyedUI.SetActive(true);
            bossHealthUI.SetActive(false);
        }
    }
}
