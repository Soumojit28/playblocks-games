using UnityEngine;

public class HealthRock : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Lazer"))
        {
            ScoreManager.IncreaseLife();
            Destroy(gameObject);
            AudioManager.PlayAudio(AudioManager.GameAudio.Destroy);
        }
    }
}
