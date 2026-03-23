using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager : MonoBehaviour
{
    private Animator ani;
    private GameInput gameinput;
    //private Player plS;
    private PlayerManager playermanager;
    private DamgeManager damageManager;
    private HeathManager heathManager;
    GamePlay gamePlay;
    AttackPlayer attackPlayer;
    void Start()
    {
    }

    void Update()
    {
        //Idle();
        //Walk();
        //Jump();
        //FlipMan01();
    }
    //public void FlipMan01()
    //{
    //    ani.SetBool("Front", playermanager.isMove  && playermanager.Front);
    //    ani.SetBool("Back", playermanager.isMove && playermanager.Back);
    //    ani.SetBool("Left", playermanager.isMove  && playermanager.Left);
    //    ani.SetBool("Right", playermanager.isMove && playermanager.Right);
    //}
    public void Idle()
    {
        ani.SetBool("Idle", playermanager.isLifepPlayer == true && playermanager.isOnGround == true);
    }
    public void Jump()
    {
        ani.SetBool("Jump",playermanager.isJumped == true && playermanager.isOnGround == false);
    }
     public void Walk()
    {
        ani.SetBool("Walk", playermanager.isMove == true && playermanager.isOnGround == true);
    }
    // public void Shoot(InputAction.CallbackContext context)
    //{
    //    if (/*attackPlayer.ShootInput == true ||*//* Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.J)*/
    //        context.performed)
    //    {
    //        ani.SetBool("Shoot", true);
    //    }
    //    else
    //    {
    //        ani.SetBool("Shoot", false);
    //    }
    //}
    public void Climb()
    {

    }
    //void MapTransition()
    //{

    //    ani.SetTrigger("endMap");
    //    gamePlay.InputKey();
    //    ani.SetTrigger("startMap");
    //}

}
