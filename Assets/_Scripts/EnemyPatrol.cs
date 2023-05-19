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

    void Start()
    {
        initPos = transform.position;
        
        //target.position += new Vector3(target.position.x, transform.position.y, 0);

        //StartCoroutine(Patrol());
    }

    void Update()
    {
        if (!moving)
        {
            StartCoroutine(Patrol());
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

        while (transform.position.x != initPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, initPos, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(cooldown);

        moving = false;
    }
}
