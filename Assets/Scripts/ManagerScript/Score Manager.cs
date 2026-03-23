using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ScoreManager : MonoBehaviour
{
    public List<ScoreInGame> listCoinInGame = new List<ScoreInGame>();
    public int coins;
    public bool IsCoin = false;

    void Start()
    {
        // Khởi tạo một phần tử ban đầu trong danh sách nếu danh sách rỗng
        if (listCoinInGame.Count == 0)
        {
            listCoinInGame.Add(new ScoreInGame { CoinInGame = 0 });
        }
    }

    void Update()
    {
        AddScoreList();
    }

    public void AddScoreList()
    {
        if (IsCoin)
        {
            coins += 1; // Tăng giá trị coin lên 1 (cho mỗi lần IsCoin = true)
            if (coins != listCoinInGame[0].CoinInGame || coins == listCoinInGame[0].CoinInGame)
            {
                listCoinInGame[0].CoinInGame += coins; // Cập nhật CoinInGame theo giá trị của coins
            }
            IsCoin = false; // Đặt lại cờ IsCoin về false sau khi cập nhật
            coins = 0; // Đặt lại coins sau khi cập nhật

            //Debug.Log("Updated CoinInGame: " + listCoinInGame[0].CoinInGame);
        }
    }
}
[System.Serializable]
public class ScoreInGame 
{
    /// <summary>
    /// kkkkkk
    /// </summary
    public int CoinInGame; // Giá trị điểm trong game
}

