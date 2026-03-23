using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OncoliderPlayerManager : MonoBehaviour
{
    public ItemManager itemManager;
    public Puzzle3_1 puzzle3_1;
    public bool IsCoin = false;
    public bool IsHeathPotion = false;

    public bool IsChestGuide = false;
    public bool IsDoorGuide = false;
    public bool IsTeleportGuide = false;

    private int Puzzle;
    public bool IsPuzzle= false;
    public bool cell1= false;
    public bool cell2= false;
    public bool cell3= false;
    public bool cell4= false;
    private void Awake()
    {
        Puzzle = LayerMask.NameToLayer("Puzzle");
    }
    // ENTER
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chest"))
        {
            IsChestGuide = true;
        }
        if (collision.gameObject.CompareTag("DoorRoom"))
        {
            IsDoorGuide = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("teleport"))
        {
            IsTeleportGuide = true;

        }
        if (collision.CompareTag("coin"))
        {
            IsCoin = true;
        }
        if (collision.CompareTag("heathPotion"))
        {
            IsHeathPotion = true;
            itemManager.listHeathPotion.Add(itemManager.listHeathPotion.Count + 1);
            //Debug.Log($"HEATH POTION:");
        }
        if (collision.gameObject.layer == Puzzle)
        {
           IsPuzzle = true;
            Debug.Log("Cell Enter 3_1");
        }

    }
    // EXIT
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chest"))
        {
            IsChestGuide = false;
        }
        if (collision.gameObject.CompareTag("DoorRoom"))
        {
            IsDoorGuide = false;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D called with tag: " + collision.tag);
        if (collision.CompareTag("teleport"))
        {
            IsTeleportGuide = false;
        }
        if (collision.CompareTag("coin"))
        {
            IsCoin = false;
        }
        if (collision.CompareTag("heathPotion"))
        {
            IsHeathPotion = false;
        }
        if (collision.gameObject.layer == Puzzle)
        {
            IsPuzzle = false;
            Debug.Log("Cell Exit 3_1");
        }
    }
}
