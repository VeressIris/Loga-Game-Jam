using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    private EnemyController enemyController;

    void Start()
    {
        enemyController = GameObject.FindWithTag("ShootingEnemy").GetComponent<EnemyController>();

        rb.velocity = transform.right * speed * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemyController.DamagePlayer();
            
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
