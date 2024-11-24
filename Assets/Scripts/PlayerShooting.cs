using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public GameObject bulletparent;  // Reference to the bullet prefab
    public Transform firePoint;     // The point where bullets are spawned
    public float shootCooldown = 3f; // Cooldown time between shots (in seconds)

    private float cooldownTimer = 0f; // Tracks time since the last shot

    [SerializeField] AudioSource pew;

    void Update()
    {
        // Update the cooldown timer
        cooldownTimer += Time.deltaTime;

        // Check for left mouse button click and if cooldown is complete
        if (Input.GetMouseButtonDown(0) && cooldownTimer >= shootCooldown)
        {
            Shoot();
            StartCoroutine(GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>().dodge());
            cooldownTimer = 0f; // Reset the cooldown timer
        }
    }

    void Shoot()
    {
        // Spawn the bullet at the fire point's position and rotation
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, bulletparent.transform);
        pew.Play();
    }
}
