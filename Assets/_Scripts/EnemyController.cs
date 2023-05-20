using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [Header("Health:")]
    public int health = 3;
    
    [Header("Damage:")]
    [SerializeField] private int damage = 1;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DamagePlayer();
        }
    }

    public void DamagePlayer()
    {
        playerController.health -= damage;
        
        if (playerController.health > 0)
        {
            //restart this level
            PlayerPrefs.SetInt("PlayerHealth", playerController.health);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            //restart game
            PlayerPrefs.SetInt("PlayerHealth", 3); //give player the health back
            SceneManager.LoadScene(1);
        }
    }
}
