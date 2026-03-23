using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    public GameObject Cutting;
    public float attackCooldown = 1f;//thời gian chờ giữa những lần tấn công(tốc đánh) (THAY ĐỔI ĐƯỢC)
    private float attackCool = 0.2f;//thời lương của attack 
    [SerializeField] public bool isAttacking = false;//trạng thái tấn công
    private float lastAttackTime;//thời điểm cuối cùng tấn công

    public void Attack()
    {
        Cut();
    }
    public void Cut()
    {
        // nếu lastAttackTime vượt qua thời gian chờ (attackCooldown), Enemy sẽ tấn công
        if (Time.time > lastAttackTime + attackCooldown && !isAttacking)
        {
            isAttacking = true;
            Debug.Log("Enemy đang tấn công!");
            if (Cutting != null)
            {
                Cutting.SetActive(true);
                Cutting.GetComponent<Collider2D>().enabled = true;  // Đảm bảo Collider đang bật
            }
            Invoke("EndAttack", attackCool); // dừng tấn công trong 1 thời gian (attackCool)
        }
    }
    public void EndAttack()
    {
        if (Cutting != null)
        {
            Cutting.SetActive(false);
        }
        isAttacking = false;
        lastAttackTime = Time.time;
    }
}
