using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private Transform spaceShipTransform;

    [SerializeField]
    private Vector2 moveSpeed = new Vector2(1.0f, 2.5f);

    [SerializeField]
    private Transform lazerEmitter;

    [SerializeField]
    private GameObject lazerPrefab;

    [SerializeField]
    private float shootDelay = 2.0f;

    private float lastShootTime;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();
        lastShootTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.AddForceY(-Random.Range(moveSpeed.x, moveSpeed.y));

        if (transform.position.y < spaceShipTransform.position.y - 5.0f)
        {
            Destroy(gameObject);
        }

        if (Time.time - lastShootTime > shootDelay && Mathf.Abs(transform.position.y - spaceShipTransform.position.y) < 10.0f)
        {
            // Check if player is in front of the enemy
            RaycastHit2D hit = Physics2D.Raycast(lazerEmitter.position, Vector2.down);
            if (hit.collider && hit.collider.CompareTag("Player"))
            {
                ShootLazer();
            }
        }
    }

    void ShootLazer()
    {
        Instantiate(lazerPrefab, lazerEmitter.position, Quaternion.identity);
        lastShootTime = Time.time;
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
            ScoreManager.IncrementScore(100);
            ScoreManager.ShipDestroyed();
            Destroy(gameObject);
            AudioManager.PlayAudio(AudioManager.GameAudio.Destroy);
        }
    }
}
