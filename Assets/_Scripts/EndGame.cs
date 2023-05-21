using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private string text;
    [SerializeField] private TMP_Text TMPText;
    [SerializeField] private SceneManagement sceneManagement;

    void Start()
    {
        TMPText.text = ""; //make sure the text box is empty
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TMPText.text = text;
            textBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            textBox.SetActive(false);
            sceneManagement.LoadNextLevel();
        }
    }
}
