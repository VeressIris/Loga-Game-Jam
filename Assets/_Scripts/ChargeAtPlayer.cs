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
    [SerializeField] private Vector2 startingPos;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool turnAround = false;
    private float initLocalScale;
    [SerializeField] private Animator anim;

    [Header("Sound:")]
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip roar;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        startingPos = transform.position;
        initLocalScale = transform.localScale.x;

        anim.Play("bear idle");
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, startingPos) <= radius && !targetAquired)
        {
            StartCoroutine(AquireTarget());
        }
        
        if (targetAquired)
        {
            Debug.Log(FoundLedge());
            if (FoundLedge() && !turnAround)
            {
                turnAround = true;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            if (!turnAround)
            {
                StartCoroutine(MoveToTarget(target));
            }
            else
            {
                StartCoroutine(MoveToTarget(startingPos));
            }
        }
    }

    private IEnumerator AquireTarget()
    {
        target = new Vector2(player.transform.position.x, transform.position.y);
        
        transform.localScale = new Vector3(initLocalScale, transform.localScale.y, transform.localScale.z);

        anim.Play("bear roar");
        if (!audioSrc.isPlaying) audioSrc.PlayOneShot(roar, 0.625f);

        yield return new WaitForSeconds(1.14f);
        
        anim.Play("bear run");

        targetAquired = true;
    }

    private bool FoundLedge()
    {
        return !Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);
    }

    private IEnumerator MoveToTarget(Vector2 target)
    {
        if (transform.position.x != target.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            anim.Play("bear idle");

            yield return new WaitForSeconds(0.5f);

            targetAquired = false;
            turnAround = false;
        }
    }
}
