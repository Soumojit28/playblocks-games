using UnityEngine;

public class Rock : MonoBehaviour
{
    private Transform spaceShipTransform;

    [SerializeField]
    internal RockData rockData;

    void Start()
    {
        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 forceDirection = (spaceShipTransform.position - transform.position).normalized;
        rb.AddForce(
            Random.Range(rockData.initialForce.x, rockData.initialForce.y)
                * rb.mass
                * forceDirection,
            ForceMode2D.Impulse
        );
        rb.AddTorque(Random.Range(rockData.initialForce.x * 50, rockData.initialForce.y * 50));
    }

    void FixedUpdate()
    {
        if (transform.position.y < spaceShipTransform.position.y - 5.0f)
        {
            Destroy(gameObject);
        }
    }

    void DestroyRock()
    {
        AudioManager.PlayAudio(AudioManager.GameAudio.Destroy);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (rockData.type) {
            case RockData.Type.Asteroid:
                HandleAsteroidCollision(collision.gameObject);
                break;
            case RockData.Type.Regen:
                HandleRegenCollision(collision.gameObject);
                break;
            case RockData.Type.Power:
                HandlePowerCollision(collision.gameObject);
                break;
        }
    }

    void HandleAsteroidCollision(GameObject collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.DecreaseLife();
        }

        if (collision.CompareTag("Lazer"))
        {
            ScoreManager.ObstacleDestroyed();
            ScoreManager.IncrementScore(50);
        }

        if (!collision.CompareTag("Enemy"))
            DestroyRock();
    }

    void HandleRegenCollision(GameObject collision)
    {
        if (collision.CompareTag("Lazer"))
        {
            ScoreManager.IncreaseLife();
            DestroyRock();
        }
    }

    void HandlePowerCollision(GameObject collision)
    {
        if (collision.CompareTag("Lazer"))
        {
            FindFirstObjectByType<SpaceShipController>().GetComponent<SpaceShipController>().ActivatePowerUp();
            DestroyRock();
        }
    }
}
