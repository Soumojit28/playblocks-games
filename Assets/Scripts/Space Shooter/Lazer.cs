using UnityEngine;

public class Lazer : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private Transform spaceShipPosition;

    [SerializeField]
    private float speed = 15.0f;

    [SerializeField]
    private bool enemyLazer = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        spaceShipPosition = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        _rigidBody.linearVelocityY = enemyLazer ? -speed : speed;

        if (Mathf.Abs(transform.position.y - spaceShipPosition.position.y) > 15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLazer && collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.DecreaseLife();
        }

        if (
            !(enemyLazer && collision.gameObject.CompareTag("Enemy"))
            && !(!enemyLazer && collision.gameObject.CompareTag("Player"))
        )
            Destroy(gameObject);
    }

    internal bool IsEnemyLazer() {
        return enemyLazer;
    }
}
