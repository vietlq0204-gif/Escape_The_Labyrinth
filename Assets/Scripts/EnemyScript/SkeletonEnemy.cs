using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkeletonEnemy : MonoBehaviour
{
    private EnemyManager enemyManager;
    private EnemyDamageManager enemyDamageManager;
    private EnemyHeathManager enemyHeathManager;

    [SerializeField] public bool isLifeSkeleton = true;


    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyDamageManager = GetComponent<EnemyDamageManager>();
        enemyHeathManager = GetComponent<EnemyHeathManager>();
    }
    private void Update()
    {
        CheckLife();
        Destroy();
    }
    private void CheckLife() // chua hoan thanh
    {
        if (enemyHeathManager.isLifeHeath == false)
        {
            isLifeSkeleton = false;
        }
    }

    private void Destroy() // chua hoan thanh
    {
        if (isLifeSkeleton == false)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D colition)
    {
        if (colition.CompareTag("Player"))
        {
            Destroy();
        }
    }
}
