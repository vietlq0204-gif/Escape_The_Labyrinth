using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    public PlayerManager playerManager;
    public DamgeManager damageManager;
    public HeathManager heathManager;
    public ScoreManager scoreManager;
    public GameObject player;
    public GameObject cupItem;

    public bool canAttack = false;
    public bool isWinGame = false;
    void Start()
    {
        if (playerManager == null)
        {
            playerManager = Object.FindFirstObjectByType<PlayerManager>();
        }
        if (scoreManager == null)
        {
            scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        }
        if (heathManager == null)
        {
            heathManager = Object.FindFirstObjectByType<HeathManager>();
        }
        if (damageManager == null)
        {
            damageManager = Object.FindFirstObjectByType<DamgeManager>();
        }
    }
    //private void Awake()
    //{
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}
    private void Update()
    {
        DestroyGameObject();
        ActiceCup();
    }
    public void DestroyGameObject()
    {
        if (heathManager.listHeathInGame != null && heathManager.listHeathInGame[0].heathInGame <= 0)
        {
            Destroy(player);
        }
    }
    public void ActiceCup()
    {
        if (scoreManager.listCoinInGame != null && scoreManager.listCoinInGame.Count > 0 && scoreManager.listCoinInGame[0].CoinInGame > 10 && !isWinGame)
        {
            cupItem.SetActive(true);
        }
        else
        {
            cupItem.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            if (scoreManager.listCoinInGame != null && scoreManager.listCoinInGame.Count > 0 && scoreManager.listCoinInGame[0].CoinInGame > 10)
            {
                isWinGame = true;
            }

        }
    }
}
