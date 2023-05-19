using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("PlayerHealth", 3);

        Debug.Log(PlayerPrefs.GetInt("PlayerHealth"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
