using UnityEngine;

public class Lazer : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    private Transform spaceShipPosition;

    [SerializeField]
    private float speed = 15.0f;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        spaceShipPosition = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        _rigidBody.linearVelocityY = speed;

        if (Mathf.Abs(transform.position.y - spaceShipPosition.position.y) > 15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }
}
