using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        float direction = playerController.transform.localScale.x;

        rb.velocity = direction * speed * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "ShootingEnemy")
        {
            EnemyController enemyController = collision.GetComponent<EnemyController>();
            enemyController.health--;

            Destroy(gameObject);
        }

    }
}
