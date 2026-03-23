using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GamePlay gamePlay;
    public PlayerManager playerManager;
    public AttackPlayer attackPlayer;
    public DamgeManager damgeManager;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Update()
    {
        playerManager.Movement.x = Input.GetAxisRaw("Horizontal");
        playerManager.Movement.y = Input.GetAxisRaw("Vertical");
    }

    private void OnEnable()
    {
        playerInput.Enable();

        // Đăng ký các sự kiện với các hành động đầu vào.
        playerInput.Player.Jump.performed += OnJump;
        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;
        playerInput.Player.Fire.performed += OnAttack;
        playerInput.Player.Fire.canceled += OnFireCancel;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        // Hủy đăng ký các sự kiện đầu vào để tránh rò rỉ bộ nhớ.
        playerInput.Player.Jump.performed -= OnJump;
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;
        playerInput.Player.Fire.performed -= OnAttack;
        playerInput.Player.Fire.canceled -= OnFireCancel;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        playerManager.isJumped = true;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>(); // Đọc tọa độ Move (Test)
        playerManager.isMove = true;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        string controlName = context.control.name;

        if (controlName == "leftButton")
        {
            if (context.control.IsPressed()) // Giữ chuột trái
            {
                //Debug.Log("Giữ chuột trái");
                gamePlay.canAttack = true;
            }
        }
        else if (controlName == "rightButton")
        {
            if (context.control.IsPressed()) //Giữ chuột phải
            {
                //Debug.Log("Giữ chuột phải");
            }
        }
    }

    private void OnFireCancel(InputAction.CallbackContext context)
    {
        string controlName = context.control.name;

        if (controlName == "leftButton")
        {
            gamePlay.canAttack = false;
            //damgeManager.isAttacked = false;
            //Debug.Log("nhả Chuột trái");
        }
        else if (controlName == "rightButton")
        {
            //Debug.Log("nhả Chuột phải");
        }
    }
}
