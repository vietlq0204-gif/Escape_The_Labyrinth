using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float movespeed = 2f; //tốc độ di chuyển của slime
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveDirection = Vector2.right; //bắt đầu slime di chuyển về phía bên phải
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //di chuyển slime 
        rb.linearVelocity = moveDirection * movespeed;

        //đổi hướng khi va chạm vật cản
        if (transform.position.x >= 2f)
        {
            moveDirection = Vector2.left;
            FlipSprite(true);
        }
        else if (transform.position.x <= -2f)
        {
            moveDirection = Vector2.right;
            FlipSprite(false);
        }
    }

    void FlipSprite(bool facingRight)
    {
        //lật sprite của slime để phản ánh hướng di chuyển
        transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //xử lí hành động khi va chạm vào người chơi
            Die();
        }
    }
    void Die()
    {
        //thực hiện hành động khi quái vật chết
        Debug.Log("quái vật đã chết!");
        Destroy(gameObject);//hủy GameObject của quái vật
    }
}
