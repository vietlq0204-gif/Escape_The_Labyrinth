using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// cs trên gán vào Player
[System.Serializable]
public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    public Animator ani;
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    public ScoreManager scoreManager;
    public HeathManager heathManager;
    public DamgeManager damgeManager;

    public bool isLifepPlayer = true;
    public Vector2 Movement;
    public float Speed;
    private float JumpingPower;
    //private bool isRightFaceing = true;
    public bool isOnGround = false;
    public bool isJumped = false;
    public bool isMove = false;

    //public bool Front = false;
    //public bool Back = false;
    //public bool Left = false;
    //public bool Right = false;

    private void Start()
    {
    }
    void Update()
    {
        Ani();
        CheckLife();
        Move();
    }
    void FixedUpdate()
    {
       
    }

    private void CheckLife()
    {
        if (!heathManager.isLife)
        {
            isLifepPlayer = false;
        }
        else
        {
            isLifepPlayer = true;
        }
    }
    private void Move() // Di chuyển nhân vật
    {
        if (isMove)
        {
            rbPlayer.MovePosition(rbPlayer.position + Movement * Speed * Time.fixedDeltaTime);
        }
    }

    public void Jump(/*InputAction.CallbackContext context*/)
    {
        if (/*context.performed && */isOnGround && isJumped)
        {
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, JumpingPower);
            isOnGround = false;
        }
        if (/*context.canceled && */rbPlayer.linearVelocity.y < 0f) // Kiểm tra nếu hành động nhảy bị hủy và vận tốc trục Y của Rigidbody2D đang hướng xuống
        {
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, rbPlayer.linearVelocity.y * 0.1f);// Giảm vận tốc trục Y để tạo cảm giác rơi nhẹ nhàng hơn

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region check ground
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("check ground true");
            isOnGround = true; // Đặt biến isOnGround thành true khi va chạm với ground
            if (isOnGround == true)
            {
                isJumped = false;
            }
        }
        #endregion
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            scoreManager.IsCoin = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("check ground false");
            isOnGround = false;
            if (isOnGround == false)
            {
                isJumped = false;
            }
        }
    }
    private void Ani()
    {
        ani.SetFloat("Horizontal", Movement.x);
        ani.SetFloat("Vertical", Movement.y);
        ani.SetFloat("Speed", Movement.sqrMagnitude);
    }
}
