using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipController : MonoBehaviour
{
    Transform _transform;

    [SerializeField]
    private float moveVelocity = 1.0f;

    [SerializeField]
    private float horizontalMovementRange = 5.0f;

    [SerializeField]
    private GameObject lazerPrefab;

    [SerializeField]
    private Transform lazerEmitter;

    [SerializeField]
    private AudioClip lazerAudio;

    private float horizontalInput = 0.0f;

    private AudioSource audioSource;

    void Start()
    {
        _transform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float xPosition = Math.Clamp(horizontalInput, -horizontalMovementRange, horizontalMovementRange);
        float yPosition =
            _transform.position.y + (moveVelocity * Time.fixedDeltaTime * Vector3.up).y;

        _transform.position = new Vector3(xPosition, yPosition);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (GameManager.isGameOver) return;
        Vector2 inputPosition = context.ReadValue<Vector2>();
        Vector2 inputToWorldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
        horizontalInput = inputToWorldPosition.x;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (GameManager.isGameOver) return;
        if (context.performed)
        {
            Instantiate(lazerPrefab, lazerEmitter.position, Quaternion.identity);
            audioSource.PlayOneShot(lazerAudio);
        }
    }
}
