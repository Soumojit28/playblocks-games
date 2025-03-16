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

    private float horizontalInput = 0.0f;

    void Awake()
    {
        _transform = GetComponent<Transform>();
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
        }
    }
}
