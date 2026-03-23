using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;


[System.Serializable]
public class GameSaveList //danh sách các data
{
    public List<Score> ListScore; 
    public List<Heath> ListHeath; 
    public List<Weapon> ListWeapon; 
}
[System.Serializable]
public class Score //chi tiết điểm số
{
    public int CoinInSave;
}
[System.Serializable]
public class Heath //chi tiết máu
{
    public int HeathInSave;
}
[System.Serializable]
public class Weapon //chi tiết Weapon data
{
    public string IdInSave;
    public string NameInSave;
    public int DamageInSave;
    public string EffectInSave;
}

public class SaveFile : MonoBehaviour
{
    public GameSaveList GameSaveList;
    public ScoreManager scoreManager;
    public HeathManager heathManager;
    public WeaponDataManager weaponDataManager;

    private void Start()
    {
        scoreManager =FindFirstObjectByType<ScoreManager>();
        heathManager = FindFirstObjectByType<HeathManager>();
        weaponDataManager = FindFirstObjectByType<WeaponDataManager>();

        if (scoreManager == null || heathManager == null || weaponDataManager == null)
        {
            Debug.LogError("Không tìm thấy ScoreManager hoặc HeathManager hoặc WeaponDataManager trong scene!");
            return;
        }

        LoadData();
        LoadListData();
        SyncDataWithManager();
    }
    public void LoadListData() // Đảm bảo rằng các list luôn được khởi tạo
    {
        if (GameSaveList == null)
        {
            GameSaveList = new GameSaveList();
        }
        if (GameSaveList.ListScore == null)
        {
            GameSaveList.ListScore = new List<Score>();
        }
        if (GameSaveList.ListHeath == null)
        {
            GameSaveList.ListHeath = new List<Heath>();
        }
        if (GameSaveList.ListWeapon == null)
        {
            GameSaveList.ListWeapon = new List<Weapon>();
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1)) // phím số 1 để lưu dữ liệu
        {
            Debug.Log("Save");
            foreach (var CoinData in scoreManager.listCoinInGame)
            {
                SaveCoin(CoinData.CoinInGame);
            }
            foreach (var HeathData in heathManager.listHeathInGame)
            {
                SaveHeath(HeathData.heathInGame);
            }
            foreach (var weaponData in weaponDataManager.listWeaponDataInGame)
            {
                SaveWeapon(weaponData.IdInGame, weaponData.nameInGame, weaponData.damageInGame, weaponData.effectInGame);
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2)) // phím số 2 để tải dữ liệu
        {
            Debug.Log("Load");
            LoadData();
        }
    }
    public void SaveCoin(int Coin) // Lưu điểm vào file 
    {
        LoadListData();

        // Cập nhật phần tử đầu tiên trong danh sách (giả sử danh sách chỉ có 1 phần tử)
        if (GameSaveList.ListScore.Count > 0)
        {
            if (Coin > GameSaveList.ListScore[0].CoinInSave)
            {
                GameSaveList.ListScore[0].CoinInSave += Coin - GameSaveList.ListScore[0].CoinInSave; // Cập nhật điểm
            }
        }
        else
        {
            // Nếu danh sách trống, thêm mới phần tử với giá trị điểm
            Score newScore = new Score();
            newScore.CoinInSave = Coin;
            GameSaveList.ListScore.Add(newScore);
        }

        SaveData();
    }
    public void SaveHeath(int heath) // Lưu máu vào file 
    {
        LoadListData();

        // Cập nhật máu của phần tử đầu tiên trong danh sách (giả sử danh sách chỉ có 1 phần tử)
        if (GameSaveList.ListHeath.Count > 0)
        {
            if (heath != GameSaveList.ListHeath[0].HeathInSave)
            {
                GameSaveList.ListHeath[0].HeathInSave = heath; // Cập nhật máu
            }
        }
        else
        {
            // Nếu danh sách trống, thêm mới phần tử với giá trị máu
            Heath newHeath = new Heath();
            newHeath.HeathInSave = heath;
            GameSaveList.ListHeath.Add(newHeath);
        }

        SaveData();
    }
    public void SaveWeapon(string Id, string name, int damage, string effect) // Lưu weapon data vào file
    {
        LoadListData();
        Weapon existingWeapon = GameSaveList.ListWeapon.Find(w => w.IdInSave == Id); // Tìm kiếm weapon có Id trùng khớp

        if (existingWeapon != null)// cập nhật thông tin
        {
            existingWeapon.NameInSave = name;
            existingWeapon.DamageInSave = damage;
            existingWeapon.EffectInSave = effect;
            Debug.Log("Updated existing weapon.");
        }
        else //thêm mới
        {
            Weapon newWeapon = new Weapon();
            newWeapon.IdInSave = Id;
            newWeapon.NameInSave = name;
            newWeapon.DamageInSave = damage;
            newWeapon.EffectInSave = effect;
            GameSaveList.ListWeapon.Add(newWeapon);
            Debug.Log("Added new weapon.");
        }
        SaveData();
    }
    public void SyncDataWithManager() // đồng bộ data từ fileSave vào game
    {
        #region đồng bộ Score
        if (scoreManager.listCoinInGame.Count == 0) // Khởi tạo một phần tử ban đầu trong danh sách nếu danh sách rỗng
        {
            scoreManager.listCoinInGame.Add(new ScoreInGame { CoinInGame = 0 });
        }

        if (GameSaveList.ListScore != null && GameSaveList.ListScore.Count > 0)// Kiểm tra nếu GameSaveList.ListScore khác null và có ít nhất một phần tử
        {
            // Cập nhật giá trị điểm đã lưu vào scoreManager
            if (scoreManager.listCoinInGame.Count > 0) // Kiểm tra danh sách coin trong scoreManager
            {
                scoreManager.listCoinInGame[0].CoinInGame = GameSaveList.ListScore[0].CoinInSave;
                //Debug.Log("đồng bộ coin: " + scoreManager.listCoinInGame[0].CoinInGame);
            }
            else
            {
                Debug.LogWarning("scoreManager.listCoinInGame trống!");
            }
        }
        else
        {
            Debug.LogWarning("GameSaveList.ListScore trống or không có giá trị!");
        }
        #endregion
        #region đồng bộ Heath
        if (heathManager.listHeathInGame.Count == 0) // Khởi tạo một phần tử ban đầu trong danh sách nếu danh sách rỗng
        {
            heathManager.listHeathInGame.Add(new HeathInGame { heathInGame = heathManager.startHeath });
        }
        if (GameSaveList.ListHeath != null && GameSaveList.ListHeath.Count > 0)// Kiểm tra nếu GameSaveList.ListHeath khác null và có ít nhất một phần tử
        {
            // Cập nhật giá trị điểm đã lưu vào heathManager
            if (heathManager.listHeathInGame.Count > 0) // Kiểm tra danh sách coin trong heathManager
            {
                heathManager.listHeathInGame[0].heathInGame = GameSaveList.ListHeath[0].HeathInSave;
                //Debug.Log("đồng bộ heath: " + heathManager.listHeathInGame[0].heathInGame);
            }
            else
            {
                Debug.LogWarning("heathManager.listHeathInGame trống!");
            }
        }
        else
        {
            Debug.LogWarning("GameSaveList.ListHeath trống or không có giá trị!");
        }
        #endregion

        #region đồng bộ Weapon data
        if (weaponDataManager != null && GameSaveList.ListWeapon != null)
        {
            weaponDataManager.LoadWeaponData(GameSaveList.ListWeapon); // Truyền danh sách vũ khí đã tải vào WeaponDataManager
        }
        #endregion
    }
    public void LoadData() // Tải dữ liệu từ file JSON (Lock)
    {
        string file = "Save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        if (!File.Exists(filePath))
        {
            LoadListData();
            SaveData(); // Lưu file trống
        }
        else
        {
            // Đọc dữ liệu từ file JSON
            string json = File.ReadAllText(filePath);
            GameSaveList = JsonUtility.FromJson<GameSaveList>(json);

            LoadListData();
        }

        Debug.Log("Load data success!");
    }
    public void SaveData() //Lưu dữ liệu hiện tại vào file JSON (lock)
    {
        string file = "Save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);

        // Chuyển đổi đối tượng GameSaveList thành chuỗi JSON
        string json = JsonUtility.ToJson(GameSaveList, true);

        // Ghi dữ liệu vào file
        File.WriteAllText(filePath, json);
        Debug.Log("FilePath Location at: " + filePath);
    }
}
