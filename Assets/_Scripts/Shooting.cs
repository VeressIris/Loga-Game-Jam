using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform startPos;
    [SerializeField] private float shootCooldown = 1.8f;
    [HideInInspector] public bool canShoot = true;

    public void Shoot()
    {
        if (canShoot)
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
