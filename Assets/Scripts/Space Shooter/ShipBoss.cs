using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class ShipBoss : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void WebGLShipBossDefeated();

    [System.Serializable]
    public class Emitter
    {
        public Transform transform;
        public float directionAngle;
    }

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private float moveVelocity = 1.0f;

    [SerializeField]
    private Transform parentTransform;

    [SerializeField]
    private float hoverDistance = 15.0f;

    [SerializeField]
    private Transform lazerEmitter;

    [SerializeField]
    private GameObject lazerPrefab;

    [SerializeField]
    private int burstLazers = 3;

    [SerializeField]
    private float burstDelay = 0.1f;

    [SerializeField]
    private Emitter[] rocketEmitters;

    [SerializeField]
    private GameObject rocketPrefab;

    private Transform spaceShipTransform;

    private Animator _animator;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        spaceShipTransform = FindFirstObjectByType<SpaceShipController>().GetComponent<Transform>();

        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (parentTransform.position.y - spaceShipTransform.position.y <= hoverDistance)
        {
            float yPosition =
                parentTransform.position.y + (moveVelocity * Time.fixedDeltaTime * Vector3.up).y;
            parentTransform.position = new Vector3(parentTransform.position.x, yPosition);
        }
    }

    internal void FireLazer()
    {
        StartCoroutine(FireLazerBurst(burstLazers));
    }

    private IEnumerator FireLazerBurst(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(lazerPrefab, lazerEmitter.position, Quaternion.identity);
            yield return new WaitForSeconds(burstDelay);
        }
    }

    internal void FireRocket()
    {
        Emitter selectedEmitter = rocketEmitters[Random.Range(0, rocketEmitters.Length)];

        Instantiate(
            rocketPrefab,
            selectedEmitter.transform.position,
            Quaternion.Euler(0, 0, selectedEmitter.directionAngle)
        );
    }

    internal void FireTargetedRocket()
    {
        Emitter selectedEmitter = rocketEmitters[Random.Range(0, rocketEmitters.Length)];

        Vector3 direction = (
            spaceShipTransform.position - selectedEmitter.transform.position
        ).normalized;
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        Instantiate(
            rocketPrefab,
            selectedEmitter.transform.position,
            Quaternion.Euler(0, 0, angle + 90)
        );
    }

    internal int GetHealthPercentage()
    {
        return (int)((float)currentHealth / maxHealth * 100);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lazer"))
        {
            currentHealth--;
        }

        if (currentHealth <= (0.7 * maxHealth))
        {
            _animator.SetInteger("Stage", 2);
        }

        if (currentHealth <= (0.4 * maxHealth))
        {
            _animator.SetInteger("Stage", 3);
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            
            ScoreManager.IncrementScore(500);
            ScoreManager.ProgressLevel();

#if UNITY_WEBGL == true && UNITY_EDITOR == false
        WebGLShipBossDefeated ();
#endif
        }
    }
}
