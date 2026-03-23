using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Checkzone : MonoBehaviour
{
    public DamgeManager damgeManager;

    public bool dangerousZone = false;
    public bool smallDmgZone = false;
    public bool EnemyColiZone = false; // va cham voi enemy
    
    void Start()
    {
    }

    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gai"))
        {
            dangerousZone = true;
            smallDmgZone = true;
            damgeManager.isDamageZone();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            dangerousZone = true;
            EnemyColiZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gai"))
        {
            dangerousZone = false;
            smallDmgZone = false;
        }
    }
}