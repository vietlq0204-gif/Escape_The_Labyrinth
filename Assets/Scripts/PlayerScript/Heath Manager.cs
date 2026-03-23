using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class HeathManager : MonoBehaviour
{
    public DamgeManager damgeManager;
    public List<HeathInGame> listHeathInGame = new List<HeathInGame>() { };
    public bool inputFalseHeath = false;
    public int startHeath = 10; // Giá trị ban đầu cho máu (THAY DOI DUOC)
    public int lastHeath = 0;
    public bool isLife;
    public bool Revival = false; // hồi sinh

    private void Start()
    {
        StartHeathManager();
    }

    private void Update()
    {
        if (!isLife)
        {
            RespawnHeath();
        }
        else
        {
            InputDamageValue();
        }
        
    }

    public void StartHeathManager()
    {
        lastHeath = 0;
        if (isLife && listHeathInGame.Count == 0) // Đảm bảo danh sách rỗng và khởi tạo các giá trị
        {
            HeathInGame newHeath = new HeathInGame();
            newHeath.heathInGame = startHeath; // Đặt giá trị ban đầu cho máu
            lastHeath = newHeath.heathInGame;
            listHeathInGame.Add(newHeath);
        }
        else
        {
            lastHeath = listHeathInGame[0].heathInGame;
        }
    }
    public void RespawnHeath()  // Hồi sinh nếu Revival = true
    {
        if (Revival)
        {
            damgeManager.ResetDamageState(); // Reset lại trạng thái sát thương trước khi hồi sinh
            if (listHeathInGame.Count > 0)
            {
                listHeathInGame[0].heathInGame = startHeath;
                lastHeath = listHeathInGame[0].heathInGame;
                //Debug.LogWarning($"Revival: {name}");
            }
            else
            {
                StartHeathManager();
            }
            isLife = true;
            Revival = false;
        }
    }
    public void InputDamageValue() // Xử lý thiệt hại nếu còn sống
    {
        //Debug.Log($"value in listInputDamage[] of {name} is: {damgeManager.listInputDamage[0].smallDamaged} ");
        if (isLife && damgeManager.listInputDamage[0].smallDamaged > 0)
        {
            //Debug.Log($"Sát thương nhận được: {damgeManager.listInputDamage[0].smallDamaged}");
            inputFalseHeath = true;
            exceptheathManager();
        }
        //else
        //{
        //    Debug.Log($"KHONG NHAN DUWOC DAMAGE");
        //}
    }
    private void exceptheathManager() // Tính toán thiệt hại
    {
        //Debug.Log($"Giá trị sát thương hiện tại: {damgeManager.listInputDamage[0].smallDamaged}");
        if (inputFalseHeath && listHeathInGame[0].heathInGame > 0)
        {
            listHeathInGame[0].heathInGame -= damgeManager.listInputDamage[0].smallDamaged;
            //Debug.Log($"Máu sau khi nhận sát thương: {listHeathInGame[0].heathInGame}");
            lastHeath = listHeathInGame[0].heathInGame;
            damgeManager.listInputDamage[0].smallDamaged = 0;
            if (listHeathInGame[0].heathInGame <= 0)
            {
                isLife = false;
            }
        }
        inputFalseHeath = false;
    }
}

[System.Serializable]
public class HeathInGame
{
    public int heathInGame;
}