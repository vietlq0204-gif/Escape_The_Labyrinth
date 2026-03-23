using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private bool IsLifeCup = true;
    [SerializeField] private ScoreManager scoreManager;
    private void Update()
    {
        DestroyCup();
    }
    private void DestroyCup()
    {
        if (!IsLifeCup)
        {
            Destroy(gameObject); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsLifeCup = false;
        }
    }
}
