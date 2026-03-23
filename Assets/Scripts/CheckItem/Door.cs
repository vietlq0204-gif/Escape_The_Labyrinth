using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public GameObject DoorOpen;
    public GameObject DoorClose;
    public bool isDoorof = false;
    public bool isDooron = false;
    //public bool doorIsOpen = false;
    //private bool CHECKDOOR = false;
    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isDoorof )
            {
                ToggleDooron();
            }
            else
            {
                ToggleDoorof();
            }
        }
    }

    private void ToggleDooron()
    {
        DoorOpen.SetActive(true);
        DoorClose.SetActive(false);
    }
    private void ToggleDoorof()
    {
        DoorOpen.SetActive(false);
        DoorClose.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDoorof = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDooron = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDoorof = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDooron = false;
        }
    }
}
