using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ItemManager : MonoBehaviour
{
    public OncoliderPlayerManager oncoliderPlayerManager;
    [SerializeField] public List<int> listHeathPotion = new List<int>();

    void Start()
    {
    }

    void Update()
    {
        //AddHeathPotionList();
    }
    public void AddHeathPotionList()
    {
        if (listHeathPotion.Count >= 0)
        {
            Debug.Log($"heath potion in list: {listHeathPotion.Count}");
        }
    }
}
