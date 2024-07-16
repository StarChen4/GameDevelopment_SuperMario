using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f;

    private SpriteRenderer SpriteRenderer;
    private int frame;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Animate()
    {
        frame++;

        if (frame >= sprites.Length)
        {
            frame = 0;
        }
        if(frame >= 0 && frame < sprites.Length)
        {
            SpriteRenderer.sprite = sprites[frame];
        }
    }
}
