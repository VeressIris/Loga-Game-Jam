using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool paused = false;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject healthBar;
    public Image[] hearts = new Image[3];
    private int playerHealth;
    [SerializeField] private Sprite heartSprite;
    public Sprite greyHeart;
    [SerializeField] private TMP_Text timerText;
    [HideInInspector] public float timer = 0f;
    [SerializeField] private PlayerController playerController;

    void Awake()
    {
        playerHealth = PlayerPrefs.GetInt("PlayerHealth");
        timer = PlayerPrefs.GetFloat("Timer");

        LoadHearts();
    }

    void Start()
    {
        Time.timeScale = 1; // make sure game is playing
        pauseScreen.SetActive(false);
        healthBar.SetActive(true);
    }

    void Update()
    {
        if (playerController.health > 0)
        {
            timer += Time.deltaTime;

            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
            timerText.text = string.Format("{0:D2}:{1:D2}.{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
        }
    }

    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            healthBar.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            healthBar.SetActive(true);
        }

        paused = !paused;
    }

    private void LoadHearts()
    {
        for (int i = 0; i < playerHealth; i++)
        {
            hearts[i].sprite = heartSprite;
        }
    }
}
