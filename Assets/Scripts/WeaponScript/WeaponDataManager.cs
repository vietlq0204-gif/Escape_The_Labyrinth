using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
/// <summary>
/// - load data cho weapon từ file save
/// - kiểm tra weapon data trong game và thêm weapon mới nếu list thiếu weapon 
/// </summary>
[System.Serializable]
public class WeaponDataManager : MonoBehaviour
{
    public List<WeaponDataIngame> listWeaponDataInGame = new List<WeaponDataIngame>() { };

    private void Start()
    {
    }
    private void Update()
    {
        CheckAndAddWeapons();
    }

    public void LoadWeaponData(List<Weapon> savedWeapons) // load data từ File save đến listWeaponDataInGame
    {
        listWeaponDataInGame.Clear(); // reset list

        foreach (Weapon savedWeapon in savedWeapons)
        {
            WeaponDataIngame newWeapon = new WeaponDataIngame
            {
                IdInGame = savedWeapon.IdInSave,
                nameInGame = savedWeapon.NameInSave,
                damageInGame = savedWeapon.DamageInSave,
                effectInGame = savedWeapon.EffectInSave
            };
            listWeaponDataInGame.Add(newWeapon);
        }

        //Debug.Log("Weapon data loaded from save file!");
    }
    public void CheckAndAddWeapons() // thêm weapon mới vào list nếu list thiếu weapon 
    {
        /// <summary>
        /// kiểm tra weapon data trong game với listWeaponDataInGame
        /// thêm weapon mới vào listWeaponDataInGame nếu listWeaponDataInGame thiếu weapon 
        /// </summary>

        foreach (Transform child in transform)
        {
            WeaponData weaponData = child.GetComponent<WeaponData>(); // (CHƯA TỐI ƯU HIỆU SUÁT)

            if (weaponData != null)
            {
                bool found = false;

                foreach (var ingameListWeapon in listWeaponDataInGame)
                {
                    if (weaponData.Id == ingameListWeapon.IdInGame) // Id weapon trong transform = Id weapon trong lisst
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) // không tìm thấy Id trùng khớp
                {
                    Debug.Log($"Weapon mới với ID: {weaponData.Id} và tên: {child.name}");
                    WeaponDataIngame newWeapon = new WeaponDataIngame
                    {
                        IdInGame = weaponData.Id,
                        nameInGame = weaponData.nameTemp,
                        damageInGame = weaponData.damageTemp,
                        effectInGame = weaponData.effectTemp
                    };

                    listWeaponDataInGame.Add(newWeapon); // Thêm weapon mới vào list
                    Debug.Log($"Weapon {newWeapon.nameInGame} đã được thêm vào listWeaponDataInGame");
                }
            }
            else
            {
                Debug.LogWarning($"WeaponData ID không được tìm thấy trên {child.name}");
            }
        }
    }
}
[System.Serializable]
public class WeaponDataIngame
{
    public string IdInGame;
    public string nameInGame;
    public int damageInGame;
    public string effectInGame;
}
