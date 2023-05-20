using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool paused = false;
    [SerializeField] private GameObject pauseScreen;

    void Start()
    {
        Time.timeScale = 1; // make sure game is playing
        pauseScreen.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }

        paused = !paused;
    }
}
