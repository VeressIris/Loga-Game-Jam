using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool paused = false;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject[] hearts = new GameObject[3];
    private int playerHealth;

    void Awake()
    {
        playerHealth = PlayerPrefs.GetInt("PlayerHealth");

        LoadHearts();
    }

    void Start()
    {
        Time.timeScale = 1; // make sure game is playing
        pauseScreen.SetActive(false);
        healthBar.SetActive(true);
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
            hearts[i].SetActive(true);
        }
    }
}
