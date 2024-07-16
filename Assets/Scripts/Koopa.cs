using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            }
            else if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(shelled && collision.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                // if touched the pushed koopa again, die
                Player player = collision.GetComponent<Player>();
                if (player.starpower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }
            }
        }

        else if (!shelled && collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void EnterShell()
    {
        shelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;

        Destroy(gameObject, 3f);
    }

    //private void OnBecameInvisible()
    //{
    //    if (pushed)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
