using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioClip gameOver;

    private static AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayAudio(GameAudio gameAudio) {
        switch (gameAudio) {
            case GameAudio.GameOver: {
                audioSource.PlayOneShot(Instance.gameOver);
                break;
            }
        }
    }

    public enum GameAudio
    {
        GameOver
    }
}
