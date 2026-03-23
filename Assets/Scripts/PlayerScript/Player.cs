using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private GameInput gameinput;
    //public ParticleSystem DUST;

    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private Animator ani;
    HeathManager heathManager;

    [SerializeField] public bool isLifepPlayer = true;
    [SerializeField] public bool isDamagePlayer = false;
    [SerializeField] private float SpeedPlayer;
    [SerializeField] private float JumpForce;

    [SerializeField] private bool IsRightFace = true;
    [SerializeField] public bool isJumped = false;
    [SerializeField] public bool isMove = false;
    [SerializeField] public bool isOnGround = true;

    [SerializeField] public float horizontalMove;
    public bool RightMoveInput = false;
    public bool LeftMoveInput = false;
    public bool JumpInput = false;
    void Start()
    {
        gameinput = GetComponent<GameInput>();
        rbPlayer = GetComponent<Rigidbody2D>();
        heathManager = GetComponent<HeathManager>();
    }
    void Update()
    {
        CheckLife(); // quan trong ne 
        PlayerMove();
    }
    private void FixedUpdate()
    {
       
    }
    private void CheckLife()
    {
        if (heathManager.isLife == false)
        {
            isLifepPlayer = false;
        }
    }
    private void PlayerMove()
    {
        //rbPlayer.velocity = new Vector2(horizontalMove, rbPlayer.velocity.y); // move code
        MoveWithKeyboard();
        //MoveWithPointer();
        RifhtFace();
        JumpCode();
    }
    void RifhtFace()
    {
        #region right face
        if (IsRightFace == true)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (IsRightFace == false)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        #endregion
    }
    private void JumpCode()
    {
        if ((Input.GetKeyDown(KeyCode.Space) && isOnGround)/* || JumpInput == true && isOnGround*/)
        {
            isJumped = true;
            //Debug.Log("space true");
            rbPlayer.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);// lực nhảy của player
            Debug.Log("nhay");
            isOnGround = false;
        }
        //else if (JumpInput == false && !isOnGround)
        //{
        //    isJumped = false;
        //}
    }

    #region MOVE WITH BUTTON
    #region pointer check
    public void PointerDownRight()
    { 
        RightMoveInput = true;
        Debug.Log("PT DOWN R");

    }
    public void PointerUpRight()
    {
        RightMoveInput = false;
        Debug.Log("PT UP R");
    }
    public void PointerDownLeft()
    {
        LeftMoveInput = true;
        Debug.Log("PT DOWN L");

    }
    public void PointerUpLeft()
    {
        LeftMoveInput = false;
        Debug.Log("PT UP L");

    }
    
    public void PointerDownJump()
    {
        JumpInput = true;
        isJumped = true;
        Debug.Log("hhhhhh");
    }
    public void PointerUpJump()
    {
        JumpInput = false;
        isJumped = false;
    }
    #endregion
    private void MoveWithPointer()
    {
        #region move bt
        if (LeftMoveInput == true)
        {
            isMove = true;
            IsRightFace = false;
            horizontalMove = -SpeedPlayer;
        }
        else if (RightMoveInput == true)
        {
            isMove = true;
            IsRightFace = true;
            horizontalMove = SpeedPlayer;
        }
        else
        {
            isMove = false;
            horizontalMove = 0;
        }
        #endregion

        #region jump pt
        //JumpCode();
        #endregion
    }
    #endregion
    #region MOVE WITH KEYBOARD
    private void MoveWithKeyboard()
    {
        //if (isLifepPlayer == false) { return; } // kiểm tra player còn sống không
        #region move

        float moveInput = gameinput.HorizontalInput;

        if (moveInput != 0)
        {
            isMove = true;
            rbPlayer.linearVelocity = new Vector2(moveInput * SpeedPlayer, rbPlayer.linearVelocity.y);
        }
        else
        {
            isMove = false;
        }
        #endregion

        #region right face
        if ( moveInput > 0)
        {
            IsRightFace = true;
        }
        else if (moveInput < 0)
        {
           IsRightFace = false;
        }
        #endregion
        //JumpCode();
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region check ground
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("check ground true");
            isOnGround = true; // Đặt biến isOnGround thành true khi va chạm với ground
            isJumped = false;
        }
        #endregion
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("check ground false");
            isOnGround = false;
            isJumped = false;
        }
    }
}
