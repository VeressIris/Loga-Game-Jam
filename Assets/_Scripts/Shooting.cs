using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform startPos;
    [SerializeField] private float shootCooldown = 1.8f;
    [HideInInspector] public bool canShoot = true;
    private bool inRange = true;
    private Transform playerTransform;
    [SerializeField] private float range = 30f;

    private void Start()
    {
        if (this.tag == "ShootingEnemy")
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    public void Shoot()
    {
        if (this.tag == "ShootingEnemy")
        {
            if (Vector2.Distance(transform.position, playerTransform.position) <= range)
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }
        }
        
        if (canShoot && inRange)
        {
            StartCoroutine(InstantiateProjectile());
        }
    }

    private IEnumerator InstantiateProjectile()
    {
        canShoot = false;
        
        Instantiate(projectile, startPos.position, startPos.transform.rotation);
        
        yield return new WaitForSeconds(shootCooldown);

        canShoot = true;
    }
}
