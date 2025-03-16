using UnityEngine;

public class Rock : MonoBehaviour
{
    private Transform spaceShipTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < spaceShipTransform.position.y - 5.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.DecreaseLife();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Lazer"))
        {
            ScoreManager.ObstacleDestroyed();
            ScoreManager.IncrementScore(50);
            Destroy(gameObject);
        }
    }
}
