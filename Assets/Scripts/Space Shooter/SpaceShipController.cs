using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void WebGLPowerUpActive(float duration);

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
    private Transform[] powerUpEmitters;

    [SerializeField]
    private AudioClip lazerAudio;

    private float horizontalInput = 0.0f;

    private AudioSource audioSource;

    private bool powerUpActive = false;

    private float powerUpEndTime;

    void Start()
    {
        _transform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float xPosition = Math.Clamp(
            horizontalInput,
            -horizontalMovementRange,
            horizontalMovementRange
        );
        float yPosition =
            _transform.position.y + (moveVelocity * Time.fixedDeltaTime * Vector3.up).y;

        _transform.position = new Vector3(xPosition, yPosition);

        // reset power up
        if (powerUpActive && Time.fixedTime > powerUpEndTime)
        {
            powerUpActive = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (GameManager.isGameOver)
            return;
        Vector2 inputPosition = context.ReadValue<Vector2>();
        Vector2 inputToWorldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
        horizontalInput = inputToWorldPosition.x;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (GameManager.isGameOver || GameManager.isPaused)
            return;
        if (context.performed)
        {
            Instantiate(lazerPrefab, lazerEmitter.position, Quaternion.identity);

            if (powerUpActive)
            {
                foreach (Transform emitter in powerUpEmitters)
                {
                    Instantiate(lazerPrefab, emitter.position, Quaternion.identity);
                }
            }

            audioSource.PlayOneShot(lazerAudio);
        }
    }

    internal void ActivatePowerUp(float duration = 40f)
    {
        powerUpEndTime = Time.fixedTime + duration;
        powerUpActive = true;

#if UNITY_WEBGL == true && UNITY_EDITOR == false
        WebGLPowerUpActive (duration);
#endif
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        if (context.performed)
        {
            GameManager.Restart();
        }
#endif
    }

    public void OnPause(InputAction.CallbackContext context)
    {
#if UNITY_EDITOR
        if (context.performed)
        {
            if (GameManager.isPaused)
            {
                GameManager.ResumeGame();
            }
            else
            {
                GameManager.PauseGame();
            }
        }
#endif
    }
}
