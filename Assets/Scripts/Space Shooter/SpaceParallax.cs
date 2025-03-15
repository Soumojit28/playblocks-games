using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    [SerializeField]
    private float effectStrength;

    [SerializeField]
    private bool isBackground;

    [SerializeField]
    private float propEffectClamp = 10.0f;

    [SerializeField]
    private bool repeatSprite = false;

    private GameObject cam;

    private float startpos;
    private float spriteLength;
    private float initialOffset;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        startpos = transform.position.y;
        initialOffset = startpos - cam.transform.position.y;

        if (repeatSprite)
            spriteLength = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        float dist = (cam.transform.position.y * effectStrength);

        if (!isBackground)
            transform.position = new Vector2(
                transform.position.x,
                startpos + (dist / propEffectClamp)
            );
        else
            transform.position = new Vector2(transform.position.x, startpos + dist);

        if (repeatSprite)
        {
            float camPos = (cam.transform.position.y * (1 - effectStrength));

            if (camPos > startpos + spriteLength - initialOffset)
                startpos += spriteLength;
            else if (camPos < startpos - spriteLength - initialOffset)
                startpos -= spriteLength;
        }
    }
}
