using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float acceleration = 1.0f;

    [SerializeField]
    private int maxHitPoints = 2;

    private Transform spaceShipPosition;

    private Rigidbody2D _rigidbody;

    private int hitPoints;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        spaceShipPosition = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();

        hitPoints = maxHitPoints;
    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(transform.up * acceleration, ForceMode2D.Force);

        if (Mathf.Abs(transform.position.y - spaceShipPosition.position.y) > 15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.GetPlayer().DecreaseLife();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Lazer") || collision.gameObject.CompareTag("Rock"))
        {
            Lazer lazer = collision.gameObject.GetComponent<Lazer>();
            Rock rock = collision.gameObject.GetComponent<Rock>();

            if (
                (lazer != null && !lazer.IsEnemyLazer())
                || (rock != null && rock.rockData.type == RockData.Type.Asteroid)
            )
                hitPoints--;

            if (hitPoints <= 0)
                Destroy(gameObject);
        }
    }
}
