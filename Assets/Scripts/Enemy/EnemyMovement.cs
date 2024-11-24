using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;  // Reference to the player (if you want the boss to react to the player)
    public float maxZ = 5f;   // Maximum Z position boundary
    public float minZ = 0f;   // Minimum Z position boundary
    public float moveRangeX = 10f; // Horizontal movement range
    public float moveRangeY = 10f; // Vertical movement range
    public float timeToMove = 0.5f; // Time interval to move to a random location (in seconds)
    public float minDistanceToPlayer = 5f;
    public float dashSpeed = 100f;
    public bool moveable = true;

    private float moveTimer;
    private float freezeTimer;
    public bool demonKinger;

    [SerializeField] Animator anim;

    void Start()
    {
        if (navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();  // Ensure the NavMeshAgent is assigned
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        // Initialize timer
        moveTimer = timeToMove;
        if (demonKinger==true)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Wall"), true);
        }
    }

    void Update()
    {
        // Check if the NavMeshAgent is stuck or not moving
        if (!navMeshAgent.isOnNavMesh)
        {
            //Debug.Log("NavMeshAgent is not on the NavMesh!");
        }

        // Timer logic to move at regular intervals
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            // Move the boss to a random position
            MoveToRandomPosition();
            moveTimer = timeToMove; // Reset the timer
        }

        if (Vector3.Distance(transform.position, player.position) <= minDistanceToPlayer && moveable == true)
        {
            Debug.Log("EEE");
            MoveAwayFromPlayer();
            moveTimer = timeToMove;
            StartCoroutine(recharge());
            PushPlayerAway();
            //this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.down * dashSpeed);
        }

        if(this.GetComponent<EnemyHealth>().health <= 0)
        {
            anim.SetBool("Dead", true);
        }

        // Make sure Z-position is clamped
        ClampZPosition();
        if(Time.time - freezeTimer > 2f)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            freezeTimer = Time.time;
        }
    }

    void PushPlayerAway()
    {
        // Calculate direction from the boss to the player and normalize it
        Vector2 direction = (transform.position - player.position).normalized;

        // Apply force to the player to push them away
        player.gameObject.GetComponent<Rigidbody2D>().AddForce(-direction * dashSpeed*2);
    }

    private void FixedUpdate()
    {
        // Rotate the boss to face the player (Optional)
        Vector2 lookDir = player.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        
    }
    void MoveAwayFromPlayer()
    {
        // Calculate direction to move away from the player
        Vector3 direction = transform.position - player.position;
        direction.Normalize();  // Ensure direction is just a unit vector

        // Move the boss in that direction (away from the player)
        Vector3 moveTo = transform.position + direction * 100f;  // Move away by 2 units

        // Ensure the move destination is within valid bounds (you can adjust if necessary)
        NavMeshHit hit;
        if (NavMesh.SamplePosition(moveTo, out hit, 1f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            Debug.Log("Cannot find valid position to move away from player.");
        }
    }

    void MoveToRandomPosition()
    {
        // Generate a random point within a given range around the current position
        Vector3 randomDirection = new Vector3(Random.Range(-moveRangeX, moveRangeX), Random.Range(-moveRangeY, moveRangeY), 0f);

        // Ensure the random position is on the NavMesh
        Vector3 targetPosition = transform.position + randomDirection;

        // Check if the random position is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 1f, NavMesh.AllAreas))
        {
            // Move the boss to the valid random position
            navMeshAgent.SetDestination(hit.position);
            //Debug.Log("Moving to random position: " + hit.position);
        }
        else
        {
            Debug.Log("Random position not valid, retrying...");
        }
    }

    void ClampZPosition()
    {
        // Clamp Z-position to prevent it from going below or above certain boundaries
        Vector3 position = transform.position;
        position.z = Mathf.Clamp(position.z, minZ, maxZ);  // Clamp Z to stay within the desired range
        transform.position = position;

        // Optional: Debugging position
        //Debug.Log("Boss Position Z: " + transform.position.z);
    }

    IEnumerator recharge()
    {
        moveable = false;

        yield return new WaitForSeconds(0.5f);

        moveable = true;
    }

    public IEnumerator dodge()
    {
        yield return new WaitForSeconds(0.01f);
        int randomValue = Random.Range(0, 2);
        Debug.Log(randomValue);
        if(randomValue == 0)
        {
            this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.right * dashSpeed);
        }
        else if (randomValue == 1)
        {
            this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.left * dashSpeed);
        }
    }
}
