using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class DamgeManager : MonoBehaviour
{
    public GamePlay gamePlay;
    public Checkzone checkzone;
    public HeathManager heathManager;

    private Coroutine damageCoroutine;
    public List<Damage> listInputDamage = new List<Damage>(); // Danh sách lưu Damage khi kiểm tra

    private bool isDamageCoroutineRunning = false; // cờ kiểm soát Coroutine
    public bool isAttacked = false;
    public bool isNotAttacked = false;
    public int inputValueSmallDamage; // lượng damage nhận vào để trừ máu, dựa trên vụ khí bị đánh
    private void Start()
    {
        //ResetDamageState();
        if (listInputDamage.Count == 0) // Khởi tạo một phần tử ban đầu trong danh sách nếu danh sách rỗng
        {
            listInputDamage.Add(new Damage { smallDamaged = 0});
        }
    }
    private void Update()
    {
        ClearDamageInList();
        LoopAddDamageAttacked();

    }
    private void FixedUpdate()
    {
        
    }
    private void LoopAddDamageAttacked() // lặp add damage nếu bị tấn công
    {
        if (isAttacked)
        {
            inputValueSmallDamage += 1;// (chưa xong)
            listInputDamage[0].smallDamaged += inputValueSmallDamage;
            //Debug.Log($"{gameObject.name} bi dinh damage:{listInputDamage[0].smallDamaged}");
            inputValueSmallDamage = 0;

            gamePlay.canAttack = false;
            isNotAttacked=true;
            isAttacked = false; // chỉ cho pép sô lần Input Damage được lặp bằng số lần bị tấn công
        }
    }
    #region logic nhân damage trong vùng nguy hiểm
    public void isDamageZone() //Enemy đang ở trong vùng nguy hiểm
    {
        if (checkzone.dangerousZone && heathManager.isLife)
        {
            if (checkzone.smallDmgZone)
            {
                if (!isDamageCoroutineRunning) // Chỉ khởi chạy nếu Coroutine chưa chạy
                {
                    damageCoroutine = StartCoroutine(LoopAddsDamageInZoneDangerous());
                }
            }
        }
    }
    public IEnumerator LoopAddsDamageInZoneDangerous()
    {
        isDamageCoroutineRunning = true;

        while (checkzone.smallDmgZone)
        {
            inputValueSmallDamage += 1; //(chưa xong)
            listInputDamage[0].smallDamaged += inputValueSmallDamage;
            //Debug.Log($"bi dinh damage:{listInputDamage[0].smallDamaged}");
            inputValueSmallDamage = 0;
            yield return new WaitForSeconds(1f); // Lặp lại sau 1 giây
        }

        isDamageCoroutineRunning = false; // Đánh dấu Coroutine đã kết thúc
    }
    #endregion
    public void ResetDamageState() //Đặt lại tất cả các biến liên quan đến sát thương (đang lỗi)
    {
        //Debug.Log($"reset damage of GameObject");
        listInputDamage[0].smallDamaged = 0;
        inputValueSmallDamage = 0;
        isAttacked = false;
        isNotAttacked = false;
        isDamageCoroutineRunning = false; //sẵn sàng cho các lần gọi tiếp theo.
    }
    public void ClearDamageInList() // Xóa smallDamaged khi Enemy không còn trong vùng nguy hiểm hoặc chết
    {
        if (!checkzone.dangerousZone || !heathManager.isLife)
        {
            listInputDamage[0].smallDamaged = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D Trigger)
    {
        //Debug.Log($"{gameObject.name} va chạm với {Trigger.gameObject.name}");
        if (Trigger.gameObject.CompareTag("cutting") && !isNotAttacked)
        {
            // Lấy parent của Trigger.gameObject
            Transform parentTransform = Trigger.gameObject.transform.parent;

            if (parentTransform != null)
            {
                Debug.Log($"{gameObject.name} bị chém bởi {parentTransform.name}");
            }
            isAttacked = true;
            isNotAttacked = true; // tránh va chạm liên tục

        }
    }
    private void OnTriggerExit2D(Collider2D Trigger)
    {
        if (Trigger.gameObject.CompareTag("cutting"))
        {
            isNotAttacked = false;
            isAttacked = false;
        }
    }

    public void CheckEror() // test
    {
        if (heathManager == null)
        {
            Debug.LogError("HeathManager không được tham chiếu trong DamgeManager.");
        }
        if (checkzone == null)
        {
            Debug.LogError("checkzone không được tham chiếu trong DamgeManager.");
        }
    }
}
[System.Serializable]
public class Damage
{
    public int smallDamaged;
}
