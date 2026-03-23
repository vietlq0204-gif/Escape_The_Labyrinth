using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapSound : MonoBehaviour
{
    [SerializeField] AudioClip TrapClip;
    private AudioSource audioSource;
    public float delaySound = 1.0f; // Thời gian chờ trước khi phát âm thanh (1 giây)
    void Start()
    {
        // Tạo một GameObject chứa AudioSource nếu nó chưa tồn tại
        GameObject audioObject = new GameObject("BulletSound");
        audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = TrapClip;
        //audioSource.loop = true; // Đặt để phát lặp lại
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //AudioSource.PlayClipAtPoint(bulletClip, Camera.main.transform.position);
            audioSource.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlaySoundWithDelay());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlaySoundWithDelay());
        }
    }
    private IEnumerator PlaySoundWithDelay()
    {
        
        audioSource.Play();
        yield return new WaitForSeconds(delaySound); // Chờ 1 giây
    }
}
