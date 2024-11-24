using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;             // Player's position
    public float speed = 3f;             // Movement speed
    public float detectionRange = 10f;   // Distance to detect the player
    public float runAwayRange = 5f;      // Distance within which the boss runs away
    public LayerMask wallMask;           // Layer mask for walls
    public float searchRadius = 5f;      // Radius to search for hiding spots

    private Vector2 targetPosition;      // Current target position
    private bool isRunningAway = false;  // Is the boss currently running away?
    private bool isHiding = false;       // Is the boss hiding behind a wall?

    private Rigidbody2D rb;              // Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= runAwayRange)
            {
                // Run away from the player
                isRunningAway = true;
                RunAwayFromPlayer();
            }
            else
            {
                // Otherwise, either hide or chase the player
                if (isHiding)
                {
                    MoveToTarget(); // Move to hiding spot
                }
                else
                {
                    // Find a hiding spot behind walls
                    FindHidingSpot();
                }
            }
        }
        else
        {
            // If not detecting the player, idle or patrol logic could go here
            isRunningAway = false;
        }
    }

    void RunAwayFromPlayer()
    {
        // Calculate the direction away from the player
        Vector2 directionAwayFromPlayer = (Vector2)transform.position - (Vector2)player.position;
        directionAwayFromPlayer.Normalize(); // Ensure unit vector for correct movement

        // Apply velocity to run away from the player
        rb.velocity = directionAwayFromPlayer * speed;

        // Rotate to face the opposite direction of the player
        float angle = Mathf.Atan2(directionAwayFromPlayer.y, directionAwayFromPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    void FindHidingSpot()
    {
        // Search for walls in the vicinity
        Collider2D[] walls = Physics2D.OverlapCircleAll(transform.position, searchRadius, wallMask);

        float bestHidingSpotDistance = Mathf.Infinity;
        Vector2 bestHidingSpot = transform.position;

        foreach (Collider2D wall in walls)
        {
            // Calculate a position "behind" the wall, away from the player
            Vector2 directionToWall = wall.transform.position - player.position;
            Vector2 hidingSpot = (Vector2)wall.transform.position + directionToWall.normalized;

            float distanceToHidingSpot = Vector2.Distance(transform.position, hidingSpot);

            // Choose the closest hiding spot
            if (distanceToHidingSpot < bestHidingSpotDistance)
            {
                bestHidingSpotDistance = distanceToHidingSpot;
                bestHidingSpot = hidingSpot;
            }
        }

        // If a hiding spot is found, move to it
        if (bestHidingSpotDistance < Mathf.Infinity)
        {
            targetPosition = bestHidingSpot;
            isHiding = true;
            MoveToTarget();
        }
        else
        {
            // If no hiding spot found, chase or run away
            isHiding = false;
            targetPosition = player.position;
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        // If the boss is hiding or running away, move towards that position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Apply velocity for movement
        rb.velocity = direction * speed;

        // Rotate to face the target (either the player or hiding spot)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    void OnDrawGizmos()
    {
        // Visualize detection and hiding search ranges in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runAwayRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
