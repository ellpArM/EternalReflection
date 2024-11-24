using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public float dashSpeed = 20f; // Speed of the player
    public Rigidbody2D rb;      // Rigidbody2D component
    public Camera cam;          // Reference to the main camera

    private Vector2 movement;   // Movement input vector
    private Vector2 mousePos;   // Mouse position in world space
    public bool dashed;

    void Update()
    {
        // Get movement input (WASD or arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Get mouse position in world space
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Move the player
        rb.AddForce(movement.normalized * moveSpeed);

        // Rotate the player to face the mouse cursor
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        if (Input.GetKeyDown(KeyCode.Space) && dashed == false)
        {
            Dash();
        }
    }
    void Dash()
    {
        // Dash the player in the direction they are facing
        Vector2 dashDirection = rb.velocity.normalized; // Direction player is moving
        rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse); // Apply an impulse for dashing
        StartCoroutine(dashTime());
    }

    IEnumerator dashTime()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bullets"), true);
        dashed = true;
        yield return new WaitForSeconds(1f);
        dashed = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Bullets"), false);
    }
}
