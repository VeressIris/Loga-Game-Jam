using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [Header("Damage:")]
    [SerializeField] private int damage = 1;
    private PlayerController playerController;
    private GameManager gameManager;
    [Header("Health:")]
    public int health = 3;

    [Header("SFX:")]
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip deathSFX;
    private bool triggered = false;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (health == 0)
        {
            if (!triggered && !audioSrc.isPlaying)
            {
                triggered = true;
                audioSrc.PlayOneShot(deathSFX);
            }
            else if (triggered && !audioSrc.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
            PlayerPrefs.SetFloat("Timer", gameManager.timer);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            gameManager.hearts[0].sprite = gameManager.greyHeart;
        }
    }
}
