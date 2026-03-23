using System.Collections;
using UnityEngine;
/// <summary>
/// quản lí viên đạn
/// </summary>
public class bullet : MonoBehaviour
{
    private Rigidbody2D Bullet;
    public float speedBullet;
    private bool isBullet = true;
    private Vector2 bulletDirection; // hướng viên đạn
    [SerializeField] LayerMask collisionLayers; // layer khi bullet va cham
    [SerializeField] AudioClip bulletClip;


    void Start()
    {
        Bullet = GetComponent<Rigidbody2D>();
        moveBullet();
    }

    public void SetDirection(Vector2 direction) // Ghi nhận hướng từ AttackPlayer
    {
        bulletDirection = direction; 
    }

    private void Update()
    {
        StartCoroutine(BulletDestroyCoroutine());
    }

    void moveBullet()
    {
        Bullet.linearVelocity = bulletDirection.normalized * speedBullet;// thiết lập tốc độ cho đạn
    }

    private void Destroybullet()
    {
        if (isBullet == false)
        {
            Destroy(gameObject);
        }
    }//(test)
    IEnumerator BulletDestroyCoroutine() // (test)
    {
        yield return new WaitForSeconds(0.2f); // Chờ trước khi cho phép tấn công lại
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (((1 << collision.gameObject.layer) & collisionLayers) != 0)
        //{
        //    Debug.Log($" colider: {collision.name}");
        //    //AudioSource.PlayClipAtPoint(bulletClip, Camera.main.transform.position); // Tạm tắt để test
        //    isBullet = false;
        //}
    }
}
