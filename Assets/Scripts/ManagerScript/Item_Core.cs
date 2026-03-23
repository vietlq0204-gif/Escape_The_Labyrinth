using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Item_Core : MonoBehaviour
{
    private Animator ani;
    //Particle coin
    //[SerializeField] AudioClip coinclip;
    //public GameObject coineffect;

    [SerializeField] public Image ImageItem;
    [SerializeField] public string NameItem;
    [SerializeField] public string descriptionTxtItem;

    [SerializeField] public bool IsCoin = false;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void GetItemInfo(out string name, out string description)
    {
        name = NameItem;
        description = descriptionTxtItem;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //AudioSource.PlayClipAtPoint(coinclip, Camera.main.transform.position);
            gameObject.SetActive(false);
            IsCoin = true;
            Destroy(gameObject);

            //GameObject hieuung = Instantiate(coineffect, transform.position, transform.localRotation);
            //Destroy(hieuung, 4);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsCoin = false;
        }
    }
}
