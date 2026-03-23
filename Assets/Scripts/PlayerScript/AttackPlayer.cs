using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>                                                                           
/// - kiểm tra Player:      nếu cho phép tấn công thì tìm weapon đang có trong UseItem
///                             nếu không có GameObject có layer là "weapon" thì kích hoạt đấm
///                             nếu có GameObject có tag "Stick" thì kích hoạt chém (tương tự với các weapon khác)
/// - Kiểm tra weppon:      lấy thông tin (damageWeapon, EffectWeapon) từ weapon và áp dụng cho Cutting/Butllet         
/// - kiểm tra Level:       lấy thông tin (strength, SpeedAttack,...) từ Level Player và áp dụng lên weapon
///                             Strength = 2 thì + 2% damage cho Punching (tương tự với các weapon khác)
///                             SpeedAttack = 2 thì + 2% AttackCooldown
/// -                       nếu isCut = true thì kích hoạt Cutting (tương tự với các weapon khác)
/// </summary>
public class AttackPlayer : MonoBehaviour
{
    public GamePlay gamePlay;

    public Transform Gun;
    public GameObject bullet;
    public GameObject Cutting;
    public GameObject Punching;

    public bool isPunch = false;
    public bool isCut = false;
    public bool isShoot = false;
    private float AttackCooldown = 1f; // Thời gian nghỉ giữa những lần tấn công (THAY DOI DUOC)
    
    void Start()
    {

    }
    void Update()
    {
        Invoke("CheckNullAndUpdate", 2f); // tạm đợị tí (tránh lỗi render) (CHƯA TỐI ƯU)
        CheckAttack();
        CheckWeapon();
    }
    private void FixedUpdate()
    {
      
    }
    private void CheckNullAndUpdate()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.name == "UseItem")
                {
                    foreach (Transform weapon in child)
                    {
                        if (child.childCount > 0)
                        {
                            weapon.gameObject.SetActive(true);
                            //Debug.Log(weapon.gameObject.name);
                        }

                    }
                }
            }
        }
    }
    public void CheckWeapon() // kiểm tra vũ khí
    {
        if (gamePlay.canAttack)
        {
            int weaponLayer = LayerMask.NameToLayer("Weapon"); // Lấy layer ID của Weapon
            bool weaponFound = false;
            foreach (Transform child in transform)
            {
                if (child.name == "UseItem")
                {
                    foreach (Transform weapon in child)
                    {
                        if (weapon.gameObject.layer == weaponLayer)
                        {
                            Debug.Log($"{weapon.name} of {transform.name}");
                            weaponFound = true;
                        }
                        if (weapon.CompareTag("stick"))
                        {
                            isCut = true;
                        }
                        else if (weapon.CompareTag("gun"))
                        {
                            isShoot = true;
                        }
                    }
                    isPunch = !weaponFound;
                }
                //else Debug.LogWarning($"không tìm thấy: UseItem");
            }
        }
        else
        {
            isPunch = false;
            isCut = false;
            isShoot = false;
        }
        Attack();
    }
    public void Attack() // điều khiển các loại tấn công
    {
        Punch();
        Cut();
        Shoot();
    }
    public void Punch()// gọi giả lập đường đấm
    {
        if (isPunch)
        {
            if (Cutting != null)
            {
                Punching.SetActive(true);
            }
        }
        else
        {
            if (Cutting != null)
            {
                Punching.SetActive(false);
            }
        }
    }
    public void Cut() // gọi giả lập đường chém
    {
        if (isCut)
        {
            if (Cutting != null)
            {
                Cutting.SetActive(true); 
                
            }
        }
        else
        {
            if (Cutting != null)
            {
                Cutting.SetActive(false);
            }
        }

    }
    public void Shoot()// Gọi đạn từ Gun
    {
        if (isShoot)
        {
            GameObject newBullet = Instantiate(bullet, Gun.position, transform.rotation);
            bullet bullets = newBullet.GetComponent<bullet>();
            bullets.SetDirection(Gun.right); // Gửi hướng đến viên đạn
        }
    }

    IEnumerator AttackCoroutine()// (chưa dùng)
    {
        yield return new WaitForSeconds(AttackCooldown); // Chờ trước khi cho phép tấn công lại
    }
    private void CheckAttack() // kiểm ra chức năng (TEST))
    {
        if (Punching != null)
        {
            //Debug.Log($"Punching status: " + Punching.activeSelf);
        }
        else if (Cutting != null)
        {
            //Debug.Log($"Cutting status: " + Cutting.activeSelf);
        }
        else if (isShoot == true)
        {
            //Debug.Log($"Shooting true");
        }
    }
}
