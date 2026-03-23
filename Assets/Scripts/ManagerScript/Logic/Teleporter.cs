using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Animator ani;
    public GameObject CurentTeleporter;
    [SerializeField] private Transform distination;
    GameObject player;
    private void Update()
    {
        InputKey();
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    IEnumerator DelayTeleport()
    {
        ani.SetTrigger("endMap");
        yield return new WaitForSeconds(1f);
        player.transform.position = distination.transform.position;
        yield return new WaitForSeconds(1f);
        ani.SetTrigger("startMap");
    }
    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.E)/* || Input.GetMouseButton(1)*/)
        {
            if (CurentTeleporter != null)
            {
                StartCoroutine(DelayTeleport());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CurentTeleporter = collision.gameObject;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject == CurentTeleporter)
            {
                CurentTeleporter = null;
            }
        }
    }
}
