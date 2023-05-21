using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float cooldown = 1f;
    private Vector2 initPos;
    private bool moving = false;
    [SerializeField] private Animator anim;

    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        if (!moving)
        {
            anim.Play("snail idle");

            StartCoroutine(Patrol());
        }
        else
        {
            anim.Play("snail walk");
        }
    }

    private IEnumerator Patrol()
    {
        moving = true;

        while (transform.position.x != target.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(cooldown);
        Flip();

        while (transform.position.x != initPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, initPos, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(cooldown);
        Flip();

        moving = false;
    }

    private void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
