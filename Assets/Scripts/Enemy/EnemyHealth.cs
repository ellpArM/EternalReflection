using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;

    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject pic;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.localScale = new Vector3(health / maxHealth, 1f, 1f);
        if (health <= 0)
        {
            StartCoroutine(wet());
        }
        pic.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 10;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator wet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}