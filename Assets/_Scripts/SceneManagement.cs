using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private Animator transitionAnim;
    private float transitionLength = 1f;

    void Start()
    {
        Time.timeScale = 1;
        Debug.Log(PlayerPrefs.GetInt("PlayerHealth"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel(int lvl)
    {
        StartCoroutine(PlayTransition(lvl));
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        //StartCoroutine(PlayTransition(0));
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //StartCoroutine(PlayTransition(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator PlayTransition(int lvlIndex)
    {
        transitionAnim.SetTrigger("start");
        yield return new WaitForSeconds(transitionLength);
        SceneManager.LoadScene(lvlIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.tag != "Interactable")
        {
            LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
