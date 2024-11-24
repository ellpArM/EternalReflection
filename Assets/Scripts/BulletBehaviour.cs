using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 10f;            // Speed of the bullet
    //public int maxBounces = 3;          // Maximum number of bounces before the bullet is destroyed
    public Rigidbody2D rb;              // Rigidbody2D component
    public SpriteRenderer spriteRenderer; // Reference to the bullet's sprite renderer

    private int bounceCount = 0;        // Tracks the number of bounces

    void Start()
    {
        // Move the bullet in the forward direction
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a wall
        if (collision.collider.CompareTag("Wall"))
        {
            bounceCount++;

            // Change color to red after the first bounce
            if (bounceCount == 1)
            {
                spriteRenderer.color = Color.red;
            }
        }
    }
}
