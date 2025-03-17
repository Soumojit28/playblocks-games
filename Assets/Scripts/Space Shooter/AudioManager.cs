using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioClip gameOver;
    [SerializeField]
    private AudioClip damage;
    [SerializeField]
    private AudioClip destroy;

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
            case GameAudio.Damage: {
                audioSource.PlayOneShot(Instance.damage);
                break;
            }
            case GameAudio.Destroy: {
                audioSource.PlayOneShot(Instance.destroy);
                break;
            }
        }
    }

    public enum GameAudio
    {
        GameOver,
        Damage,
        Destroy
    }
}
