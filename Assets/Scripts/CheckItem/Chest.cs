using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator ani;

    public bool Ischest = false;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        OpenChest();
    }
    public void OpenChest()
    {
        ani.SetBool("Open", Ischest == true);
    }
    private void OnCollisionEnter2D(Collision2D Chest)
    {
        if (Chest.gameObject.CompareTag("Player"))
        {
            Ischest = true;
        }
    }
    private void OnCollisionExit2D(Collision2D Chest)
    {
        if (Chest.gameObject.CompareTag("Player"))
        {
            Ischest = false;
        }
    }
}
