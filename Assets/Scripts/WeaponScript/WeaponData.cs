using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
/// <summary>
/// lấy data từ bên ngoài và cập nhật cho weapon trong game
/// - gửi Data weapon đến Class dataWeapon để lưu vào file
/// </summary>
public class WeaponData : MonoBehaviour
{
    WeaponDataManager weaponDataManager;

    WeaponDataIngame Weapon = new WeaponDataIngame();

    public string Id;
    public string nameTemp;
    public int damageTemp;
    public string effectTemp;
    private void Start()
    {
        weaponDataManager = Object.FindFirstObjectByType<WeaponDataManager>(); // Tìm và gán WeaponDataManager
        if (weaponDataManager == null)
        {
            Debug.LogError("WeaponDataManager không được tìm thấy!");
            return;
        }
        CreateWeaponData();
    }
    private void Update()
    {
        //Debug.Log($" weapon In game: {Weapon.name}");
    }
    public void CreateWeaponData()// tạo weapon mới (tránh lỗi list = null)
    {
        if (weaponDataManager != null && weaponDataManager.listWeaponDataInGame.Count <= 0)
        {
            WeaponDataIngame newWeapon = new WeaponDataIngame
            {
                IdInGame = this.Id,
                nameInGame = this.nameTemp,
                damageInGame = this.damageTemp,
                effectInGame = this.effectTemp
            };
            weaponDataManager.listWeaponDataInGame.Add(newWeapon);
        }
    }
    public void SyncData() // đồng bộ Data (đang lỗi)
    {
        this.Weapon.IdInGame = this.Id;
        this.Weapon.nameInGame = this.nameTemp;
        this.Weapon.damageInGame = this.damageTemp;
        this.Weapon.effectInGame = this.effectTemp;
    }
}

