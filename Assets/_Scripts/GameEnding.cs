using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    [Header("Animation:")]
    [SerializeField] private Animator anim;
    [SerializeField] private string wakeAnim;
    [SerializeField] private string leaveAnim;
    
    [Header("Leaving:")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform target;
    private bool startLeaving = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !startLeaving)
        {
            anim.Play(wakeAnim);
        }
    }

    private void Update()
    {
        if (transform.position.x != target.transform.position.x && startLeaving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !startLeaving)
        {
            startLeaving = true;
            
            //flip sprite
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            
            anim.Play(leaveAnim);
        }
    }
}
