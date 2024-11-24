using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;

    [SerializeField] GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.localScale = new Vector3(health/maxHealth, 1f, 1f);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet" && this.GetComponent<PlayerMovement>().dashed == false)
        {
            health -= 10;
            Destroy(collision.gameObject);
        }
    }
}
