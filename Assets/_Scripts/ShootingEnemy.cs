using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private Shooting shootScript;

    void Update()
    {
        shootScript.Shoot();
    }
}
