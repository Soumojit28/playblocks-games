using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private Transform spaceShipTransform;

    [SerializeField]
    private Vector2 moveSpeed = new Vector2(1.0f, 2.5f);

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.AddForceY(-Random.Range(moveSpeed.x, moveSpeed.y));

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
            ScoreManager.IncrementScore(100);
            ScoreManager.ShipDestroyed();
            Destroy(gameObject);
        }
    }
}
