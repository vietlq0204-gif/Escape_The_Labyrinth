using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeathManager : MonoBehaviour
{
    private Checkzone checkzone;

    [SerializeField] public List<int> listHeathManager = new List<int>() { }; // Danh sách lưu heath manager

    [SerializeField] public bool bigHeath = false;
    [SerializeField] public bool smallHeath = false; //(tạm chưa dùng)
    [SerializeField] public int lastHeath = 0;
    [SerializeField] public int startHeath = 5;
    [SerializeField] public bool isLifeHeath = true;

    private void Start()
    {
        StartHeathManager();
        checkzone = GetComponent<Checkzone>();
    }
    private void Update()
    {
        //lastHeathManager();
        heathManager();
    }
    public void StartHeathManager()
    {
        for (int i = 0; i < startHeath; i++) // thêm máu vào list máu
        {
            listHeathManager.Add(i);
        }
        //lastHeathManager();
    }
    private void lastHeathManager()
    {
        lastHeath = listHeathManager.Count;
    } //(tạm chưa dùng)
    private void heathManager()
    {
        if (bigHeath == true && listHeathManager.Count > 0) // nếu dính bigDamage thì hết máu //(tạm chưa dùng)
        {
            listHeathManager.Clear();
            lastHeath = listHeathManager.Count;
        }
        if (smallHeath == true && listHeathManager.Count > 0) // nếu dính SmallDamage thì bị trừ máu 
        {
            listHeathManager.RemoveAt(listHeathManager.Count - 1);
            lastHeath = listHeathManager.Count;
        }
        if (listHeathManager.Count <= 0) // nếu list máu <= 0 thì chết mẹ lun :)
        {
            isLifeHeath = false;

        }
        smallHeath = false;
    }
    void OnTriggerEnter2D(Collider2D colition)
    {
        if (colition.CompareTag("Climbing"))
        {
            smallHeath = true;
        }
    }
}
