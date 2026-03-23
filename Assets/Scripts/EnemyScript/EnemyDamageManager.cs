using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    //private Checkzone checkzone; //(tạm chưa dùng)

    [SerializeField] public List<int> listBigDamage = new List<int>(); // Danh sách lưu BigCountDamage
    [SerializeField] public List<int> listSmallDamage = new List<int>(); // Danh sách lưu SmallCountDamage //(tạm chưa dùng)

    [SerializeField] public bool bigDamage = false;
    [SerializeField] public bool smallDamaged = false;
    public int CountBigDamage = 0; // Biến đếm số lượng Bigdamage
    public int CountSmallDamage = 0; // Biến đếm số lượng Smalldamage //(tạm chưa dùng)

    private void Start()
    {
        //checkzone = GetComponent<Checkzone>(); //(tạm chưa dùng)
    }
    private void Update()
    {
        changeListDamage();
    }
    public void changeListDamage()
    {
        if (bigDamage == true) // add list big damage
        {
            //CountBigDamage = checkzone.listBigZoneDamage.Count; //(tạm chưa dùng)
            if (!listBigDamage.Contains(CountBigDamage)) // kiem tra xem count trong listDamage co = countDamage hay khong
            {
                listBigDamage.Add(CountBigDamage);
            }
        }
        if (smallDamaged == true) // add list small damage //(tạm chưa dùng)
        {
            //CountSmallDamage = checkzone.listSmallZoneDamage.Count; //(tạm chưa dùng)
            if (!listSmallDamage.Contains(CountSmallDamage)) // kiem tra xem count trong listDamage co = countDamage hay khong
            {
                listSmallDamage.Add(CountSmallDamage);
            }
        }
    }

}
