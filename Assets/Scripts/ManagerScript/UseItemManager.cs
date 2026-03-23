using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UseItemManager : MonoBehaviour
{
    WeaponDataManager weaponDataManager;
    public List<GameObject> ListWeaponting = new List<GameObject>(); // Danh sách lưu trữ các Enemy đang chết
    public string Id;
    public string nameTemp;
    public int damageTemp;
    public string effectTemp;
    private void Start()
    {
        weaponDataManager = Object.FindFirstObjectByType<WeaponDataManager>(); // Tìm và gán WeaponDataManager
    }
    private void Update()
    {
        CheckChild();
    }
    private void CheckChild()
    {
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                if (child.gameObject.layer == LayerMask.NameToLayer("Weapon"))
                {
                    //foreach (GameObject wea in weaponDataManager.listWeaponDataInGame)
                    //{

                    //}
                    Debug.Log($"Layer : {child.name}");
                }
            }
        }
    }
}
