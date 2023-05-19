using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAtPlayer : MonoBehaviour
{
    [SerializeField] private float radius = 15f;
    [SerializeField] private float speed = 1f;
    private GameObject player;
    public Vector2 target;
    private bool targetAquired = false;
    private Vector2 startingPos;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        startingPos = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, startingPos) <= radius && !targetAquired)
        {
            target = new Vector2(player.transform.position.x, transform.position.y);
            startingPos = transform.position;

            targetAquired = true;
        }

        if (targetAquired)
        {
            if (transform.position.x != target.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            else
            {
                targetAquired = false;
            }
        }
    }
}
